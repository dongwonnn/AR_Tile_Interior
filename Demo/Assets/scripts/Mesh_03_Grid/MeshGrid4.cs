using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
//[ExecuteInEditMode]
public class MeshGrid4 : MonoBehaviour
{
	public int xSize, ySize;
	public Material mat;
	Vector3[] vertices = new Vector3[0];
	Vector2[] uv = new Vector2[0];
	Vector4[] tangents = new Vector4[0];
	int[] triangles = new int[0];
	MeshFilter meshFilter;
	MeshRenderer meshRenderer;
	Mesh mesh;


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

		//vertices, uv, tangent
		vertices	= new Vector3[(xSize + 1) * (ySize + 1)];
		uv			= new Vector2[vertices.Length];
		tangents	= new Vector4[vertices.Length];
		Vector4 _tangent = new Vector4(1f, 0f, 0f, -1f);
		for (int i = 0, y = 0; y <= ySize; y++)
		{
			for(int x = 0; x <= xSize; x++, i++)
			{ 
				vertices[i] = new Vector3(x, y);
				uv[i] = new Vector2((float)x / xSize, (float)y / ySize);
				//Debug.Log(i + " > " + uv[i]);
				tangents[i] = _tangent;
			}
		}
		mesh.vertices = vertices;
		mesh.uv = uv;
		mesh.tangents = tangents;

		//triangle
		//int[] triangles = new int[] {
		//	0, xSize + 1, 1,
		//	1, xSize + 1, xSize + 2
		//};
		//triangles[0] = 0;
		//triangles[1] = triangles[4] = xSize + 1;
		//triangles[2] = triangles[3] = 1;
		//triangles[5] = xSize + 2;

		triangles = new int[xSize * 6 * ySize];
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
		meshRenderer.material = mat;
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
