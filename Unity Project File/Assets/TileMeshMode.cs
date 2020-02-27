using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileMeshMode : MonoBehaviour
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
        Interior.SetActive(false);
        TileMesh.SetActive(true);
        ModeText.text = "Tile";
    }
}
