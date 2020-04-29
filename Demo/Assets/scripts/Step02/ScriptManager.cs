using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptManager : MonoBehaviour
{
	Renderer renderer;
	public static ScriptManager ins;

	private void Awake()
	{
		ins = this;
	}

	void Start()
    {
		renderer = GetComponent<Renderer>();
	}


	public void SetMaterial(Material _mat)
	{
		renderer.material = _mat;
	}
}
