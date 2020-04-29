using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Step03
{
	public class Ui_ItemBuyPopup : MonoBehaviour
	{
		public static Ui_ItemBuyPopup ins;
		[SerializeField] GameObject body;
		private void Awake()
		{
			ins = this;
			if (body == null)
				body = transform.GetChild(0).gameObject;
			body.SetActive(false);
		}

		public void ShowPopup()
		{
			body.SetActive(true);
		}

		public void Invoke_BtnBuy()
		{
			body.SetActive(false);
			GameManager.ins.ItemBuy();
		}

		public void Invoke_BtnCancel()
		{
			body.SetActive(false);

		}
	}
}