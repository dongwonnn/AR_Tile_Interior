using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;

public class Plane_Mgr : MonoBehaviour
{
    public Camera arCamera;
    public GameObject andy;


    // Start is called before the first frame update
    void Start()
    {
        arCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Touch touch = Input.GetTouch(0);
        if(Input.touchCount > 0 && touch.phase == TouchPhase.Began)
        {
            TrackableHit hit;
            TrackableHitFlags flags = TrackableHitFlags.PlaneWithinPolygon
                                    | TrackableHitFlags.FeaturePointWithSurfaceNormal;

            if(Frame.Raycast(touch.position.x, touch.position.y, flags, out hit))
            {
                var anchor = hit.Trackable.CreateAnchor(hit.Pose);
                Instantiate(andy, hit.Pose.position, hit.Pose.rotation, anchor.transform);
            }
        }
    }
}
