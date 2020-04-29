using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner3 : MonoBehaviour
{
	public List<Texture> listData = new List<Texture>();

	public GameObject prefabCube;
	//GameObject[] list;
	public List<GameObject> list = new List<GameObject>();
	Coroutine coListDelete;
	

	private void Start()
	{
		//gameObject;
		//transform;
	}

	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{

			for (int i = 0; i < listData.Count; i++)
			{
				GameObject _go = Instantiate(prefabCube, Random.insideUnitSphere * 5f, Quaternion.identity);

				_go.transform.SetParent(transform);
				_go.name = _go.name + listData[i].ToString();

				list.Add(_go);

				MeshRenderer _meshRenderer = _go.GetComponent<MeshRenderer>();
				Material _mat = _meshRenderer.material;
				_mat.mainTexture = listData[i];
			}
		}
	}

}
