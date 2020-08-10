using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GoogleARCore.Examples.ObjectManipulation
{
    public class FurnitureInfo : MonoBehaviour
    {
        public int buttonIndex = 0;
        public string furnitureType = "furniture type";
        public string furnitureName = "furniture name";
        public float width = 0.0f;
        public float depth = 0.0f;
        public float height = 0.0f;
        public float price = 0.0f;

        GameObject PawnGenerator;
        GameObject pawnPrefab;

        GameObject PrefabManager;
        List<GameObject> furnitures = new List<GameObject>();
        
        void Start()
        {
            /* Get target gameobject */
            PawnGenerator = GameObject.Find("Generator").transform.Find("PawnGenerator").gameObject;
            pawnPrefab = PawnGenerator.GetComponent<PawnGenerator>().PawnPrefab;

            /* Get resources */
            PrefabManager = GameObject.Find("ResourceContainer").transform.Find("PrefabContainer").gameObject;
            furnitures = PrefabManager.GetComponent<PrefabContainer>().prefabs;
        }
        public void onClickedButton()
        {
            PawnGenerator.GetComponent<PawnGenerator>().PawnPrefab = furnitures[buttonIndex];
            PawnGenerator.GetComponent<PawnGenerator>().isFurnitureMode = 2;
        }
    }
}
