using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;

namespace GoogleARCore.Examples.ObjectManipulation
{
    public class InteriorButton : MonoBehaviour
    {
        public void onClickedButton()
        {
            gameObject.GetComponent<PawnManipulator>().enabled = true;
            GameObject.FindWithTag("TileMesh").SetActive(false);
        }
    }
}
