using System;
using System.Collections;
using System.Collections.Generic;
using GoogleARCore;
using UnityEngine;

public class AugmentedImageController : MonoBehaviour
{
    public GameObject DinoPrefab;
    // public GameObject DronePrefab;
    private readonly List<AugmentedImage> _images = new List<AugmentedImage>();
    private bool _dinoIsActive;
    // private bool _droneIsActive;

    void Start()
    {
        _dinoIsActive = false;
        // _droneIsActive = false;
    }

    void Update()
    {
        if (Session.Status != SessionStatus.Tracking)
        {
            // Debug.Log("#####################Lost Tracking#######################");
            return;
        }

        Session.GetTrackables(_images, TrackableQueryFilter.Updated);
        VisualizeGameObject();
    }

    private void VisualizeGameObject()
    {
        foreach (var image in _images)
        {
            if (image.DatabaseIndex == 0 && _dinoIsActive == false)
            {
                Anchor anchor = image.CreateAnchor(image.CenterPose);
                var dino = Instantiate(DinoPrefab, anchor.transform);
                _dinoIsActive = true;
            }
            // if (image.DatabaseIndex == 1 && _droneIsActive == false)
            // {
            //     Vector3 offset = new Vector3(0, 0, 10);
            //     Anchor anchor = image.CreateAnchor(image.CenterPose);
            //     GameObject drone = Instantiate(DronePrefab, anchor.transform);
            //     _droneIsActive = true;
            // }
        }
    }

}
