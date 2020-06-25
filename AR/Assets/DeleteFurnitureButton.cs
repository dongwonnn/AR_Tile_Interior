using System.Collections;
using System.Collections.Generic;
using GoogleARCore;
using GoogleARCore.Examples.Common;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DeleteFurnitureButton : MonoBehaviour
{
    public Camera FirstPersonCamera;
    public Text destoryText;
    bool onClicked = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (onClicked)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = FirstPersonCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit) && hit.collider.tag == "Furniture")
                {
                    Destroy(hit.collider.gameObject);
                }
            }
        }
    }

    public void onClickedButton()
    {
        if (onClicked)
        {
            onClicked = false;
            destoryText.text = "OFF";
        }
        else
        {
            onClicked = true;
            destoryText.text = "ON";
        }
    }
}
