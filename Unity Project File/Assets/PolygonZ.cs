using System.Collections;
using System.Collections.Generic;
using GoogleARCore;
using GoogleARCore.Examples.Common;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PolygonZ : MonoBehaviour
{
    public Material mat;

    public Camera FirstPersonCamera;

    public GameObject GameObjectPointPrefab;
    
    private const float k_PrefabRotation = 180.0f;

    private List<GameObject> Points = new List<GameObject>();
    private List<Vector3> positions = new List<Vector3>();

    Mesh mesh;
    MeshFilter meshFilter;
    MeshRenderer meshRenderer;
    LineRenderer lineRenderer;

    bool isCreated = false;

    // Start is called before the first frame update
    void Start()
    {
        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();
        lineRenderer = GetComponent<LineRenderer>();
        meshRenderer.material = mat;

        mesh = new Mesh();
        meshFilter.mesh = mesh;
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

        if (!isCreated)
        {
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
                    var ob = Instantiate(GameObjectPointPrefab, hit.Pose.position, Quaternion.identity);
                    Points.Add(ob);
                    positions.Add(ob.transform.position);

                    if (Points.Count >= 2)
                    {
                        lineRenderer.SetVertexCount(Points.Count);

                        if (Points.Count >= 3) lineRenderer.loop = enabled;
                        lineRenderer.SetPositions(positions.ToArray());
                    }

                    // Compensate for the hitPose rotation facing away from the raycast (i.e.
                    // camera).
                   // gameObject.transform.Rotate(0, k_PrefabRotation, 0, Space.Self);

                    // Create an anchor to allow ARCore to track the hitpoint as understanding of
                    // the physical world evolves.
                   // var anchor = hit.Trackable.CreateAnchor(hit.Pose);

                    // Make game object a child of the anchor.
                   // gameObject.transform.parent = anchor.transform;
                }
            }
        }
    }
    public void onClickedButton()
    {
        GenerateMesh();
        DestroyPoints();
        Destroy(lineRenderer);
        isCreated = true;
    }
    void GenerateMesh()
    {
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

            //// Create the mesh
            //Mesh msh = GetComponent<MeshFilter>().mesh;
            //msh.vertices = vertices;
            //msh.triangles = indices;
            //msh.RecalculateNormals();
            //msh.RecalculateBounds();

            mesh.Clear();
            mesh.vertices = vertices;
            mesh.triangles = indices;
            mesh.uv = tr.CalculateUV();
            mesh.RecalculateNormals();
            mesh.RecalculateBounds();
            mat.mainTextureScale = tr.CalculateScale(1f);

            //// Create UV maps for mesh
            //Vector2[] uvs = new Vector2[vertices.Length];
            //Bounds bounds = msh.bounds;
            //int j = 0;
            //while (j < uvs.Length)
            //{
            //    uvs[j] = new Vector2(vertices[j].x / bounds.size.x, vertices[j].z / bounds.size.z);
            //    j++;
            //}
            //msh.uv = uvs;

            //rend.material.mainTextureScale = new Vector2(bounds.size.x, bounds.size.z);
        }
    }
    void DestroyPoints()
    {
        for (int i = 0; i < Points.Count; i++)
        {
            Destroy(Points[i]);
        }
    }
}



