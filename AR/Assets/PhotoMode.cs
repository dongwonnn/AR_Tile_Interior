using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine;

public class PhotoMode : MonoBehaviour
{
    public GameObject TextureButton;
    public GameObject PlaneButton;
    public GameObject ScrollViewTexture;
    public GameObject ScrollViewFurniture;
    public GameObject FurnitureModeButton;
    public GameObject FurnitureModeText;
    public GameObject TextureRotationRightButton;
    public GameObject TextureRotationLeftButton;
    public GameObject DestroyFurnitureButton;
    public GameObject FurnitureCostText;
    public GameObject TileCostText;

    bool isPhotoMode = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClikedButton()
    {
        if (isPhotoMode)
        {
            TextureButton.SetActive(false);
            PlaneButton.SetActive(false);
            FurnitureModeButton.SetActive(false);
            FurnitureModeText.SetActive(false);
            TextureRotationRightButton.SetActive(false);
            TextureRotationLeftButton.SetActive(false);
            DestroyFurnitureButton.SetActive(false);
            FurnitureCostText.SetActive(false);
            TileCostText.SetActive(false);
            isPhotoMode = false;
        }
        else
        {
            TextureButton.SetActive(true);
            PlaneButton.SetActive(true);
            FurnitureModeButton.SetActive(true);
            FurnitureModeText.SetActive(true);
            TextureRotationRightButton.SetActive(true);
            TextureRotationLeftButton.SetActive(true);
            DestroyFurnitureButton.SetActive(true);
            FurnitureCostText.SetActive(true);
            TileCostText.SetActive(true);
            isPhotoMode = true;
        }
    }
}
