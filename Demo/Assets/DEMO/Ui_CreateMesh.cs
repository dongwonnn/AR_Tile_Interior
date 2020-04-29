using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore.Examples.HelloAR;

public class Ui_CreateMesh : MonoBehaviour
{
    //public HelloARController2 ar;
    public void Invoke_CreateMesh()
    {
        //ar.CreateMeshing(true);
        HelloARController2.ins.CreateMeshing(true);
        Debug.Log("Invoke_CreateMesh 함수 호출");
    }
}
