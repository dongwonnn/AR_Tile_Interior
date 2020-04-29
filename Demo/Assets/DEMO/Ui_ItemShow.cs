using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ui_ItemShow : MonoBehaviour
{
    public Ui_ItemScrollView scpItemList;


    public void Invoke_Show_ItemScrollView()
    {
        scpItemList.gameObject.SetActive(true);
        scpItemList.InitData();


        gameObject.SetActive(false);
    }
}
