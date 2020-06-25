using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UIShow : MonoBehaviour
{
    public GameObject CreateButton;
    public GameObject TextureButton;
    public GameObject PlaneButton;
    public GameObject ScrollViewTexture;
    public GameObject ScrollViewFurniture;
    public GameObject FurnitureModeButton;
    public GameObject FurnitureModeText;
    public GameObject TextureRotationRightButton;
    public GameObject TextureRotationLeftButton;
    public GameObject DestroyFurnitureButton;

    public Text costText;

    float furnitureCost = 0.0f;

    //GameObject MeshGenerator;
    GameObject PawnGenerator;
    //GameObject ManipulationSystem;

    bool isOnScrollViewTexture;
    bool isOnScrollViewFurniture;

    // Start is called before the first frame update
    void Start()
    {
        //CreateButton = GameObject.Find("CreateButton");
        //TextureButton = GameObject.Find("Canvas").transform.Find("TextureChangeButton").gameObject;
        //PlaneButton = GameObject.Find("Canvas").transform.Find("HidePlaneButton").gameObject;
        //ScrollView = GameObject.Find("Canvas").transform.Find("TextureScrollView").gameObject;
        //TextureRotationButton = GameObject.Find("Canvas").transform.Find("TextureRotationButton").gameObject;
        //FurnitureModeButton = GameObject.Find("Canvas").transform.Find("FurnitureModeButton").gameObject;
        //FurnitureModeText = GameObject.Find("Canvas").transform.Find("FurnitureModeText").gameObject;

        PawnGenerator = GameObject.Find("Generator").transform.Find("PawnGenerator").gameObject;

        isOnScrollViewTexture = false;
        isOnScrollViewFurniture = false;
    }

    private void Update()
    {
        furnitureCost = gameObject.GetComponent<CalcFurnitureCost>().cost;
        costText.text = "Furniture Cost : " + furnitureCost + "$";
    }

    public void afterCreation()
    {
        CreateButton.SetActive(false);
        TextureButton.SetActive(true);
        PlaneButton.SetActive(true);
        PawnGenerator.SetActive(true);
        FurnitureModeButton.SetActive(true);
        FurnitureModeText.SetActive(true);
        TextureRotationRightButton.SetActive(true);
        TextureRotationLeftButton.SetActive(true);
        DestroyFurnitureButton.SetActive(true);
    }

    public void scrollViewTexture()
    {
        if(!isOnScrollViewTexture)
        {
            ScrollViewTexture.SetActive(true);
            isOnScrollViewTexture = true;
        }
        else
        {
            ScrollViewTexture.SetActive(false);
            isOnScrollViewTexture = false;
        }
    }

    public void scrollViewFurniture()
    {
        if (!isOnScrollViewFurniture)
        {
            ScrollViewFurniture.SetActive(true);
            isOnScrollViewFurniture = true;
        }
        else
        {
            ScrollViewFurniture.SetActive(false);
            isOnScrollViewFurniture = false;
        }
    }
}
