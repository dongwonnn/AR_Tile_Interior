using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptB : MonoBehaviour
{
	Material material;
	ScriptManager scriptManager;
	private void Start()
	{
		material = GetComponent<Renderer>().material;

		GameObject _go = GameObject.Find("ScriptManager");
		scriptManager = _go.GetComponent<ScriptManager>();
		
	}

	private void OnMouseDown()
	{

		scriptManager.SetMaterial(material);

	}
}
