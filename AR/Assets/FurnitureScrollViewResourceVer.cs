using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GoogleARCore.Examples.ObjectManipulation
{
    public class FurnitureScrollViewResourceVer : MonoBehaviour
    {
        public GameObject itemPrefab; // prefab to add

        GameObject spriteManager;
        GameObject furnitureManager;

        List<Sprite> sprites = new List<Sprite>();
        List<GameObject> furnitures = new List<GameObject>();
        List<GameObject> items = new List<GameObject>();

        // Start is called before the first frame update
        void Start()
        {
            spriteManager = GameObject.Find("ResourceContainer").transform.Find("SpriteContainer2").gameObject;
            furnitureManager = GameObject.Find("ResourceContainer").transform.Find("PrefabContainer").gameObject;
            //infoText = transform.Find("InfoText").gameObject.GetComponent<Text>();

            sprites = spriteManager.GetComponent<SpriteContainer>().sprites;
            furnitures = furnitureManager.GetComponent<PrefabContainer>().prefabs;

            Debug.Log(sprites.Count);

            // Set Button's image and index
            for (int i = 0; i < sprites.Count; i++)
            {
                GameObject item = Instantiate(itemPrefab);
                item.GetComponent<FurnitureInfo>().buttonIndex = i;
                item.GetComponent<Image>().sprite = sprites[i];
                char sp = '_';
                string furnitureType = "furnitureType";
                string[] substring = sprites[i].name.Split(sp);
                checkTileMat(substring[0], ref furnitureType);
                item.GetComponent<FurnitureInfo>().furnitureType = furnitureType; // set furniture type
                item.GetComponent<FurnitureInfo>().price = float.Parse(substring[5]); // set furniture price
                /* print */
                item.GetComponentInChildren<Text>().text = "Name: " + substring[1] +
                                                          "\nWidth: " + substring[2] +
                                                          "\nDepth: " + substring[3] +
                                                          "\nHeight: " + substring[4] +
                                                          "\nPrice: " + substring[5] + " $";
                //item.GetComponentInChildren<Text>().text = sprites[i].name;
                item.transform.SetParent(GameObject.Find("Content").transform);
                items.Add(item);
            }
        }

        void checkTileMat(string s, ref string furnitureType)
        {
            if (s[0] == 'T')
            {
                furnitureType = "Table";
            }
            else if (s[0] == 'S')
            {
                furnitureType = "Sofa";
            }
            else if (s[0] == 'C')
            {
                furnitureType = "Chair";
            }
            else if (s[0] == 'B')
            {
                furnitureType = "Bed";
            }
            else
            {
                furnitureType = "Etc";
            }
        }

        public void onClickedAll()
        {
            for (int i = 0; i < items.Count; i++)
            {
                items[i].SetActive(true);
            }
        }

        public void onClickedChair()
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].GetComponent<FurnitureInfo>().furnitureType != "Chair")
                {
                    items[i].SetActive(false);
                }
                else
                {
                    items[i].SetActive(true);
                }
            }
        }

        public void onClickedSofa()
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].GetComponent<FurnitureInfo>().furnitureType != "Sofa")
                {
                    items[i].SetActive(false);
                }
                else
                {
                    items[i].SetActive(true);
                }
            }
        }

        public void onClickedTable()
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].GetComponent<FurnitureInfo>().furnitureType != "Table")
                {
                    items[i].SetActive(false);
                }
                else
                {
                    items[i].SetActive(true);
                }
            }
        }

        public void onClickedBed()
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].GetComponent<FurnitureInfo>().furnitureType != "Bed")
                {
                    items[i].SetActive(false);
                }
                else
                {
                    items[i].SetActive(true);
                }
            }
        }

        public void onClickedEtc()
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].GetComponent<FurnitureInfo>().furnitureType != "Etc")
                {
                    items[i].SetActive(false);
                }
                else
                {
                    items[i].SetActive(true);
                }
            }
        }
    }
}
