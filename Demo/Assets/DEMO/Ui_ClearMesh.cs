using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore.Examples.HelloAR;

public class Ui_ClearMesh : MonoBehaviour
{
    //public HelloARController2 ar;
    public void Invoke_ClearMesh()
    {
        //ar.CreateMeshing(true);
        HelloARController2.ins.ClearMesh(true);
        Debug.Log("Invoke_ClearMesh 함수 호출");

    }
}
