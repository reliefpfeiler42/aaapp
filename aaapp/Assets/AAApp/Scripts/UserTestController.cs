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
    private Vector3 _left;
    private Vector3 _right;
    private Vector3 _front;
    private Vector3 _back;
    private Vector3 _frontLeft;
    private Vector3 _frontRight;
    private Vector3 _backLeft;
    private Vector3 _backRight;

    // Start is called before the first frame update
    void Start()
    {
        _ballIsActive = false;
        _left = new Vector3(-2, 0, 0);
        _right = new Vector3(2, 0, 0);
        _front = new Vector3(0, 0, 2);
        _back = new Vector3(0, 0, -2);
        _frontLeft = new Vector3(-2, 0, 2);
        _frontRight = new Vector3(2, 0, 2);
        _backLeft = new Vector3(-2, 0, -2);
        _backRight = new Vector3(2, 0, -2);
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
        foreach (var image in _images)
        {
            if (_ballIsActive == false)
            {
                switch (image.DatabaseIndex)
                {
                    case 0:
                        InstantiateWithOffset(image, _left);
                        break;
                    case 1:
                        InstantiateWithOffset(image, _right);
                        break;
                    case 2:
                        InstantiateWithOffset(image, _front);
                        break;
                    case 3:
                        InstantiateWithOffset(image, _back);
                        break;
                    case 4:
                        InstantiateWithOffset(image, _frontLeft);
                        break;
                    case 5:
                        InstantiateWithOffset(image, _frontRight);
                        break;
                    case 6:
                        InstantiateWithOffset(image, _backLeft);
                        break;
                    case 7:
                        InstantiateWithOffset(image, _backRight);
                        break;
                    default:
                        break;
                }
            }
            else
                return;
        }
    }

    private void InstantiateWithOffset(AugmentedImage image, Vector3 offset)
    {
        Anchor anchor = image.CreateAnchor(image.CenterPose);
        _ball = Instantiate(BallPrefab, anchor.transform);
        _ball.transform.position = _ball.transform.position + offset;
        _ballIsActive = true;
    }

    private void Raycaster(Vector3 origin, Vector3 direction)
    {
        if (Physics.Raycast(origin, direction, 200.0f))
        {
            GameObject.Destroy(_ball);
            _ballIsActive = false;
        }
    }

}
