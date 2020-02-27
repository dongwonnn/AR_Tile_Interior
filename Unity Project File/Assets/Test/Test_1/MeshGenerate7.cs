using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class MeshGenerate7 : MonoBehaviour
{
	public GameObject prefab;
	public Material mat;
	public GameObject goCenter;
	Camera camera;
	Plane ground;
	List<Transform> list = new List<Transform>();
	MeshFilter meshFilter;
	MeshRenderer meshRenderer;
	Mesh mesh;
	bool bCalculate;


	void Start()
	{
		camera = Camera.main;
		meshFilter = GetComponent<MeshFilter>();
		meshRenderer = GetComponent<MeshRenderer>();
		meshRenderer.material = mat;

		mesh = new Mesh();
		mesh.name = "PolygonTest";

		meshFilter.mesh = mesh;

		ground = new Plane(-Vector3.forward, Vector3.zero);
		//ground = new Plane(Vector3.up, Vector3.zero);
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Ray _ray = camera.ScreenPointToRay(Input.mousePosition);
			//Debug.DrawRay(_ray.origin, _ray.direction, Color.red);
		
			//RaycastHit _hit;
			float _distance;
			if (ground.Raycast(_ray, out _distance))
			{
				Debug.Log(_distance);
				Vector3 _hitPoint = _ray.GetPoint(_distance);

				Debug.Log(_hitPoint);
				GameObject _go = Instantiate(prefab, _hitPoint, Quaternion.identity);

				list.Add(_go.transform);
				_go.transform.SetParent(transform);
				bCalculate = true;
			}
		}

		if (Input.GetMouseButtonDown(1))
		{
			mesh.Clear();
		}

		if (bCalculate && Input.GetKeyDown(KeyCode.Space) && list.Count >=3)
		{
			bCalculate = false;
			CalculateListObject();
			ClearListObject();
		}

	}

	void CalculateListObject() {

		if(list.Count == 4)
			Triangulator.ReCalculatePosition(list);

		Vector2[] _vertices2D = new Vector2[list.Count];
		Vector3 _pos;

		for (int i = 0; i < list.Count; i++) {
			_pos = list[i].position;
			_vertices2D[i] = new Vector2(_pos.x, _pos.y);
		}

		Triangulator _tr = new Triangulator(_vertices2D);
		int[] _triangles = _tr.Triangulate();

		Vector3[] _vertices = new Vector3[_vertices2D.Length];

		for (int i = 0; i < _vertices.Length; i++) {
			_vertices[i] = new Vector3(_vertices2D[i].x, _vertices2D[i].y, 0);
		}

		mesh.Clear();
		mesh.vertices = _vertices;
		mesh.triangles = _triangles;
		mesh.uv = _tr.CalculateUV();
		mesh.RecalculateNormals();
		mesh.RecalculateBounds();

		mat.mainTextureScale = _tr.CalculateScale(1f);

	}
	void ClearListObject() {
		for (int i = 0; i < list.Count; i++) {
			DestroyImmediate(list[i].gameObject);
		}
		list.Clear();
	}
}