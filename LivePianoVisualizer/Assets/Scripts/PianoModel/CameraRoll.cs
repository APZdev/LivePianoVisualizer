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

    private void Update()
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
        if (Input.GetKeyDown(KeyCode.Alpha0))
            cameraIndexChanger(1);
        else if (Input.GetKeyDown(KeyCode.Alpha1))
            cameraIndexChanger(2);
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            cameraIndexChanger(3);
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            cameraIndexChanger(4);
        else if (Input.GetKeyDown(KeyCode.Alpha4))
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
