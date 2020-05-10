using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextureChange : MonoBehaviour
{
    public int buttonIndex = 0;

    GameObject assetBundle;
    GameObject meshGenerator;
    Mesh mesh;
    MeshRenderer meshRenderer;
    GetAssetBundle asset;

    void Start()
    {
        meshGenerator = GameObject.Find("Generator").transform.Find("MeshGenerator").gameObject;
        mesh = meshGenerator.GetComponent<MeshGenerator>().mesh;
        meshRenderer = meshGenerator.GetComponent<MeshRenderer>();
        assetBundle = GameObject.Find("AssetBundleManager");
        asset = assetBundle.GetComponent<GetAssetBundle>();
    }

    public void onClickedButton()
    {
        meshGenerator.GetComponent<MeshRenderer>().material = asset.mats[buttonIndex];

        // Recalculate UVs and TextureScale on changing material.
        Vector3[] vertices = meshGenerator.GetComponent<MeshGenerator>().vertsForTextureChange;

        Vector2[] uvs = new Vector2[vertices.Length];
        Bounds bounds = mesh.bounds;
        int j = 0;
        while (j < uvs.Length)
        {
            uvs[j] = new Vector2(vertices[j].x / bounds.size.x, vertices[j].z / bounds.size.z);
            j++;
        }
        mesh.uv = uvs;

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        meshRenderer.material.mainTextureScale = new Vector2(bounds.size.x, bounds.size.z);

    }
}