using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextureButton : MonoBehaviour
{
    public int buttonIndex = 0;

    GameObject assetBundle;
    GameObject meshGenerator;
    GetAssetBundle asset;

    void Start()
    {
        meshGenerator = GameObject.Find("Generator").transform.Find("MeshGenerator").gameObject;
        assetBundle = GameObject.Find("AssetBundleManager");
        asset = assetBundle.GetComponent<GetAssetBundle>();
    }

    public void onClickedButton()
    {
        meshGenerator.GetComponent<MeshRenderer>().material = asset.mats[buttonIndex];
    }
}
