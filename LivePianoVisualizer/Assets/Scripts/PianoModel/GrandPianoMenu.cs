using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GrandPianoMenu : MonoBehaviour
{
    private bool menuEnabled;

    [SerializeField] private CameraOrbit cameraOrbit;

    [SerializeField] private GameObject menuPannel;
    [SerializeField] private InputField setNameInputField;

    [SerializeField] private TMP_Text frontPianoLogo;
    [SerializeField] private TMP_Text SidePianoLogo;


    void Start()
    {
        menuEnabled = false;
        menuPannel.SetActive(false);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            menuEnabled = !menuEnabled;
            LockCursor(!menuEnabled);
            menuPannel.SetActive(menuEnabled);
        }
    }

    private void LockCursor(bool state)
    {
        Cursor.visible = !state;
        Cursor.lockState = state ? CursorLockMode.Locked : CursorLockMode.Confined;
        cameraOrbit.enabled = state;
    }

    public void SetPianoName()
    {
        frontPianoLogo.text = setNameInputField.text;
        SidePianoLogo.text = setNameInputField.text;
    }

    public void OnClick_ChangeTo2dScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
