using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.EventSystems;
//using UnityEngine.UI;
//using UnityScript.Scripting;
using System;
using System.IO;

public class GetAssetBundle : MonoBehaviour
{
    public List<Material> mats = new List<Material>();
    public List<Sprite> sprites = new List<Sprite>();
    public List<Sprite> furniture_sprites = new List<Sprite>();
    public List<GameObject> furniture_models = new List<GameObject>();

    
    // Use this for initialization
    void Start()
    {
        StartCoroutine(LoadAssetBundle());
    }

    IEnumerator LoadAssetBundle()
    {
        string assetBundleName = "tilepack01";
        //string uri1 = "jar:file://" + Application.dataPath + "!/assets/tilepack01";
        string uri1 = "file:///" + Application.dataPath + "/AssetBundles/StandaloneWindows" + assetBundleName;
        //string uri2 = "jar:file://" + Application.dataPath + "!/assets/furniturepack01";

        UnityWebRequest request1 = UnityWebRequestAssetBundle.GetAssetBundle(uri1);
        //UnityWebRequest request2 = UnityWebRequestAssetBundle.GetAssetBundle(uri2);

        yield return request1.SendWebRequest();
        //yield return request2.SendWebRequest();

        AssetBundle bundle1 = DownloadHandlerAssetBundle.GetContent(request1);
        //AssetBundle bundle2 = DownloadHandlerAssetBundle.GetContent(request2);

        for (int i = 2; i <= 9; i++)
        {
            var mat = bundle1.LoadAsset<Material>("M_Tile0" + i);
            mats.Add(mat);
            var sprite = bundle1.LoadAsset<Sprite>("ST_Tile0" + i);
            sprites.Add(sprite);
        }
        //for(int i=1; i<=5; i++)
        //{
        //    var furniture = bundle2.LoadAAsset<GameObject>("F_" + i);
        //    furniture_models.Add(furniture);
        //}
        Debug.Log(mats.Count);

        bundle1.Unload(false);
        //bundle2.Unload(false);
    }
    
}