using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class MeshGenerate8 : MonoBehaviour
{
	public LineRenderer line;
	public GameObject prefab;
	public Material mat;
	public GameObject goCenter;
	Camera camera;
	Plane ground;
	List<Transform> list = new List<Transform>();
	MeshFilter meshFilter;
	MeshRenderer meshRenderer;
	Mesh mesh;
	bool bCalculate, bLine;


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

		if (line == null) {
			line = GetComponent<LineRenderer>();
		}
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Debug.Log("Left Mouse Button On");
			Ray _ray = camera.ScreenPointToRay(Input.mousePosition);
			//Debug.DrawRay(_ray.origin, _ray.direction, Color.red);
		
			//RaycastHit _hit;
			float _distance;
			if (ground.Raycast(_ray, out _distance))
			{
				//Debug.Log(_distance);
				Vector3 _hitPoint = _ray.GetPoint(_distance);

				Debug.Log(_hitPoint);
				GameObject _go = Instantiate(prefab, _hitPoint, Quaternion.identity);

				list.Add(_go.transform);
				_go.transform.SetParent(transform);
				bCalculate = true;
				bLine = true;
			}
		}

		//if(클릭 && 점이 2개 이상){
		//	라인그리기
		//}

		if (bLine && list.Count >= 2) {
			bLine = false;
			int _count = list.Count;

			line.SetVertexCount(_count);
			line.SetWidth(0.1f, 0.1f);
			//

			for (int i = 0; i < _count; i++)
			{
				line.SetPosition(i, list[i].position);
			}
		}

		if (bCalculate && Input.GetKeyDown(KeyCode.Space) && list.Count >=3)
		{
			bCalculate = false;
			CalculateListObject();
			ClearListObject();
		}

		if (Input.GetMouseButtonDown(1)) {
			Debug.Log("Right Mouse Button On");
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

		// Create Mesh
		mesh.Clear();
		mesh.vertices = _vertices;
		mesh.triangles = _triangles;
		mesh.uv = _tr.CalculateUV();
		mesh.RecalculateNormals();
		mesh.RecalculateBounds();
		// mesh.RecalculateTangents();

		mat.mainTextureScale = _tr.CalculateScale(1f);

	}

	void ClearListObject() {
		for (int i = 0; i < list.Count; i++) {
			DestroyImmediate(list[i].gameObject);
		}
		list.Clear();

		//mesh.Clear();

	}
}