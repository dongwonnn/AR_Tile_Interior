using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileCost : MonoBehaviour
{
    float cost;
    float area;
    float area2;
    string tmp;
    int index = 0;
    
    GameObject spriteManager;
    List<Sprite> sprites = new List<Sprite>();
    Mesh mesh;

    public Text costText;

    // Start is called before the first frame update
    void Start()
    {
        /* Get target gameobject */
        mesh = gameObject.GetComponent<MeshGenerator>().mesh;

        spriteManager = GameObject.Find("ResourceContainer").transform.Find("SpriteContainer").gameObject;
        sprites = spriteManager.GetComponent<SpriteContainer>().sprites;
        index = sprites.Count;
    }

    // Update is called once per frame
    void Update()
    {
        index = gameObject.GetComponent<MeshGenerator>().textureIndex;
        area = gameObject.GetComponent<MeshGenerator>().areaSize;
        //tmp = area.ToString("F4");
        //area = (int)(area * 10000);
        //area2 = area * 10000.0f;
        area = Mathf.Round(area);
        char sp = '_';
        string[] substring = sprites[index].name.Split(sp);
        cost = area / (float.Parse(substring[1]) * float.Parse(substring[2])) * float.Parse(substring[3]);
        //cost = Mathf.Round(cost);
        costText.text = "Tile Area : " + area + "cm^2" + "\n" + "Tile Cost : " + cost + "$";
    }
}
