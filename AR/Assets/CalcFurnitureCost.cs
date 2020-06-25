using GoogleARCore.Examples.ObjectManipulation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class CalcFurnitureCost : MonoBehaviour
{
    public GameObject[] furnitures;
    public float cost = 0.0f;
    public string number;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        furnitures = GameObject.FindGameObjectsWithTag("Furniture");
        char sp = '_';
        float tmp = 0.0f;
        for (int i = 0; i < furnitures.Length; i++)
        {
            string[] substring = furnitures[i].name.Split(sp);
            //substring[2].Replace("(Clone)", "").Trim();
            number = Regex.Replace(substring[2], "[^0-9]", "");
            //tmp += float.Parse(substring[2]);
            tmp += float.Parse(number);
        }
        cost = tmp;
    }
}
