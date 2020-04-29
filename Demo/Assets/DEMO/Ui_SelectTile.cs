using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore.Examples.HelloAR;

public class Ui_SelectTile : MonoBehaviour
{
    bool showCheck = false;
    public static Ui_SelectTile ins;
    [HideInInspector] public TileItem tileItem;
    [SerializeField] UITexture uiTexture;

    // 싱글톤
    private void Awake()
    {
        ins = this;
    }

    public void SetSelectTileData(TileItem _tileItem)
    {
        tileItem = _tileItem;
        uiTexture.mainTexture = tileItem.tileInfo.texture;
        HelloARController2.ins.SetMaterial(tileItem.tileInfo.texture);
        //Debug.Log(tileItem.tileInfo.xxx);

        gameObject.SetActive(true);
    }

    public void Invoke_Show_ItemScrollView()
    {
        if (showCheck == false)
        {
            Ui_ItemScrollView.ins.gameObject.SetActive(true);
            Ui_ItemScrollView.ins.InitData();
            showCheck = true;
            Debug.Log("show");
        }

        else if (showCheck == true)
        {
            Ui_ItemScrollView.ins.gameObject.SetActive(false);
            showCheck = false;
            Debug.Log("hide");
        }
    }
}
