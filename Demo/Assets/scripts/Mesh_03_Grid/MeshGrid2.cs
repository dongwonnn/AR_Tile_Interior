using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
//[ExecuteInEditMode]
public class MeshGrid2 : MonoBehaviour
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
		for(int i = 0, y = 0; y <= ySize; y++)
		{
			for(int x = 0; x <= xSize; x++, i++)
			{ 
				vertices[i] = new Vector3(x, y);
			}
		}
		mesh.vertices = vertices;

		//triangle
		//int[] triangles = new int[] {
		//	0, xSize + 1, 1,
		//	1, xSize + 1, xSize + 2
		//};
		//triangles[0] = 0;
		//triangles[1] = triangles[4] = xSize + 1;
		//triangles[2] = triangles[3] = 1;
		//triangles[5] = xSize + 2;

		int[] triangles = new int[xSize * 6 * ySize];
		for (int ti = 0, vi = 0, y = 0; y < ySize; y++, vi++)
		{
			for (int x = 0; x < xSize; x++, ti += 6, vi++)
			{
				triangles[ti + 0] = vi;
				triangles[ti + 1] = triangles[ti + 4] = vi + xSize + 1;
				triangles[ti + 2] = triangles[ti + 3] = vi + 1;
				triangles[ti + 5] = vi + xSize + 2;
			}
		}
		mesh.triangles = triangles;

		mesh.RecalculateNormals();
	}

	private void OnDrawGizmos()
	{
		if (vertices == null) return;

		Generate();

		Gizmos.color = Color.black;
		for(int i = 0; i < vertices.Length; i++)
		{
			Gizmos.DrawSphere(transform.position + vertices[i], 0.1f);
		}
	}
}
