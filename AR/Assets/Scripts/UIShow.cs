using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIShow : MonoBehaviour
{
    GameObject CreateButton;
    GameObject TextureButton;
    GameObject PlaneButton;
    GameObject ScrollView;

    //GameObject MeshGenerator;
    GameObject PawnGenerator;
    //GameObject ManipulationSystem;

    bool isOnScrollView;

    // Start is called before the first frame update
    void Start()
    {
        CreateButton = GameObject.Find("CreateButton");
        TextureButton = GameObject.Find("Canvas").transform.Find("TextureChangeButton").gameObject;
        PlaneButton = GameObject.Find("Canvas").transform.Find("HidePlaneButton").gameObject;
        ScrollView = GameObject.Find("Canvas").transform.Find("TextureScrollView").gameObject;

        //MeshGenerator = GameObject.Find("Generator").transform.Find("MeshGenerator").gameObject;
        PawnGenerator = GameObject.Find("Generator").transform.Find("PawnGenerator").gameObject;
        //ManipulationSystem = GameObject.Find("ManipulationSystem");
        
        isOnScrollView = false;
    }

    public void afterCreation()
    {
        CreateButton.SetActive(false);
        TextureButton.SetActive(true);
        PlaneButton.SetActive(true);
        PawnGenerator.SetActive(true);
        //ManipulationSystem.SetActive(true);
    }

    public void scrollView()
    {
        if(!isOnScrollView)
        {
            ScrollView.SetActive(true);
            isOnScrollView = true;
        }
        else
        {
            ScrollView.SetActive(false);
            isOnScrollView = false;
        }
    }
}
