using System.Collections;
using System.Collections.Generic;
using GoogleARCore;
using UnityEngine;

public class UserTestController : MonoBehaviour
{
    public GameObject FirstPersonCamera;
    public GameObject BallPrefab;
    private GameObject _ball;
    private readonly List<AugmentedImage> _images = new List<AugmentedImage>();
    private bool _ballIsActive;

    // Start is called before the first frame update
    void Start()
    {
        _ballIsActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Session.Status != SessionStatus.Tracking)
        {
            // Debug.Log("#####################Lost Tracking#######################");
            return;
        }

        Session.GetTrackables(_images, TrackableQueryFilter.Updated);
        VisualizeGameObjects();
        Raycaster(FirstPersonCamera.transform.position, FirstPersonCamera.transform.TransformDirection(Vector3.forward));
    }

    private void VisualizeGameObjects()
    {
        Vector3 offset = new Vector3(10, 0, 0);

        foreach (var image in _images)
        {
            if (image.DatabaseIndex == 0 && _ballIsActive == false)
            {
                Anchor anchor = image.CreateAnchor(image.CenterPose);
                _ball = Instantiate(BallPrefab, anchor.transform);
                _ball.transform.position = _ball.transform.position + offset;
                _ballIsActive = true;
            }
        }
    }

    private void Raycaster(Vector3 origin, Vector3 direction)
    {
        RaycastHit hit;

        if (Physics.Raycast(origin, direction, out hit, 200.0f))
        {
            GameObject.Destroy(_ball);
            _ballIsActive = false;
        }
    }

}
