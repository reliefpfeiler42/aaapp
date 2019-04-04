using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public Button LoadUserTestButton;
    public Button LoadUserTestStereoButton;
    public Button LoadPlayButton;

    // Start is called before the first frame update
    void Start()
    {
        LoadUserTestButton.onClick.AddListener(LoadUserTest);
        LoadUserTestStereoButton.onClick.AddListener(LoadUserTestStereo);
        LoadPlayButton.onClick.AddListener(LoadPlay);
    }

    void LoadUserTest()
    {
        SceneManager.LoadScene("UserTest");
    }

    void LoadUserTestStereo()
    {
        SceneManager.LoadScene("UserTestStereo");
    }

    void LoadPlay()
    {
        SceneManager.LoadScene("Play");
    }
}
