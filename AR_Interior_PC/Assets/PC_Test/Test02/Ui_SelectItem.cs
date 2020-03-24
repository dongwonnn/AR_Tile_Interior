using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ui_SelectItem : MonoBehaviour
{
	public static Ui_SelectItem ins;
	public UISprite sprite;

	private void Awake()
	{
		ins = this;
	}


	public void SetItem(Item77 _scp)
	{
		Debug.Log(1 + ":" + _scp);
		sprite.spriteName = _scp.spriteName;
	}
}
