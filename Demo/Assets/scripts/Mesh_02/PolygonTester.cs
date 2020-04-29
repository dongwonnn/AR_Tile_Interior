using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolygonTester : MonoBehaviour
{
	public enum eMeshType {TMesh, BoxClock, BoxAntiClock, PolygonClock, PolygonAntiClock };
	public eMeshType type;
	Mesh mesh;

	Vector2[] vertices2D;
	void Start()
	{
		mesh		= new Mesh();
		mesh.name	= "MeshName";
		GetComponent<MeshFilter>().mesh = mesh;
		CreateMesh();
	}

	void CreateMesh()
	{
		switch (type)
		{
			case eMeshType.TMesh:
				vertices2D = new Vector2[] {
					  new Vector2(0,0),
					  new Vector2(0,50),
					  new Vector2(50,50),
					  new Vector2(50,100),
					  new Vector2(0,100),
					  new Vector2(0,150),
					  new Vector2(150,150),
					  new Vector2(150,100),
					  new Vector2(100,100),
					  new Vector2(100,50),
					  new Vector2(150,50),
					  new Vector2(150,0),
				  };
				break;
			case eMeshType.BoxClock:
				vertices2D = new Vector2[] {
					  new Vector2(0,0),
					  new Vector2(0,50),
					  new Vector2(50,50),
					  new Vector2(50,0)
				  };
				break;
			case eMeshType.BoxAntiClock:
				vertices2D = new Vector2[] {
					  new Vector2(0,0),
					  new Vector2(50,0),
					  new Vector2(50,50),
					  new Vector2(0,50)
				  };
				break;
			case eMeshType.PolygonClock:
				vertices2D = new Vector2[] {
					  new Vector2(10,0),
					  new Vector2(0,50),
					  new Vector2(50,50),
					  new Vector2(40,0)
				  };
				break;
			case eMeshType.PolygonAntiClock:
				vertices2D = new Vector2[] {
					  new Vector2(0,0),
					  new Vector2(10,50),
					  new Vector2(40,50),
					  new Vector2(50,0)
				  };
				break;
		}
		
		// Use the triangulator to get indices for creating triangles
		Triangulator tr = new Triangulator(vertices2D);
		int[] _triangles = tr.Triangulate();

		// Create the Vector3 vertices
		Vector3[] _vertices = new Vector3[vertices2D.Length];
		for (int i = 0; i < _vertices.Length; i++)
		{
			_vertices[i] = new Vector3(vertices2D[i].x, vertices2D[i].y, 0);
		}

		// Create the mesh
		mesh.Clear();
		mesh.vertices	= _vertices;
		mesh.triangles	= _triangles;
		mesh.uv			= tr.CalculateUV();
		mesh.RecalculateNormals();
		mesh.RecalculateBounds();

		GetComponent<MeshRenderer>().material.mainTextureScale = tr.CalculateScale(10f);

		// Set up game object with mesh;
		//gameObject.AddComponent(typeof(MeshRenderer));
		//MeshFilter filter = gameObject.AddComponent(typeof(MeshFilter)) as MeshFilter;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			CreateMesh();
		}
	}

	private void OnDrawGizmos()
	{
		if (Application.isPlaying)
		{
			for(int i = 0; i < vertices2D.Length; i++)
			{
				Gizmos.DrawWireSphere(vertices2D[i], 1f);
			}
		}
	}

}
