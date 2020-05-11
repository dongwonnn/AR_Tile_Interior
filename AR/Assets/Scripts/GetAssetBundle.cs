using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GetAssetBundle : MonoBehaviour
{
    public List<Material> mats = new List<Material>();
    public List<Sprite> sprites = new List<Sprite>();
    
    // Use this for initialization
    void Start()
    {
        StartCoroutine(LoadAssetBundle());
    }

    IEnumerator LoadAssetBundle()
    {
        string uri = "jar:file://" + Application.dataPath + "!/assets/tilepack01";
        UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(uri);

        yield return request.SendWebRequest();

        AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(request);


        for (int i = 1; i <= 9; i++)
        {
            var mat = bundle.LoadAsset<Material>("M_Tile0" + i);
            mats.Add(mat);
            var sprite = bundle.LoadAsset<Sprite>("ST_Tile0" + i);
            sprites.Add(sprite);
        }

        Debug.Log(mats.Count);

        bundle.Unload(false);
    }
}