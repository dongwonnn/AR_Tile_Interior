using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]

public class MeshGenerate1_XY : MonoBehaviour
{
    Mesh mesh;
	public Material mat;
    Vector3[] vertices;
    int[] triangles;
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
		GetComponent<MeshRenderer>().material = mat;

        CreateMesh();
    }

    void CreateMesh()
    {
		//verties, triangle
		vertices = new Vector3[]
        {
            new Vector3(0, 0, 0),
            new Vector3(0, 2, 0),
            new Vector3(2, 2, 0),
			new Vector3(2, 0, 0)
        };

        triangles = new int[]
        {
            0, 1, 2,     //ccr
			2, 3, 0
            //2, 3, 0
        };

        // 
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
       // mesh.RecalculateBounds();
       // mesh.RecalculateTangents();
    }

}
