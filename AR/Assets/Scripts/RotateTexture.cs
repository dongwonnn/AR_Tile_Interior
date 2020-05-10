using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotateTexture : MonoBehaviour
{
    GameObject meshGenerator;
    MeshRenderer meshRenderer;
    Mesh mesh;
    Material material;
    Texture2D texture;

    float degree = 0.0f;

    private void Start()
    {
        meshGenerator = GameObject.Find("Generator").transform.Find("MeshGenerator").gameObject;
        mesh = meshGenerator.GetComponent<MeshGenerator>().mesh;
        meshRenderer = meshGenerator.GetComponent<MeshRenderer>();
        texture = (Texture2D)meshRenderer.material.GetTexture("_MainTex");
    }

    public void onClickedButton()
    {
        //texture = rotateTexture(texture, true);
        //meshRenderer.material.mainTexture = texture;

        // Recalculate UVs and TextureScale on changing material.
        //Vector3[] vertices = meshGenerator.GetComponent<MeshGenerator>().vertsForTextureChange;

        //Vector2[] uvs = new Vector2[vertices.Length];
        //Bounds bounds = mesh.bounds;
        //int j = 0;
        //while (j < uvs.Length)
        //{
        //    uvs[j] = new Vector2(vertices[j].x / bounds.size.x, vertices[j].z / bounds.size.z);
        //    j++;
        //}

        //for(int i=0; i<uvs.Length; i++)
        //{
        //    uvs[i] = Rotate(uvs[i], 10);
        //}

        //mesh.uv = uvs;

        //mesh.RecalculateNormals();
        //mesh.RecalculateBounds();

        //meshRenderer.material.mainTextureScale = new Vector2(bounds.size.x, bounds.size.z);


        //if(meshRenderer.material.HasProperty("Angle"))
        //{
            degree += .05f;
            meshRenderer.material.SetFloat("Angle",degree);
        //}
    }
    Vector2 Rotate(Vector2 input, float angle)
    {
        float c = Mathf.Cos(angle * Mathf.Deg2Rad);
        float s = Mathf.Sin(angle * Mathf.Deg2Rad);
        return new Vector2(
            input.x * c - input.y * s,
            input.x * s + input.y * c);
    }

    //Texture2D rotateTexture(Texture2D originalTexture, bool clockwise)
    //{
    //    Color32[] original = originalTexture.GetPixels32();
    //    Color32[] rotated = new Color32[original.Length];
    //    int w = originalTexture.width;
    //    int h = originalTexture.height;

    //    int iRotated, iOriginal;

    //    for (int j = 0; j < h; ++j)
    //    {
    //        for (int i = 0; i < w; ++i)
    //        {
    //            iRotated = (i + 1) * h - j - 1;
    //            iOriginal = clockwise ? original.Length - 1 - (j * w + i) : j * w + i;
    //            rotated[iRotated] = original[iOriginal];
    //        }
    //    }

    //    Texture2D rotatedTexture = new Texture2D(h, w);
    //    rotatedTexture.SetPixels32(rotated);
    //    rotatedTexture.Apply();
    //    return rotatedTexture;
    //}

    //Texture2D rotateTexture(Texture2D tex, float angle)
    //{
    //    Debug.Log("rotating");
    //    Texture2D rotImage = new Texture2D(tex.width, tex.height);
    //    int x, y;
    //    float x1, y1, x2, y2;

    //    int w = tex.width;
    //    int h = tex.height;
    //    float x0 = rot_x(angle, -w / 2.0f, -h / 2.0f) + w / 2.0f;
    //    float y0 = rot_y(angle, -w / 2.0f, -h / 2.0f) + h / 2.0f;

    //    float dx_x = rot_x(angle, 1.0f, 0.0f);
    //    float dx_y = rot_y(angle, 1.0f, 0.0f);
    //    float dy_x = rot_x(angle, 0.0f, 1.0f);
    //    float dy_y = rot_y(angle, 0.0f, 1.0f);


    //    x1 = x0;
    //    y1 = y0;

    //    for (x = 0; x < tex.width; x++)
    //    {
    //        x2 = x1;
    //        y2 = y1;
    //        for (y = 0; y < tex.height; y++)
    //        {
    //            //rotImage.SetPixel (x1, y1, Color.clear);          

    //            x2 += dx_x;//rot_x(angle, x1, y1);
    //            y2 += dx_y;//rot_y(angle, x1, y1);
    //            rotImage.SetPixel((int)Mathf.Floor(x), (int)Mathf.Floor(y), getPixel(tex, x2, y2));
    //        }

    //        x1 += dy_x;
    //        y1 += dy_y;

    //    }

    //    rotImage.Apply();
    //    return rotImage;
    //}

    //private Color getPixel(Texture2D tex, float x, float y)
    //{
    //    Color pix;
    //    int x1 = (int)Mathf.Floor(x);
    //    int y1 = (int)Mathf.Floor(y);

    //    if (x1 > tex.width || x1 < 0 ||
    //       y1 > tex.height || y1 < 0)
    //    {
    //        pix = Color.clear;
    //    }
    //    else
    //    {
    //        pix = tex.GetPixel(x1, y1);
    //    }

    //    return pix;
    //}

    //private float rot_x(float angle, float x, float y)
    //{
    //    float cos = Mathf.Cos(angle / 180.0f * Mathf.PI);
    //    float sin = Mathf.Sin(angle / 180.0f * Mathf.PI);
    //    return (x * cos + y * (-sin));
    //}
    //private float rot_y(float angle, float x, float y)
    //{
    //    float cos = Mathf.Cos(angle / 180.0f * Mathf.PI);
    //    float sin = Mathf.Sin(angle / 180.0f * Mathf.PI);
    //    return (x * sin + y * cos);
    //}
}
