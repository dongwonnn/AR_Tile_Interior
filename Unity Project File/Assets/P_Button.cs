namespace GoogleARCore.Examples.Common
{
    using System.Collections.Generic;
    using GoogleARCore;
    using UnityEngine;
    using UnityEngine.UI;

    public class P_Button : MonoBehaviour
    {
        public Text ButtonText;
        private DetectedPlaneGenerator PlaneDetectionComponent;
        private GameObject PlaneObject;
        private DetectedPlaneVisualizer PlaneDetectionVisualizer;
        private MeshRenderer m_MeshRenderer;
        private Renderer rend;
        private bool Status;

        // Start is called before the first frame update
        void Awake()
        {
            // Get Button
            ButtonText = ButtonText.GetComponent<Text>();

            PlaneDetectionComponent = GetComponent<DetectedPlaneGenerator>();

            PlaneObject = PlaneDetectionComponent.DetectedPlanePrefab;

            PlaneDetectionVisualizer = PlaneObject.GetComponent<DetectedPlaneVisualizer>();

            //PlaneDetectionVisualizer = gameObject.GetComponent<DetectedPlaneVisualizer>();

            //PlaneDetectionVisualizer = GameObject.Find("DetectedPlaneVisualizer").GetComponent<DetectedPlaneVisualizer>();

            m_MeshRenderer = PlaneObject.GetComponent<MeshRenderer>();

            rend = PlaneObject.GetComponent<Renderer>();
            
            Status = true;
        }

        // Update is called once per frame
        void Update()
        {

        }
        //public void DestroyThisObject()
        //{
        //    Destroy(gameObject);
        //}
        public void Display()
        {
            if (PlaneDetectionVisualizer != null)
            {
                if (Status) // hide
                {
                    //rend.enabled = false;
                    PlaneDetectionVisualizer.updateScript(0);
                    m_MeshRenderer.enabled = false;
                    ButtonText.text = "Show";
                    Status = false;
                }
                else // show
                {
                    //rend.enabled = true;
                    PlaneDetectionVisualizer.updateScript(1);
                    m_MeshRenderer.enabled = true;
                    ButtonText.text = "Hide";
                    Status = true;
                }
            }
        }
        public void Destory()
        {
            Destroy(gameObject);
        }
    }
}
