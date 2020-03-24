using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item88 : MonoBehaviour
{
	public UITexture sprite;
	//public string spriteName;
	public void SetInit(Texture _sprite)
	{
		sprite.mainTexture = _sprite;
	}


	public void Inoke_SelectThis()
	{
		Ui_ItemList88.ins.Invoke_SelectItem(this);
	}
}
