using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class LaserSettings : MonoBehaviour
{
    [SerializeField] [Range(0f, 10f)] private float videoSpeed = 1f;

    [HideInInspector] public GameObject laserObject;
    [HideInInspector] public float laserOpacity;
    [HideInInspector] public Material laserMaterial;

    private VideoPlayer videoPlayer;

    void Start()
    {
        laserObject = transform.GetChild(0).gameObject;
        laserMaterial = laserObject.GetComponentInChildren<Renderer>().material;
        videoPlayer = GetComponent<VideoPlayer>();
    }

    void Update()
    {
        //Set the video playback speed
        videoPlayer.playbackSpeed = videoSpeed;
    }
}
