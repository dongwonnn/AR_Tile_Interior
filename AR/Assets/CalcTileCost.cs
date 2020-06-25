using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalcTileCost : MonoBehaviour
{
    GameObject meshGenerator;
    GameObject spriteManager;

    List<Sprite> sprites = new List<Sprite>();
    Mesh mesh;

    public float cost = 0.0f;
    public int indivCost;
    public int index;
    public float area = 0.0f;

    public int tileIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        /* Get target gameobject */
        meshGenerator = GameObject.Find("Generator").transform.Find("MeshGenerator").gameObject;
        mesh = meshGenerator.GetComponent<MeshGenerator>().mesh;

        tileIndex = meshGenerator.GetComponent<MeshGenerator>().textureIndex;
        
        spriteManager = GameObject.Find("ResourceContainer").transform.Find("SpriteContainer").gameObject;
        sprites = spriteManager.GetComponent<SpriteContainer>().sprites;
        index = sprites.Count;
    }

    // Update is called once per frame
    void Update()
    {
        char sp = '_';
        string[] substring = sprites[tileIndex].name.Split(sp);
        area = Area(mesh);
        indivCost = meshGenerator.GetComponent<MeshGenerator>().textureIndex;
        //index = sprites.Count;
        cost = (Area(mesh) / (float.Parse(substring[1]) * float.Parse(substring[2]))) * float.Parse(substring[3]);
    }
    
    public float Area(Mesh m)
    {
        Vector3[] mVertices = m.vertices;
        Vector3 result = Vector3.zero;
        for (int p = mVertices.Length - 1, q = 0; q < mVertices.Length; p = q++)
        {
            result += Vector3.Cross(mVertices[q], mVertices[p]);
        }
        Debug.Log(result);
        result *= 0.5f;
        return result.magnitude;
    }
}
