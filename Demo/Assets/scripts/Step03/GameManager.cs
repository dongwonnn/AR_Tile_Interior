using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Step03
{

	public class GameManager : MonoBehaviour
	{
		public static GameManager ins;
		public UserData userData;
		private void Awake()
		{
			ins = this;
		}

		#region item buy ....
		//....검사후 팝업호출...
		public void CheckMoneyToItemBuyPopup()
		{
			if (userData.coin >= 100)
			{
				Ui_ItemBuyPopup.ins.ShowPopup();
			}
			else
			{
				Ui_Alert.ins.ShowPopup();
			}
		}

		public void ItemBuy()
		{
			if (userData.coin >= 100)
			{
				userData.AddItem(new Item(), 100);
			}
		}

		#endregion
	}
}
