using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Step03
{
	[System.Serializable]
	public class Item
	{
		public int itemcode;
	}

	public class UserData : MonoBehaviour
	{
		public int coin;

		public List<Item> listOwners = new List<Item>();

		public void AddItem(Item _item, int _price)
		{
			coin -= _price;
			listOwners.Add(_item);
		}
	}
}
