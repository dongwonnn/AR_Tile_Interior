using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Step03
{
	public class Ui_TopRight : MonoBehaviour
	{

		public static Ui_TopRight ins;
		private void Awake()
		{
			ins = this;
		}

		public void Invoke_OpenItemBuyPopup()
		{
			GameManager.ins.CheckMoneyToItemBuyPopup();
		}

	}
}
