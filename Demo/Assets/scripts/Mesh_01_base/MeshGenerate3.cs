using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshGenerate3 : MonoBehaviour
{
    Mesh mesh;
    Vector3[] vertices;
    int[] triangles;
    Vector2[] uvs;
    [SerializeField] Material mat;
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        CreateMesh();
    }

    void CreateMesh()
    {
        //verties, triangle
        vertices = new Vector3[]
        {
            new Vector3(0, 0, 0),
            new Vector3(0, 1, 0),
            new Vector3(1, 1, 0),
            new Vector3(1, 0, 0)
        };

        triangles = new int[]
        {
            0, 1, 2,
            0, 2, 3
            //3, 0, 2
            //2, 3, 0
        };

        uvs = new Vector2[]
        {
            
            
            //new Vector2(0, 0),
            //new Vector2(0, 1),
            //new Vector2(1, 1),
            //new Vector2(1, 0)
        };

        // 
        mesh.Clear();
        mesh.vertices   = vertices;
        mesh.triangles  = triangles;
        mesh.uv         = uvs;
        mesh.RecalculateNormals();

		
        GetComponent<Renderer>().material = mat;
        // mesh.RecalculateBounds();
        // mesh.RecalculateTangents();
    }

}
