using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class NoteTexture : MonoBehaviour
{
    private VideoPlayer videoPlayer;
    [HideInInspector] public Sprite videoTexture;
    [SerializeField] [Range(0f, 10f)] private float videoSpeed = 1f;

    [HideInInspector] public GameObject laserObject;
    [HideInInspector] public float laserPositionY;

    void Start()
    {
        laserObject = transform.GetChild(0).gameObject;
        laserObject.SetActive(true);
        videoTexture = transform.GetComponentInChildren<Sprite>();
        videoPlayer = GetComponent<VideoPlayer>();
    }

    void Update()
    {   
        //Set the video playback speed
        videoPlayer.playbackSpeed = videoSpeed;

        //Set the position of the laser on the screen
        laserObject.transform.localPosition = new Vector3(laserObject.transform.localPosition.x, laserPositionY, laserObject.transform.localPosition.z);
    }
}
