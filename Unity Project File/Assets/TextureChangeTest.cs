using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class TextureChangeTest : MonoBehaviour
{
    public List<Material> mats = new List<Material>();
    // Use this for initialization
    void Start()
    {
        StartCoroutine(LoadAssetBundle());
    }

    IEnumerator LoadAssetBundle()
    {
        string uri = "file:///" + Application.dataPath + "/AssetBundles/tilemaps0";
        UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(uri);

        yield return request.SendWebRequest();

        AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(request);

 
        for(int i=0; i<2; i++)
        {
            var mat = bundle.LoadAsset<Material>("tilemat" + i);
            mats.Add(mat);
        }
        
        //GetComponent<MeshRenderer>().material = mats[0];

        Debug.Log(mats.Count);
        //var prefab = bundle.LoadAsset<GameObject>("Stone");
        //Instantiate(prefab);

        //prefab = bundle.LoadAsset<GameObject>("Brick");
        //Instantiate(prefab);
        
        bundle.Unload(false);
    }
}

