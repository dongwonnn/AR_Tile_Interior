using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ui_ItemList : MonoBehaviour
{
	public static Ui_ItemList ins;
	public GameObject prefabItem;
	public UIGrid grid;
	public UIScrollView scrollview;
	public List<string> listItemName = new List<string>();
	public List<Item77> listItem = new List<Item77>();


	private void Awake()
	{
		ins = this;
	}

	private void Start()
	{
		for(int i = 0; i < listItemName.Count; i++)
		{
			//Instantiate(prefabItem, gridTrans.position, gridTrans.rotation);
			GameObject _go = NGUITools.AddChild(grid.gameObject, prefabItem);
			Item77 _scp = _go.GetComponent<Item77>();
			_scp.SetInit(listItemName[i]);

			listItem.Add(_scp);
		}
		grid.Reposition();
		scrollview.ResetPosition();
	}


	public void Invoke_SelectItem(Item77 _scp)
	{
		Ui_SelectItem.ins.SetItem(_scp);

	}



}
