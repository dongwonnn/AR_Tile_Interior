using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace GoogleARCore.Examples.ObjectManipulation
{
    public class SwapFurnitureMode : MonoBehaviour
    {
        GameObject pawnGenerator;
        //public Text furnitureModeText;
        // Start is called before the first frame update
        void Start()
        {
            pawnGenerator = GameObject.Find("Generator").transform.Find("PawnGenerator").gameObject;
        }

        // Update is called once per frame
        public void onClickedButton()
        {
            if (pawnGenerator.GetComponent<PawnGenerator>().isFurnitureMode == 0)
            {
                pawnGenerator.GetComponent<PawnGenerator>().isFurnitureMode = 1;
                //pawnGenerator.SetActive(true);
                //furnitureModeText.text = "Transform Furniture";
            }
            else if (pawnGenerator.GetComponent<PawnGenerator>().isFurnitureMode == 2)
            {
                pawnGenerator.GetComponent<PawnGenerator>().isFurnitureMode = 3;
                //pawnGenerator.SetActive(false);
                //furnitureModeText.text = "Selecting Furniture";
            }
            else
            {
                pawnGenerator.GetComponent<PawnGenerator>().isFurnitureMode = 0;
            }
        }
    }
}
