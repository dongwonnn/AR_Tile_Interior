using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Step03
{
	public class Ui_Alert : MonoBehaviour
	{
		public static Ui_Alert ins;
		[SerializeField]GameObject body;
		private void Awake()
		{
			ins = this;
		}

		public void ShowPopup()
		{
			body.SetActive(true);
		}

		public void Inovke_OK()
		{
			body.SetActive(false);

		}
	}
}
