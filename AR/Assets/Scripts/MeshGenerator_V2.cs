using System.Collections;
using System.Collections.Generic;
using GoogleARCore;
using GoogleARCore.Examples.Common;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MeshGenerator_V2 : MonoBehaviour
{
    public Material mat;

    public Camera FirstPersonCamera;

    public GameObject GameObjectPointPrefab;

    private const float k_PrefabRotation = 180.0f;

    private List<GameObject> Points = new List<GameObject>();
    private List<Vector3> positions = new List<Vector3>();

    public Mesh mesh;
    MeshFilter meshFilter;
    MeshRenderer meshRenderer;
    LineRenderer lineRenderer;

    public Vector3[] vertsForTextureChange;

    public Vector2[] uvsForChange;

    bool isCreated;

    float degree = 5f;
    float degreecount = 0f;

    Vector2[] originaluvs;
    Vector2[] rotateduvs;

    // Start is called before the first frame update
    void Start()
    {
        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();
        lineRenderer = GetComponent<LineRenderer>();
        meshRenderer.material = mat;

        mesh = new Mesh();
        meshFilter.mesh = mesh;

        isCreated = false;
    }

    // Update is called once per frame
    void Update()
    {
        // If the player has not touched the screen, we are done with this update.
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
                Debug.Log(touch.position.x);
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
                }
            }
        }
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

            Triangulator tr = new Triangulator(vert);
            int[] indices = tr.Triangulate();

            Vector3[] vertices = new Vector3[vert.Length];
            for (int i = 0; i < vertices.Length; i++)
            {
                vertices[i] = new Vector3(vert[i].x, vert[i].y, vert[i].z);
            }
            vertsForTextureChange = vertices;

            // Create the mesh
            mesh.Clear();
            mesh.vertices = vertices;
            mesh.triangles = indices;
            //mesh.uv = tr.CalculateUV();
            //mesh.RecalculateNormals();
            //mesh.RecalculateBounds();
            //mat.mainTextureScale = tr.CalculateScale(1f);

            // Create UV maps for mesh
            Vector2[] uvs = new Vector2[vertices.Length];
            Bounds bounds = mesh.bounds;
            int j = 0;


            while (j < uvs.Length)
            {
                uvs[j] = new Vector2(vertices[j].x / bounds.size.x, vertices[j].z / bounds.size.z);
                j++;
            }
            originaluvs = uvs;
            rotateduvs = uvs;

            mesh.uv = uvs;

            // Set Texture Size
            float sizeX = 100f;
            float sizeZ = 100f;

            mesh.RecalculateNormals();
            mesh.RecalculateBounds();

            meshRenderer.material.mainTextureScale = new Vector2(bounds.size.x / sizeX, bounds.size.z / sizeZ);
        }
    }
    void DestroyPoints()
    {
        for (int i = 0; i < Points.Count; i++)
        {
            Destroy(Points[i]);
        }
    }
    public void ClickedGenerateButton()
    {
        GenerateMesh();
        DestroyPoints();
        Destroy(lineRenderer);
        isCreated = true;
    }

    public void RotateTexture()
    {
        Bounds bounds = mesh.bounds;

        for (int i = 0; i < rotateduvs.Length; i++)
        {
            if (degreecount <= 4.5)
                rotateduvs[i] = 
                Quaternion.AngleAxis(degree, Vector3.left) *
                Quaternion.AngleAxis(degree, Vector3.forward) * rotateduvs[i];
            else
            {
                rotateduvs[i] =
                Quaternion.AngleAxis(degree, Vector3.right) *
                Quaternion.AngleAxis(degree, Vector3.forward) * rotateduvs[i];
            }
        }
        mesh.uv = rotateduvs;
        degreecount += 0.5f;
        if (degreecount == 9) degreecount = 0;

        Debug.Log(degreecount);
    }
}



