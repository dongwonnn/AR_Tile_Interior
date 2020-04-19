using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureChangeButton : MonoBehaviour
{
    public GameObject obj;
    private TextureChangeTest textureChange;
    private PolygonTesterZ polygonTester;
    int n = 0;
    // Start is called before the first frame update
    void Start()
    {
        textureChange = obj.GetComponent<TextureChangeTest>();
        polygonTester = obj.GetComponent<PolygonTesterZ>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onClickedButton()
    {
        polygonTester.isCreated = true;
        if (n < textureChange.mats.Count)
        {
            GetComponent<MeshRenderer>().material = textureChange.mats[n];
            n++;
        }
    }
}
