namespace GoogleARCore.Examples.ObjectManipulation
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public class FurnitureScrollViewContents : MonoBehaviour
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
            for (int i = 0; i < asset.furniture_models.Count; i++)
            {
                GameObject item = Instantiate(itemPrefab);
                item.GetComponent<FurnitureSelect>().buttonIndex = i;
                item.GetComponent<Image>().color = new Color(255, 0, 0);
                item.transform.SetParent(GameObject.Find("Content").transform);
            }
        }
    }
}
