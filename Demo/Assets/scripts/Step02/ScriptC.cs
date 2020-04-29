using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptC : MonoBehaviour
{
	Material material;
	private void Start()
	{
		material = GetComponent<Renderer>().material;
	}

	private void OnMouseDown()
	{
		ScriptManager.ins.SetMaterial(material);

	}
}
