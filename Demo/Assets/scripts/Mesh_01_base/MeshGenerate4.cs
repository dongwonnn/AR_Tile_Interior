using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class MeshGenerate4 : MonoBehaviour
{
    public GameObject prefab;
	public Material mat;
	public GameObject goCenter;
    Camera camera;
    Plane ground;
    List<GameObject> list = new List<GameObject>();
    MeshFilter meshFilter;
    MeshRenderer meshRenderer;
    Mesh mesh;
    bool bCalculate;


    void Start()
    {
        meshFilter      = GetComponent<MeshFilter>();
        meshRenderer    = GetComponent<MeshRenderer>();
        mesh			= new Mesh();
        meshFilter.mesh = mesh;


        camera = Camera.main;
        ground = new Plane(-Vector3.forward, Vector3.zero);
        //ground = new Plane(Vector3.up, Vector3.zero);
    }

    // Update is called once per frame
    void Update()
	{
		if (Input.GetMouseButtonDown(0))
        {
			Ray _ray = camera.ScreenPointToRay(Input.mousePosition);
			//Debug.DrawRay(_ray.origin, _ray.direction * 10f, Color.red);
			//RaycastHit _hit;
			float _distance;
            if(ground.Raycast(_ray, out _distance))
            {
				Debug.Log(_distance);
                Vector3 _hitPoint = _ray.GetPoint(_distance);
				Debug.Log(_hitPoint);
                GameObject _go = Instantiate(prefab, _hitPoint, Quaternion.identity);
				

                list.Add(_go);
                _go.transform.SetParent(transform);
                bCalculate = true;
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            for (int i = 0; i < list.Count; i++)
				DestroyImmediate(list[i]);

            if(goCenter)
                DestroyImmediate(goCenter);

            list.Clear();
        }

        if(bCalculate && list.Count == 4)
        {
            bCalculate = false;

            //Calculate Center....
            Vector3 _sum = Vector3.zero;
            for(int i = 0; i < list.Count; i++)
            {
                _sum += list[i].transform.position;
            }
            Vector3 _center = _sum / list.Count;


            if (goCenter != null)
                DestroyImmediate(goCenter);
            goCenter = Instantiate(prefab, _center, Quaternion.identity);

            //Camera 각에서 보는 좌표계...
            CreateMesh();
        }

    }
    void CreateMesh()
    {
        //verties, triangle
        Vector3[] _vertices = new Vector3[]
        {
            list[0].transform.position,
            list[1].transform.position,
            list[2].transform.position,
            list[3].transform.position
        };

        int[]  _triangles = new int[]
        {
			0, 1, 2,
			2, 1, 3
			//2, 1, 0,
			//3, 1, 2
        };

        Vector2[]  _uvs = new Vector2[]
        {
            new Vector2(0, 1),
            new Vector2(1, 1),
            new Vector2(0, 0),
            new Vector2(1, 0)
            
            //new Vector2(0, 0),
            //new Vector2(0, 1),
            //new Vector2(1, 1),
            //new Vector2(1, 0)
        };

        // 
        mesh.Clear();
        mesh.vertices   = _vertices;
        mesh.triangles  = _triangles;
        mesh.uv         = _uvs;
        mesh.RecalculateNormals();

        GetComponent<Renderer>().material = mat;
        // mesh.RecalculateBounds();
        // mesh.RecalculateTangents();
    }
}
