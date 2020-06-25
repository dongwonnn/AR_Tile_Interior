using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SizeDisplay : MonoBehaviour
{
    GameObject textMeshWidth;
    GameObject textMeshDepth;
    GameObject textMeshHeight;

    Vector3 boxSize;

    float width;
    float depth;
    float height;

    bool isOn;
    // Start is called before the first frame update
    void Start()
    {
        textMeshWidth = transform.Find("width").gameObject;
        textMeshDepth = transform.Find("depth").gameObject;
        textMeshHeight = transform.Find("height").gameObject;

        width = gameObject.transform.localScale.x;
        depth = gameObject.transform.localScale.z;
        height = gameObject.transform.localScale.y;

        isOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (textMeshWidth == null)
        {
            Debug.Log("null");
        }
        textMeshWidth.GetComponent<TextMeshPro>().text = width.ToString() + "m";
        textMeshDepth.GetComponent<TextMeshPro>().text = depth.ToString() + "m";
        textMeshHeight.GetComponent<TextMeshPro>().text = height.ToString() + "m";
    }

    public void OnOffMeter()
    {
        if (isOn)
        {
            textMeshWidth.SetActive(false);
            textMeshDepth.SetActive(false);
            textMeshHeight.SetActive(false);
            isOn = false;
        }
        else
        {
            textMeshWidth.SetActive(true);
            textMeshDepth.SetActive(true);
            textMeshHeight.SetActive(true);
            isOn = true;
        }
    }
}
