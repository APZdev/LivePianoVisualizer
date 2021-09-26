using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CameraRoll : MonoBehaviour
{
    public Transform[] cameraPoses;
    public GameObject cameraOrbit;
    public float cameraRollTime;

    public bool cameraRollEnabled;

    public Animator fadeAnimator;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
            cameraRollEnabled = !cameraRollEnabled;

        if(cameraRollEnabled)
        {
            cameraRoll();
        }
        else
        {
            for (int i = 0; i < cameraPoses.Length; i++)
            {
                cameraPoses[i].gameObject.SetActive(false);
            }
            cameraOrbit.SetActive(true);
        }
    }
    private void cameraRoll()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
            cameraIndexChanger(1);
        else if (Input.GetKeyDown(KeyCode.Keypad2))
            cameraIndexChanger(2);
        else if (Input.GetKeyDown(KeyCode.Keypad3))
            cameraIndexChanger(3);
        else if (Input.GetKeyDown(KeyCode.Keypad4))
            cameraIndexChanger(4);
        else if (Input.GetKeyDown(KeyCode.Keypad5))
            cameraIndexChanger(5);
    }

    private void cameraIndexChanger(int index)
    {
        for (int i = 0; i < cameraPoses.Length; i++)
        {
            if(i == index)
                cameraPoses[i].gameObject.SetActive(true);
            if(i != index)
                cameraPoses[i].gameObject.SetActive(false);
        }
    }
}
