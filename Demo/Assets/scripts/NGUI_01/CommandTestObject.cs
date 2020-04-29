using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandTestObject : MonoBehaviour
{

	private void OnMouseDown()
	{

		Debug.Log(this + ":" + UICamera.currentCamera);
		Debug.Log(this + "OnMouseDown");
	}
}
