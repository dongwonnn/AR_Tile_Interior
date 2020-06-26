namespace GoogleARCore.Examples.Common
{
    using System.Collections.Generic;
    using GoogleARCore;
    using UnityEngine;
    using UnityEngine.UI;

    public class OnOffPlane : MonoBehaviour
    {
        private bool status;
        //public Text ButtonText;
        public GameObject pointCloud;

        // Start is called before the first frame update
        void Start()
        {
            status = true;
            //ButtonText = ButtonText.GetComponent<Text>();
        }
        
        public void onClickedButton()
        {
            if (status)
            {
                status = false;
                OnTogglePlanes(status);
                //ButtonText.text = "Show";
            }
            else
            {
                status = true;
                OnTogglePlanes(status);
                //ButtonText.text = "Hide";
            }
        }
        
        // Button Clicked, Hide or Unhide Detected Plane
        public void OnTogglePlanes(bool flag)
        {
            foreach (GameObject plane in GameObject.FindGameObjectsWithTag("DetectedPlane"))
            {
                Renderer r = plane.GetComponent<Renderer>();
                DetectedPlaneVisualizer t = plane.GetComponent<DetectedPlaneVisualizer>();
                r.enabled = flag;
                t.enabled = flag;
            }
            pointCloud.SetActive(flag);
        }
    }
}
