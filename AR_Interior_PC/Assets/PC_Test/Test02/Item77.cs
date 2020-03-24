using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item77 : MonoBehaviour
{
	public UISprite sprite;
	public string spriteName;
	public void SetInit(string _spriteName)
	{
		spriteName = _spriteName;
		gameObject.name += "_"+_spriteName;
		sprite.spriteName = _spriteName;
	}


	public void Inoke_SelectThis()
	{
		Ui_ItemList.ins.Invoke_SelectItem(this);
	}
}
