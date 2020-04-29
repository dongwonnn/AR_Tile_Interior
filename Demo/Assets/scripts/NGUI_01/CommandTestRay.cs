using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandTestRay : MonoBehaviour
{

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Debug.Log(this + ":" + UICamera.currentCamera);
			Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit _hit;
			if (Physics.Raycast(_ray) && UICamera.currentCamera == Camera.main)
			{
				Debug.Log(this + " Ray -> Update");

			}
		}
	}
}
