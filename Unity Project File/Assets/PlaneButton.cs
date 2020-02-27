namespace GoogleARCore.Examples.Common
{
    using System.Collections.Generic;
    using GoogleARCore;
    using UnityEngine;
    using UnityEngine.UI;

    public class PlaneButton : MonoBehaviour
    {
        private bool status;
        public Text ButtonText;

        // Start is called before the first frame update
        void Start()
        {
            status = true;
            ButtonText = ButtonText.GetComponent<Text>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void onClickedButton()
        {
            if (status)
            {
                status = false;
                OnTogglePlanes(status);
                ButtonText.text = "Show";
            }
            else
            {
                status = true;
                OnTogglePlanes(status);
                ButtonText.text = "Hide";
            }
        }

        public void OnTogglePlanes(bool flag)
        {
            foreach (GameObject plane in GameObject.FindGameObjectsWithTag("Plane"))
            {
                Renderer r = plane.GetComponent<Renderer>();
                DetectedPlaneVisualizer t = plane.GetComponent<DetectedPlaneVisualizer>();
                r.enabled = flag;
                t.enabled = flag;
            }
        }
    }
}
