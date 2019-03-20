using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using GoogleARCore;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UserTestController : MonoBehaviour
{
    public GameObject FirstPersonCamera;
    public GameObject BallPrefab;
    public GameObject Text1;
    public GameObject Text2;
    public GameObject Text3;
    public GameObject Text4;
    public GameObject Text5;
    public GameObject Text6;
    public GameObject Text7;
    public GameObject Text8;
    public Button LoadMenuButton;

    private GameObject _ball;
    private readonly List<AugmentedImage> _images = new List<AugmentedImage>();
    private bool _ballIsActive;
    private int _timeCounter;
    private Vector3 _left;
    private Vector3 _right;
    private Vector3 _front;
    private Vector3 _back;
    private Vector3 _frontLeft;
    private Vector3 _frontRight;
    private Vector3 _backLeft;
    private Vector3 _backRight;

    Stopwatch stopWatch = new Stopwatch();

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

        LoadMenuButton.onClick.AddListener(LoadMenu);
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
                        _timeCounter = 0;
                        break;
                    case 1:
                        InstantiateWithOffset(image, _right);
                        _timeCounter = 1;
                        break;
                    case 2:
                        InstantiateWithOffset(image, _front);
                        _timeCounter = 2;
                        break;
                    case 3:
                        InstantiateWithOffset(image, _back);
                        _timeCounter = 3;
                        break;
                    case 4:
                        InstantiateWithOffset(image, _frontLeft);
                        _timeCounter = 4;
                        break;
                    case 5:
                        InstantiateWithOffset(image, _frontRight);
                        _timeCounter = 5;
                        break;
                    case 6:
                        InstantiateWithOffset(image, _backLeft);
                        _timeCounter = 6;
                        break;
                    case 7:
                        InstantiateWithOffset(image, _backRight);
                        _timeCounter = 7;
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
        stopWatch.Reset();
        stopWatch.Start();
    }

    private void Raycaster(Vector3 origin, Vector3 direction)
    {
        if (Physics.Raycast(origin, direction, 200.0f))
        {
            GameObject.Destroy(_ball);
            _ballIsActive = false;
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);

            InsertTimeValue(elapsedTime, _timeCounter);
        }
    }

    private void InsertTimeValue(string time, int count)
    {
        switch (count)
        {
            case 0:
                SetText(Text1, time);
                break;
            case 1:
                SetText(Text2, time);
                break;
            case 2:
                SetText(Text3, time);
                break;
            case 3:
                SetText(Text4, time);
                break;
            case 4:
                SetText(Text5, time);
                break;
            case 5:
                SetText(Text6, time);
                break;
            case 6:
                SetText(Text7, time);
                break;
            case 7:
                SetText(Text8, time);
                break;
            default:
                break;
        }
    }

    private void SetText(GameObject Text, string time)
    {
        var text = Text.GetComponent<Text>();
        text.text = time;
    }

    void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }

}
