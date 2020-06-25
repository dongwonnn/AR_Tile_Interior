using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class TextureScrollViewContents : MonoBehaviour
{
    public GameObject itemPrefab; // prefab to add

    GameObject assetBundle;
    GetAssetBundle asset;

    // Start is called before the first frame update
    void Start()
    {
        assetBundle = GameObject.Find("AssetBundleManager").gameObject;
        asset = assetBundle.GetComponent<GetAssetBundle>();

        // Set Button's image and index
        for (int i = 0; i < asset.mats.Count ; i++)
        {
            GameObject item = Instantiate(itemPrefab);
            item.GetComponent<TextureChange>().buttonIndex = i;
            item.GetComponent<Image>().sprite = asset.sprites[i];
            item.transform.SetParent(GameObject.Find("Content").transform);
        }
    }
}
