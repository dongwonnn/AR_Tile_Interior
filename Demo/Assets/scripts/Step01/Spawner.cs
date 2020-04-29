using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
	public GameObject prefabCube;
	//GameObject[] list;
	public List<GameObject> list;
	

	private void Start()
	{
		//gameObject;
		//transform;
	}

	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			GameObject _go = Instantiate(prefabCube, Random.insideUnitSphere * 5f, Quaternion.identity);

			_go.transform.SetParent(transform);
			_go.name = _go.name + (list.Count + 1);

			list.Add(_go);
		}

		if (Input.GetMouseButtonDown(1))
		{
			if(list.Count >= 1)
			{
				//GameObject _go = list.Count;
				int _idx = list.Count - 1;
				GameObject _go = list[_idx];
				list.RemoveAt(_idx);
				Destroy(_go);
			}
		}
	}
}
