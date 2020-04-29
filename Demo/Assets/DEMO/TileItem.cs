using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TileInfo
{
    public Texture texture;
    public int xxx;
}

public class TileItem : MonoBehaviour
{
    public TileInfo tileInfo;
    public UITexture texture;

    public void SetInit(Texture _texture)
    {
        if (tileInfo == null)
            tileInfo = new TileInfo();

        tileInfo.texture = _texture;
        texture.mainTexture = _texture;
    }

    public void Invoke_Select()
    {
        Ui_ItemScrollView.ins.SetSelectTileData(this);
    }
}
