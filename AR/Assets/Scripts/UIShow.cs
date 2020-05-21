using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIShow : MonoBehaviour
{
    public GameObject CreateButton;
    public GameObject TextureButton;
    public GameObject PlaneButton;
    public GameObject ScrollView;
    public GameObject FurnitureModeButton;
    public GameObject FurnitureModeText;

    //GameObject MeshGenerator;
    GameObject PawnGenerator;
    //GameObject ManipulationSystem;

    bool isOnScrollView;

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
        
        isOnScrollView = false;
    }

    public void afterCreation()
    {
        CreateButton.SetActive(false);
        TextureButton.SetActive(true);
        PlaneButton.SetActive(true);
        PawnGenerator.SetActive(true);

        //가구
        FurnitureModeButton.SetActive(true);
        FurnitureModeText.SetActive(true);
    }

    public void scrollView()
    {
        if(!isOnScrollView)
        {
            Debug.Log("is false"); 
            ScrollView.SetActive(true);
            isOnScrollView = true;
        }
        else
        {
            Debug.Log("is true");
            ScrollView.SetActive(false);
            isOnScrollView = false;
        }
    }
}
