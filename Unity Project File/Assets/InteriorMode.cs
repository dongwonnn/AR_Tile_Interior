using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteriorMode : MonoBehaviour
{
    public GameObject Interior;
    public GameObject TileMesh;
    public Text ModeText;
    private void Awake()
    {
        Interior = GameObject.FindWithTag("Interior");
        TileMesh = GameObject.FindWithTag("TileMesh");
        ModeText = ModeText.GetComponent<Text>();
    }
    public void onClickedButton()
    {
        Interior.SetActive(true);
        TileMesh.SetActive(false);
        ModeText.text = "Interior";
    }
}
