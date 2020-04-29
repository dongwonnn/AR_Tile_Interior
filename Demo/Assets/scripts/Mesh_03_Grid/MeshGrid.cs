using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
//[ExecuteInEditMode]
public class MeshGrid : MonoBehaviour
{
	public int xSize, ySize;
	Vector3[] vertices = new Vector3[0];
	MeshFilter meshFilter;
	MeshRenderer meshRenderer;
	Mesh mesh;
	Material mat;


    void Start()
	{

	}

    // Update is called once per frame
 //   void Update()
	//{
	//	Generate();
	//}

	void Generate()
	{
		meshFilter	= GetComponent<MeshFilter>();
		meshRenderer= GetComponent<MeshRenderer>();
		mesh		= new Mesh();
		meshFilter.mesh = mesh;

		//name...
		mesh.name = "Procedural Grid";

		//matrix
		vertices = new Vector3[(xSize + 1) * (ySize + 1)];
		for(int i = 0, y = 0; y < ySize; y++)
		{
			for(int x = 0; x < xSize; x++, i++)
			{ 
				vertices[i] = new Vector3(x, y);

			}
		}

		//triangle
		int[] triangles = new int[] {
			//0, xSize + 1, 1,
			//1, xSize + 1, xSize + 2

			//0, xSize + 1, 1,
			//1, xSize + 1, xSize + 2

			0, xSize, 1,
			1, xSize, xSize + 1
		};
		triangles[0] = 0;
		triangles[1] = triangles[4] = xSize;
		triangles[2] = triangles[3] = 1;
					   triangles[5] = xSize + 1;



		mesh.vertices = vertices;
		mesh.triangles = triangles;
	}

	private void OnDrawGizmos()
	{
		if (vertices == null) return;

		Generate();

		Gizmos.color = Color.black;
		for(int i = 0; i < vertices.Length; i++)
		{
			Gizmos.DrawSphere(vertices[i], 0.1f);
		}
	}
}
