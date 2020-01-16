using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolygonTesterZ : MonoBehaviour
{
    public List<GameObject> Points = new List<GameObject>();
    public List<Vector3> positions = new List<Vector3>();
    public GameObject GameObjectPointPrefab;
    Renderer rend;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("MouseLeft");
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("The ray hit at: " + hit.point);
                var ob = Instantiate(GameObjectPointPrefab, hit.point, Quaternion.identity);
                Points.Add(ob);
                positions.Add(ob.transform.position);
            }
        }

        if (Points.Count == 4)
        {
            //msh.Clear();
            Vector3[] vert = new Vector3[positions.Count];

            for (int i = 0; i < Points.Count; i++)
            {
                vert[i].x = positions[i].x;
                vert[i].y = 0;
                vert[i].z = positions[i].z;
            }

            TriangulatorZ tr = new TriangulatorZ(vert);
            int[] indices = tr.Triangulate();

            Vector3[] vertices = new Vector3[vert.Length];
            for (int i = 0; i < vertices.Length; i++)
            {
                vertices[i] = new Vector3(vert[i].x, 0, vert[i].z);
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

            float sizeX = 100f;
            float sizeZ = 100f;

            //rend.material : 
            Texture tx = rend.material.GetTexture("_MainTex");

            float textureToMeshX = ((float)tx.width / tx.height) * 2f;          // width: 넓이, height: 높이

            rend.material.mainTextureScale = new Vector2(bounds.size.x / sizeX, bounds.size.z / sizeZ);

            Points.Clear();
            positions.Clear();
        }
    }
}


