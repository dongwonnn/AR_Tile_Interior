using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotateTexture : MonoBehaviour
{
    GameObject meshGenerator;
    MeshRenderer meshRenderer;
    Mesh mesh;
    Vector2[] rotated_Uvs;
    //Vector2[] rotated_uvs;
    //Material material;
    //Texture2D texture;

    float degree = 1f;
    float degreeCount = 0f;

    private void Start()
    {
        meshGenerator = GameObject.Find("Generator").transform.Find("MeshGenerator").gameObject;
        mesh = meshGenerator.GetComponent<MeshGenerator>().mesh;
        meshRenderer = meshGenerator.GetComponent<MeshRenderer>();
        rotated_Uvs = meshGenerator.GetComponent<MeshGenerator>().uvsForChange;
        //rotated_uvs = meshGenerator.GetComponent<MeshGenerator>().uvsForChange;
        //texture = (Texture2D)meshRenderer.material.GetTexture("_MainTex");
    }

    public void onClickedButton()
    {
        Vector3[] vertices = meshGenerator.GetComponent<MeshGenerator>().vertsForTextureChange;
        Vector2[] uvs = new Vector2[vertices.Length];
        Vector2[] rotated_uvs = new Vector2[vertices.Length];

        Bounds bounds = mesh.bounds;
        int j = 0;

        while (j < uvs.Length)
        {
            uvs[j] = new Vector2(vertices[j].x / bounds.size.x, vertices[j].z / bounds.size.z);
            j++;
        }
        rotated_uvs = uvs;
        
        /*
        for (int i=0; i < meshGenerator.GetComponent<MeshGenerator>().uvsForChange.Length; i++)
        {
            if (degreeCount <= 4.5)
                meshGenerator.GetComponent<MeshGenerator>().uvsForChange[i] =
                Quaternion.AngleAxis(degree, Vector3.left) *
                Quaternion.AngleAxis(degree, Vector3.forward) * meshGenerator.GetComponent<MeshGenerator>().uvsForChange[i];
            else
            {
                meshGenerator.GetComponent<MeshGenerator>().uvsForChange[i] =
                Quaternion.AngleAxis(degree, Vector3.right) *
                Quaternion.AngleAxis(degree, Vector3.forward) * meshGenerator.GetComponent<MeshGenerator>().uvsForChange[i];
            }
        }
        mesh.uv = meshGenerator.GetComponent<MeshGenerator>().uvsForChange;
        degreeCount += 0.5f;
        if (degreeCount == 9.0f) degreeCount = 0;
        */

        for (int i = 0; i < rotated_uvs.Length; i++)
        {
            if (degreeCount <= 9)
                rotated_uvs[i] =
                Quaternion.AngleAxis(degree, Vector3.left) *
                Quaternion.AngleAxis(degree, Vector3.forward) * uvs[i];
            else
            {
                rotated_uvs[i] =
                Quaternion.AngleAxis(degree, Vector3.right) *
                Quaternion.AngleAxis(degree, Vector3.forward) * uvs[i];
            }
        }
        mesh.uv = rotated_uvs;
        degreeCount += 1f;
        if (degreeCount == 9) degreeCount = 0;

        meshRenderer.material.mainTextureScale = new Vector2(bounds.size.x, bounds.size.z);
    }
}
