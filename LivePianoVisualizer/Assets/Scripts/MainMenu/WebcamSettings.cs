using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebcamSettings : MonoBehaviour
{
    [SerializeField] private int width = 640;
    [SerializeField] private int height = 360;
    [SerializeField] private int frameRate = 30;

    [HideInInspector] public float webcamZoom = 1f;
    [HideInInspector] public float offsetX = 0f;
    [HideInInspector] public float offsetY = 0f;
    [HideInInspector] public float rotate = 180f;
    [HideInInspector] public float stretchX = 1;
    [HideInInspector] public float stretchY = 1;

    public GameObject cropObject;
    [HideInInspector] public bool cropActive;
    public GameObject topCropObject;
    [Range(0f, 6f)] public float topCropHeight = 0f;
    public GameObject bottomCropObject;
    [Range(0f, 6f)] public float bottomCropHeight = 0f;

    private Transform webcamObject;
    private Renderer webcamImage;

    private MenuManager menuManager;
    [HideInInspector] public WebCamTexture webcamTextureTemp;

    void Awake()
    {
        webcamObject = transform;
        webcamImage = GetComponent<Renderer>();
        menuManager = transform.root.GetComponent<Essentials>().menuManager;
    }

    void Update()
    {
        //Crop size
        topCropObject.transform.localScale = new Vector2(topCropObject.transform.localScale.x, topCropHeight);
        topCropObject.transform.localPosition = new Vector3(topCropObject.transform.localPosition.x, -(topCropHeight / 2), 0.5f);

        bottomCropObject.transform.localScale = new Vector2(bottomCropObject.transform.localScale.x, bottomCropHeight);
        bottomCropObject.transform.localPosition = new Vector3(bottomCropObject.transform.localPosition.x, (bottomCropHeight / 2) - 6f, 0.5f);

        //Scale and position the camera based on slider values
        webcamObject.localScale = new Vector3(16 * webcamZoom * stretchX, 9 * webcamZoom * stretchY, 1f);
        webcamObject.localPosition = new Vector3(offsetX, offsetY, 1f);
        webcamObject.localEulerAngles = new Vector3(webcamObject.localRotation.x, webcamObject.localRotation.y, rotate);
    }   

    public WebCamDevice[] GetWebcamDevices()
    {
        return WebCamTexture.devices;
    }

    public void SelectWebcamDevice(string webcamName)
    {
        //Check if the camera we are trying to get the texture from exists
        foreach(WebCamDevice webcam in WebCamTexture.devices)
        {
            if(webcam.name == webcamName)
            {
                //Apply the camera texture and store the camera name for the later settings save
                UpdateCameraSettings(new WebCamTexture(webcamName, width, height, frameRate));
                menuManager.selectedWebcamDeviceName = webcamName;
            }
        }
    }

    public void UpdateCameraSettings(WebCamTexture texture)
    {
        //TODO : FIX THIS SHIT
        //Doesnt work if we want to change webcam because it check if a camera has already been selected
        if (webcamImage.material.mainTexture != null) return;

        webcamImage.material.SetTexture("_BaseMap", texture);
        webcamTextureTemp = texture;
        texture.Play();
    }

    public void StopWebcam()
    {
        webcamTextureTemp.Stop();
    }
}
