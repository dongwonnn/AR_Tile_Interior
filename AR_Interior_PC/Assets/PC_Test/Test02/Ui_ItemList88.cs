using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ui_ItemList88 : MonoBehaviour
{
	public static Ui_ItemList88 ins;
	public GameObject prefabItem;
	public UIGrid grid;
	public UIScrollView scrollview;
	public List<Texture> listItemTexture = new List<Texture>();
	public List<Item88> listItem = new List<Item88>();


	private void Awake()
	{
		ins = this;
	}

	private void Start()
	{
		for(int i = 0; i < listItemTexture.Count; i++)
		{
			//Instantiate(prefabItem, gridTrans.position, gridTrans.rotation);
			GameObject _go = NGUITools.AddChild(grid.gameObject, prefabItem);
			Item88 _scp = _go.GetComponent<Item88>();
			_scp.SetInit(listItemTexture[i]);

			listItem.Add(_scp);
		}
		grid.Reposition();
		scrollview.ResetPosition();
	}


	public void Invoke_SelectItem(Item88 _scp)
	{
		Ui_SelectItem88.ins.SetItem(_scp);

	}



}
