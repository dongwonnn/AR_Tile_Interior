using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace GoogleARCore.Examples.ObjectManipulation
{
    public class SwapFurnitureMode : MonoBehaviour
    {
        GameObject pawnGenerator;
        public Text furnitureModeText;
        // Start is called before the first frame update
        void Start()
        {
            pawnGenerator = GameObject.Find("Generator").transform.Find("PawnGenerator").gameObject;
        }

        // Update is called once per frame
        public void onClickedButton()
        {
            if (pawnGenerator.GetComponent<PawnGenerator>().isFurnitureMode)
            {
                pawnGenerator.GetComponent<PawnGenerator>().isFurnitureMode = false;
                furnitureModeText.text = "Furniture Mode OFF";
            }
            else
            {
                pawnGenerator.GetComponent<PawnGenerator>().isFurnitureMode = true;
                furnitureModeText.text = "Furniture Mode ON";
            }
        }
    }
}
