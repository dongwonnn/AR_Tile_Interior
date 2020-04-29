using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandTestUI : MonoBehaviour
{
	public void Invoke_Click()
	{
		Debug.Log(this + ":" + UICamera.currentCamera);
		Debug.Log(this + "click");
	}


}
