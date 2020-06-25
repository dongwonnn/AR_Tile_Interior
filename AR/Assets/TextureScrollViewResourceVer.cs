using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class TextureScrollViewResourceVer : MonoBehaviour
{
    public GameObject itemPrefab; // prefab to add

    GameObject spriteManager;
    GameObject materialManager;

    List<Sprite> sprites = new List<Sprite>();
    List<Material> materials = new List<Material>();
    List<GameObject> items = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        spriteManager = GameObject.Find("ResourceContainer").transform.Find("SpriteContainer").gameObject;
        materialManager = GameObject.Find("ResourceContainer").transform.Find("MaterialContainer").gameObject;
        //infoText = transform.Find("InfoText").gameObject.GetComponent<Text>();

        sprites = spriteManager.GetComponent<SpriteContainer>().sprites;
        materials = materialManager.GetComponent<MaterialContainer>().materials;

        Debug.Log(sprites.Count);

        // Set Button's image and index
        for (int i = 0; i < sprites.Count; i++)
        {
            GameObject item = Instantiate(itemPrefab);
            item.GetComponent<TextureChange>().buttonIndex = i;
            item.GetComponent<Image>().sprite = sprites[i];
            char sp = '_';
            string tileMat = "material";
            string[] substring = sprites[i].name.Split(sp);
            checkTileMat(substring[0], ref tileMat);
            item.GetComponent<TextureChange>().tileMat = tileMat; // set tile material
            item.GetComponent<TextureChange>().price = float.Parse(substring[3]); // set tile price
            /* print */
            item.GetComponentInChildren<Text>().text = "Name: " + substring[0] +
                                                      "\nHorizontal: " + substring[1] +
                                                      "\nVertial: " + substring[2] +
                                                      "\nMaterial: " + tileMat +
                                                      "\nPrice: " + substring[3] + " $";
            //item.GetComponentInChildren<Text>().text = sprites[i].name;
            item.transform.SetParent(GameObject.Find("Content").transform);
            items.Add(item);
        }
    }

    void checkTileMat(string s, ref string tileMat)
    {
        if(s[0] == 'M')
        {
            tileMat = "Marble";
        }
        else if(s[0] == 'W')
        {
            tileMat = "Wood";
        }
        else if(s[0] == 'S')
        {
            tileMat = "Stone";
        }
    }

    public void onClickedAll()
    {
        for (int i = 0; i < items.Count; i++)
        {
            items[i].SetActive(true);
        }
    }

    public void onClickedMarble()
    {
        for(int i=0; i<items.Count; i++)
        {
            if(items[i].GetComponent<TextureChange>().tileMat != "Marble")
            {
                items[i].SetActive(false);
            }
            else
            {
                items[i].SetActive(true);
            }
        }
    }

    public void onClickedStone()
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].GetComponent<TextureChange>().tileMat != "Stone")
            {
                items[i].SetActive(false);
            }
            else
            {
                items[i].SetActive(true);
            }
        }
    }

    public void onClickedWood()
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].GetComponent<TextureChange>().tileMat != "Wood")
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
