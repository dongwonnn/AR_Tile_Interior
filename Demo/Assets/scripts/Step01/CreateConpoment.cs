using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateConpoment : MonoBehaviour
{
	public MeshFilter meshFilter;
	public MeshRenderer meshRenderer;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			meshFilter = gameObject.AddComponent<MeshFilter>();
		}

		if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			meshRenderer = gameObject.AddComponent<MeshRenderer>();
		}
	}
}
