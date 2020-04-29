using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ui_ItemScrollView : MonoBehaviour
{
    public static Ui_ItemScrollView ins;
    public GameObject goSelectTile;
    private void Awake()
    {
        ins = this;
        gameObject.SetActive(false);
    }

    [SerializeField] List<Texture> listTexture = new List<Texture>();
    //[SerializeField] Transform body;
    [SerializeField] UIScrollView scrollview;
    [SerializeField] UIGrid grid;
    [SerializeField] TileItem prefabTileItem;
    List<TileItem> list_TileItme = new List<TileItem>();

    public void InitData()
    {
        TileItem _scp;

        int imax = listTexture.Count;
        for (int i = 0; i < imax; i++)
        {
            GameObject _go = NGUITools.AddChild(grid.gameObject, prefabTileItem.gameObject);
            _scp = _go.GetComponent<TileItem>();
            _scp.SetInit(listTexture[i]);
        }

        DestroyImmediate(prefabTileItem.gameObject);
        scrollview.ResetPosition();
        grid.Reposition();
    }

    //   public void Invoke_ShowBtn()
    //   {
    //       goSelectTile.SetActive(true);
    //       gameObject.SetActive(false);
    //   }

    //   public void Invoke_Hide()
    //{
    //	gameObject.SetActive(false);
    //}

    public void SetSelectTileData(TileItem _scp)
    {
        Ui_SelectTile.ins.SetSelectTileData(_scp);
    }

}
