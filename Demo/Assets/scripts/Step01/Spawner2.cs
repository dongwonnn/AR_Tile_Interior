using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner2 : MonoBehaviour
{
	public List<int> listData = new List<int>();

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
				_go.name = _go.name + listData[i];

				list.Add(_go);
			}

		}

		if (Input.GetMouseButtonDown(1))
		{
			//StopAllCoroutines();
			//if(coListDelete != null)
			//	StopCoroutine(coListDelete);

			//coListDelete = StartCoroutine(Co_Delete());

			StopCoroutine("Co_Delete");
			StartCoroutine("Co_Delete");
		}
	}

	IEnumerator Co_Delete()
	{
		while (list.Count >= 1)
		{
			GameObject _go = list[0];
			list.RemoveAt(0);
			Destroy(_go);
			yield return new WaitForSeconds(0.1f);
			//yield return null;
		}
	}
}
