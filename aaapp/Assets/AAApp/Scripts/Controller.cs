using System.Collections;
using System.Collections.Generic;
using GoogleARCore;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public GameObject PrefabToInstantiate;
    private const float objectTransformation = 0.075f;
    private const float objectRotation = 180.0f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // If the player has not touched the screen, we are done with this update.
        Touch touch;
        if (Input.touchCount < 1 || (touch = Input.GetTouch(0)).phase != TouchPhase.Began)
        {
            return;
        }

        TrackableHit hit;
        TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinBounds | TrackableHitFlags.PlaneWithinPolygon;

        if (Frame.Raycast(touch.position.x, touch.position.y, raycastFilter, out hit))
        {
            // Instantiate at hit pose.
            var myObject = Instantiate(PrefabToInstantiate, hit.Pose.position, hit.Pose.rotation);
            // Compensate for hitpose rotation.
            myObject.transform.Rotate(0, objectRotation, 0, Space.Self);
            // Compensate fpr hitpose tansformation.
            myObject.transform.Translate(0, objectTransformation, 0, Space.Self);
            // Create anchor.
            var anchor = hit.Trackable.CreateAnchor(hit.Pose);
            // Make instantiated object a child of the anchor.
            myObject.transform.parent = anchor.transform;

        }

    }
}
