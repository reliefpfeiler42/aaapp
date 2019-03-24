using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GoogleARCore;
using GoogleARCore.Examples.Common;
using UnityEngine.EventSystems;

public class PlayController : MonoBehaviour
{
    public GameObject DinoPrefab;
    public GameObject DronePrefab;
    public GameObject DinoStereoPrefab;
    public GameObject DroneStereoPrefab;
    public GameObject DetectedPlanePrefab;
    public Button LoadMenuButton;
    public Button DeleteButton;
    public Button SelectDinoButton;
    public Button SelectDroneButton;
    public Button SelectDinoStereoButton;
    public Button SelectDroneStereoButton;

    private const float objectTransformation = 0.0f;
    private const float objectRotation = 270.0f;
    private Vector3 _objectScale;
    private Vector3 _scaleDino;
    private Vector3 _scaleDrone;
    private List<GameObject> _allObjects = new List<GameObject>();
    private bool _dinoIsSelected;
    private bool _droneIsSelected;
    private bool _dinoStereoIsSelected;
    private bool _droneStereoIsSelected;

    void Start()
    {
        // Initialize listeners on buttons
        LoadMenuButton.onClick.AddListener(LoadMenu);
        DeleteButton.onClick.AddListener(DeleteAllObjects);
        SelectDinoButton.onClick.AddListener(SelectDino);
        SelectDroneButton.onClick.AddListener(SelectDrone);
        SelectDinoStereoButton.onClick.AddListener(SelectDinoStereo);
        SelectDroneStereoButton.onClick.AddListener(SelectDroneStereo);

        _dinoIsSelected = true;
        _droneIsSelected = false;
        _dinoStereoIsSelected = false;
        _droneStereoIsSelected = false;
        _scaleDino = new Vector3(0.2f, 0.2f, 0.2f);
        _scaleDrone = new Vector3(0.2f, 0.2f, 0.2f);
        _objectScale = _scaleDino;
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
            if (!IsPointerOverUIObject())
            {
                // Instantiate at hit pose.
                var myObject = Instantiate(CurrentSelectedGameObject(), hit.Pose.position, hit.Pose.rotation);
                myObject.transform.localScale = _objectScale;
                // Compensate for hitpose rotation.
                myObject.transform.Rotate(0, objectRotation, 0, Space.Self);
                // Compensate fpr hitpose tansformation.
                myObject.transform.Translate(0, objectTransformation, 0, Space.Self);
                // Create anchor.
                var anchor = hit.Trackable.CreateAnchor(hit.Pose);
                // Make instantiated object a child of the anchor.
                myObject.transform.parent = anchor.transform;

                // Add game object to list
                _allObjects.Add(myObject as GameObject);

            }
        }
    }

    private GameObject CurrentSelectedGameObject()
    {
        GameObject selectedObject = new GameObject();

        if (_dinoIsSelected)
        {
            selectedObject = DinoPrefab;
            _objectScale = _scaleDino;
        }

        if (_droneIsSelected)
        {
            selectedObject = DronePrefab;
            _objectScale = _scaleDrone;
        }

        if (_dinoStereoIsSelected)
        {
            selectedObject = DinoStereoPrefab;
            _objectScale = _scaleDino;
        }

        if (_droneStereoIsSelected)
        {
            selectedObject = DroneStereoPrefab;
            _objectScale = _scaleDrone;
        }

        return selectedObject;
    }

    void SelectDino()
    {
        _dinoIsSelected = true;
        _droneIsSelected = false;
        _dinoStereoIsSelected = false;
        _droneStereoIsSelected = false;
        ChangeButtonColor(_dinoIsSelected, _droneIsSelected,  _dinoStereoIsSelected, _droneStereoIsSelected);
    }

    void SelectDrone()
    {
        _dinoIsSelected = false;
        _droneIsSelected = true;
        _dinoStereoIsSelected = false;
        _droneStereoIsSelected = false;
        ChangeButtonColor(_dinoIsSelected, _droneIsSelected,  _dinoStereoIsSelected, _droneStereoIsSelected);
    }

    void SelectDinoStereo()
    {
        _dinoIsSelected = false;
        _droneIsSelected = false;
        _dinoStereoIsSelected = true;
        _droneStereoIsSelected = false;
        ChangeButtonColor(_dinoIsSelected, _droneIsSelected,  _dinoStereoIsSelected, _droneStereoIsSelected);
    }

    void SelectDroneStereo()
    {
        _dinoIsSelected = false;
        _droneIsSelected = false;
        _dinoStereoIsSelected = false;
        _droneStereoIsSelected = true;
        ChangeButtonColor(_dinoIsSelected, _droneIsSelected,  _dinoStereoIsSelected, _droneStereoIsSelected);
    }

    private void ChangeButtonColor(bool value1, bool value2, bool value3, bool value4)
    {
        Image sDinoBtn = SelectDinoButton.GetComponent<Image>();
        Image sDroneBtn = SelectDroneButton.GetComponent<Image>();
        Image sDinoSBtn = SelectDinoStereoButton.GetComponent<Image>();
        Image sDroneSBtn = SelectDroneStereoButton.GetComponent<Image>();

        Color32 white = new Color32(255, 255, 255, 255);
        Color32 green = new Color32(80, 168, 124, 255);

        // dino selected
        if (value1)
        {
            sDinoBtn.color = green;
            sDroneBtn.color = white;
            sDinoSBtn.color = white;
            sDroneSBtn.color = white;
        }
        // drone selected
        if (value2)
        {
            sDinoBtn.color = white;
            sDroneBtn.color = green;
            sDinoSBtn.color = white;
            sDroneSBtn.color = white;
        }
        // dino stereo selected
        if (value3)
        {
            sDinoBtn.color = white;
            sDroneBtn.color = white;
            sDinoSBtn.color = green;
            sDroneSBtn.color = white;
        }
        // drone stereo selected
        if (value4)
        {
            sDinoBtn.color = white;
            sDroneBtn.color = white;
            sDinoSBtn.color = white;
            sDroneSBtn.color = green;
        }
    }

    void DeleteAllObjects()
    {
        for (int i = 0; i < _allObjects.Count; i++)
        {
            Destroy(_allObjects[i]);
        }
        _allObjects.Clear();
    }

    void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    private bool IsPointerOverUIObject()
    {
        Touch touch = Input.GetTouch(0);
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(touch.position.x, touch.position.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}
