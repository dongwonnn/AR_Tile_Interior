using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetComponent : MonoBehaviour
{
	public MeshFilter meshFilter;
	public MeshRenderer meshRenderer;
	public Rigidbody rigidbody;

	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
	{
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			meshFilter = gameObject.GetComponent<MeshFilter>();
		}

		if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			meshRenderer = gameObject.GetComponent<MeshRenderer>();
		}

		if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			rigidbody = gameObject.GetComponent<Rigidbody>();
		}
	}
}
