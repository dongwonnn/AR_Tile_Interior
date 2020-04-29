using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptA : MonoBehaviour
{

	private void OnMouseDown()
	{
		GameObject _go = GameObject.Find("ScriptManager");
		ScriptManager _scp = _go.GetComponent<ScriptManager>();
		_scp.SetMaterial(GetComponent<Renderer>().material);
		
	}
}
