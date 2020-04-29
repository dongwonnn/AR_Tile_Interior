using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolygonTester3 : MonoBehaviour
{
    List<Transform> list = new List<Transform>();
    public GameObject prefab;
	public Material material;
    MeshRenderer meshRenderer;
	MeshFilter meshFilter;
	Mesh mesh;
	Camera camera;
	bool bCalculate;
	Plane ground;


    void Start()
    {
		camera			= Camera.main;
		meshRenderer	= GetComponent<MeshRenderer>();
		meshFilter		= GetComponent<MeshFilter>();
		meshRenderer.material = material;

		mesh		= new Mesh();
		mesh.name	= "PolygonTest";
		meshFilter.mesh = mesh;

		ground = new Plane(-camera.transform.forward, Vector3.zero);
	}

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
			float _distance;
            Ray _ray = camera.ScreenPointToRay(Input.mousePosition);
            if (ground.Raycast(_ray, out _distance))
            {
                GameObject _go = Instantiate(prefab, _ray.GetPoint(_distance), Quaternion.identity);
                list.Add(_go.transform);
				bCalculate = true;
				_go.transform.SetParent(transform);
			}
        }


		if (bCalculate && Input.GetKeyDown(KeyCode.Space) && list.Count >= 3)
        {
			bCalculate = false;
			CalculateListObject();
			ClearListObject();
		}
    }

	void CalculateListObject()
	{
		//list relist
		if(list.Count == 4)
			Triangulator.ReCalcuatePosition(list);


		//3D -> 2D position recalculate
		Vector2[] _vertices2D = new Vector2[list.Count];
		Vector3 _pos;

		for (int i = 0; i < list.Count; i++)
		{
			_pos = list[i].position;
			_vertices2D[i] = new Vector2(_pos.x, _pos.y);
		}

		Triangulator _tr = new Triangulator(_vertices2D);
		int[] _triangles = _tr.Triangulate();

		Vector3[] _vertices = new Vector3[_vertices2D.Length];
		for (int i = 0; i < _vertices.Length; i++)
		{
			_vertices[i] = new Vector3(_vertices2D[i].x, _vertices2D[i].y, 0);
		}

		// Create the mesh
		mesh.Clear();
		mesh.vertices	= _vertices;
		mesh.triangles	= _triangles;
		mesh.uv			= _tr.CalculateUV();
		mesh.RecalculateNormals();
		mesh.RecalculateBounds();

		//material texture, 
		material.mainTextureScale = _tr.CalculateScale(1f);

	}

	void ClearListObject()
	{
		for (int i = 0; i < list.Count; i++)
			DestroyImmediate(list[i].gameObject);
		list.Clear();
	}


}


