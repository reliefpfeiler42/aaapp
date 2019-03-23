using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GoogleARCore;
using GoogleARCore.Examples.Common;

public class PlayController : MonoBehaviour
{
    public GameObject DinoPrefab;
    public GameObject DetectedPlanePrefab;
    public Button LoadMenuButton;

    private const float objectTransformation = 0.0f;
    private const float objectRotation = 270.0f;
    private Vector3 objectScale;

    // Start is called before the first frame update
    void Start()
    {
        LoadMenuButton.onClick.AddListener(LoadMenu);
        objectScale = new Vector3(0.2f, 0.2f, 0.2f);
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
            var myObject = Instantiate(DinoPrefab, hit.Pose.position, hit.Pose.rotation);
            myObject.transform.localScale = objectScale;
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

    void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
