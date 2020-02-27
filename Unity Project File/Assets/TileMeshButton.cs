using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;

public class TileMeshButton : MonoBehaviour
{
    public void onClickedButton()
    {
        GameObject.FindWithTag("Interior").SetActive(false);
    }
}
