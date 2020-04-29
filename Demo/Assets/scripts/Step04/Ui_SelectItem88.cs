using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ui_SelectItem88 : MonoBehaviour
{
	public static Ui_SelectItem88 ins;
	public UITexture sprite;

	private void Awake()
	{
		ins = this;
	}


	public void SetItem(Item88 _scp)
	{
		sprite.mainTexture = _scp.sprite.mainTexture;
	}
}
