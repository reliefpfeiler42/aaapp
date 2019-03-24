using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using GoogleARCore;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace UserTest
{
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
        public GameObject Text9;
        public Button LoadMenuButton;

        private GameObject _ball;
        private readonly List<AugmentedImage> _images = new List<AugmentedImage>();
        private bool _ballIsActive;
        private Anchor _anchor;
        private int _timeCounter;
        private Vector3 _ballScale;
        private Vector3 _left;
        private Vector3 _right;
        private Vector3 _back;
        private Vector3 _up;
        private Vector3 _down;
        private Vector3 _frontLeft;
        private Vector3 _frontRight;
        private Vector3 _backLeft;
        private Vector3 _backRight;
        private Vector3 _testBall;

        Stopwatch stopWatch = new Stopwatch();

        void Start()
        {
            _ballIsActive = false;
            _ballScale = new Vector3(0.3f, 0.3f, 0.3f);

            // The offsets the ball will place from the image
            _left = new Vector3(-3, 0, 0);
            _right = new Vector3(3, 0, 0);
            _back = new Vector3(0, 0, -4);
            _up = new Vector3(0, 3, 0);
            _down = new Vector3(0, -3, 0);
            _frontLeft = new Vector3(-3, 0, 3);
            _frontRight = new Vector3(3, 0, 3);
            _backLeft = new Vector3(-3, 0, -4);
            _backRight = new Vector3(3, 0, -4);
            _testBall = new Vector3(0, 2, 2);

            LoadMenuButton.onClick.AddListener(LoadMenu);
        }

        void Update()
        {
            if (Session.Status != SessionStatus.Tracking)
            {
                GameObject.Destroy(_ball);
                GameObject.Destroy(_anchor);
                _ballIsActive = false;
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
                            InstantiateWithOffset(image, _back);
                            _timeCounter = 2;
                            break;
                        case 3:
                            InstantiateWithOffset(image, _up);
                            _timeCounter = 3;
                            break;
                        case 4:
                            InstantiateWithOffset(image, _down);
                            _timeCounter = 4;
                            break;
                        case 5:
                            InstantiateWithOffset(image, _frontLeft);
                            _timeCounter = 5;
                            break;
                        case 6:
                            InstantiateWithOffset(image, _frontRight);
                            _timeCounter = 6;
                            break;
                        case 7:
                            InstantiateWithOffset(image, _backLeft);
                            _timeCounter = 7;
                            break;
                        case 8:
                            InstantiateWithOffset(image, _backRight);
                            _timeCounter = 8;
                            break;
                        case 9:
                            InstantiateWithOffset(image, _testBall);
                            _timeCounter = 9;
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
            _anchor = image.CreateAnchor(image.CenterPose);
            _ball = Instantiate(BallPrefab, _anchor.transform);
            _ballIsActive = true;
            _ball.transform.position = _ball.transform.position + offset;
            _ball.transform.localScale = _ballScale;
            stopWatch.Reset();
            stopWatch.Start();
        }

        private void Raycaster(Vector3 origin, Vector3 direction)
        {
            if (Physics.Raycast(origin, direction, 200.0f))
            {
                GameObject.Destroy(_ball);
                GameObject.Destroy(_anchor);
                _ballIsActive = false;
                stopWatch.Stop();
                TimeSpan ts = stopWatch.Elapsed;
                string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:000}",
                    ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);

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
                case 8:
                    SetText(Text9, time);
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
}