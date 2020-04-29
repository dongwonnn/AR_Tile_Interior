using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ui_XXX1 : MonoBehaviour
{
	public void Invoke_ClickeSample()
	{
		//Debug.Log(" >>>>>");
	}

	public GameObject target;
	public void Invoke_ScalePlus()
	{
		target.transform.localScale *= 1.1f;
	}

	public void Invoke_ScaleMinus()
	{
		target.transform.localScale *= 0.9f;
	}
}
