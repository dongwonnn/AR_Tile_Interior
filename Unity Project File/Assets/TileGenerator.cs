using System.Collections;
using System.Collections.Generic;
using GoogleARCore;
using GoogleARCore.Examples.Common;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TileGenerator : MonoBehaviour
{
    /// <summary>
    /// The first-person camera being used to render the passthrough camera image (i.e. AR
    /// background).
    /// </summary>
    public Camera FirstPersonCamera;

    /// <summary>
    /// A prefab to place when a raycast from a user touch hits a feature point.
    /// </summary>
    public GameObject GameObjectPointPrefab;

    /// <summary>
    /// The rotation in degrees need to apply to prefab when it is placed.
    /// </summary>
    private const float k_PrefabRotation = 180.0f;

    private List<GameObject> Points = new List<GameObject>();
    private List<Vector3> positions = new List<Vector3>();
    public Text areaText;
    float areaSize = 0;
    Renderer rend;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        areaText.text = "Size: " + areaSize;
    }

    // Update is called once per frame
    void Update()
    {
        Touch touch;
        if (Input.touchCount < 1 || (touch = Input.GetTouch(0)).phase != TouchPhase.Began)
        {
            return;
        }
        // Should not handle input if the player is pointing on UI.
        if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
        {
            return;
        }

        // Raycast against the location the player touched to search for planes.
        TrackableHit hit;
        TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinPolygon |
            TrackableHitFlags.FeaturePointWithSurfaceNormal;

        if (Frame.Raycast(touch.position.x, touch.position.y, raycastFilter, out hit))
        {
            // Use hit pose and camera pose to check if hittest is from the
            // back of the plane, if it is, no need to create the anchor.
            if ((hit.Trackable is DetectedPlane) &&
                Vector3.Dot(FirstPersonCamera.transform.position - hit.Pose.position,
                    hit.Pose.rotation * Vector3.up) < 0)
            {
                Debug.Log("Hit at back of the current DetectedPlane");
            }
            else
            {
                // Instantiate prefab at the hit pose.
                var ob = Instantiate(GameObjectPointPrefab, hit.Pose.position, hit.Pose.rotation);
                Points.Add(ob);
                positions.Add(ob.transform.position);

                if (Points.Count >= 4)
                {
                    Vector3[] vert = new Vector3[positions.Count];

                    for (int i = 0; i < Points.Count; i++)
                    {
                        vert[i].x = positions[i].x;
                        vert[i].y = positions[i].y;
                        vert[i].z = positions[i].z;
                    }

                    TriangulatorZ tr = new TriangulatorZ(vert);
                    int[] indices = tr.Triangulate();

                    Vector3[] vertices = new Vector3[vert.Length];
                    for (int i = 0; i < vertices.Length; i++)
                    {
                        vertices[i] = new Vector3(vert[i].x, vert[i].y, vert[i].z);
                    }

                    // Create the mesh
                    Mesh msh = GetComponent<MeshFilter>().mesh;
                    msh.vertices = vertices;
                    msh.triangles = indices;
                    msh.RecalculateNormals();
                    msh.RecalculateBounds();

                    // Create UV maps for mesh
                    Vector2[] uvs = new Vector2[vertices.Length];
                    Bounds bounds = msh.bounds;
                    int j = 0;
                    while (j < uvs.Length)
                    {
                        uvs[j] = new Vector2(vertices[j].x / bounds.size.x, vertices[j].z / bounds.size.z);
                        j++;
                    }
                    msh.uv = uvs;

                    // Set Texture Size
                    Texture tx = rend.material.GetTexture("_MainTex");

                    float textureToMeshX = ((float)tx.width / tx.height) * 2f;

                    rend.material.mainTextureScale = new Vector2(bounds.size.x, bounds.size.z);

                    // Set Area Size
                    Vector3[] ver = this.GetComponent<MeshFilter>().mesh.vertices;
                    int[] triangles = this.GetComponent<MeshFilter>().mesh.triangles;

                    
                    float result = 0f;
                    for (int p = 0; p < triangles.Length; p += 3)
                    {
                        result += (Vector3.Cross(ver[triangles[p + 1]] - ver[triangles[p]],
                                    ver[triangles[p + 2]] - ver[triangles[p]])).magnitude;
                    }
                    result *= 0.5f;
                    areaText.text = "Size: " + result;

                }
                // Compensate for the hitPose rotation facing away from the raycast (i.e.
                // camera).
                gameObject.transform.Rotate(0, k_PrefabRotation, 0, Space.Self);

                // Create an anchor to allow ARCore to track the hitpoint as understanding of
                // the physical world evolves.
                var anchor = hit.Trackable.CreateAnchor(hit.Pose);

                // Make game object a child of the anchor.
                gameObject.transform.parent = anchor.transform;

            }
        }
    }
}



