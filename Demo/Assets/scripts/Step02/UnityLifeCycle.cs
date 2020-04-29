using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityLifeCycle : MonoBehaviour
{
	public Rigidbody rg;
	private void Awake()
	{
		if (rg == null)
			rg = GetComponent<Rigidbody>();

		Debug.Log(this + " Awake");
	}

	private void OnEnable()
	{
		Debug.Log(this + " OnEnable");
	}

	// Start is called before the first frame update
	void Start()
    {
		Debug.Log(this + " Start");
    }

	private void FixedUpdate()
	{
		Debug.Log(this + " FixedUpdate" + Time.deltaTime);
	}

	// Update is called once per frame
	void Update()
	{
		if(rg != null)
			rg.AddForce(Vector3.up);
		Debug.Log(this + " Update" + Time.deltaTime);

	}
	private void LateUpdate()
	{
		Debug.Log(this + " LateUpdate" + Time.deltaTime);

	}
}
