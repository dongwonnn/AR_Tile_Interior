namespace GoogleARCore.Examples.ObjectManipulation
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class FurnitureSelect : MonoBehaviour
    {
        public int buttonIndex = 0;

        GameObject PawnGenerator;
        GameObject furniture_prefab;
        GameObject assetBundle;
        GetAssetBundle asset;

        // Start is called before the first frame update
        void Start()
        {
            PawnGenerator = GameObject.Find("Generator").transform.Find("PawnGenerator").gameObject;
            furniture_prefab = PawnGenerator.GetComponent<PawnGenerator>().PawnPrefab;
            assetBundle = GameObject.Find("AssetBundleManager");
            asset = assetBundle.GetComponent<GetAssetBundle>();
        }

        public void onClickedButton()
        {
            furniture_prefab = asset.furniture_models[buttonIndex];
        }
    }
}
