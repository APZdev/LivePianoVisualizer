using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;
using UnityEngine.SceneManagement;
using SFB;
using System.IO;

public class MenuManager : MonoBehaviour
{
    #region UI Elements
    [BoxGroup("Design Pannel List")] [SerializeField] private GameObject[] pannelList;

    [BoxGroup("Sidebar")] [SerializeField] private GameObject sideBarObject = null;
    [BoxGroup("Sidebar")] [SerializeField] private float hiddenPositionX = -1020f;
    [BoxGroup("Sidebar")] [SerializeField] private float visiblePositionX = -900f;
    [BoxGroup("Sidebar")] [SerializeField] private float lerpTime = 10f;

    [BoxGroup("Laser")] public Switch laserSwitch = null;
    [BoxGroup("Laser")] public Slider laserOpacitySlider = null;

    [BoxGroup("Notes")] public Switch notesRgbSwitch = null;
    [BoxGroup("Notes")] public Slider notesGlidingSpeedSlider = null;
    [BoxGroup("Notes")] public Slider notesWhiteWidthSlider = null;
    [BoxGroup("Notes")] public Slider notesBlackWidthSlider = null;

    [BoxGroup("Particles")] public Switch particlesEnabledSwitch = null;
    [BoxGroup("Particles")] public Slider particlesTurbulenceFrequencySlider = null;
    [BoxGroup("Particles")] public Slider particlesTurbulenceSpreadSlider = null;
    [BoxGroup("Particles")] public Slider particlesCountSlider = null;
    [BoxGroup("Particles")] public Slider particlesSizeSlider = null;
    [BoxGroup("Particles")] public Slider particlesSpeedSlider = null;
    [BoxGroup("Particles")] public Slider particlesOpacitySlider = null;
    [BoxGroup("Particles")] public Slider particlesLifetimeSlider = null;
    [BoxGroup("Particles")] public Switch particlesTrailsSwitch = null;
    [BoxGroup("Particles")] public Slider particlesTrailsSpeedSlider = null;
    [BoxGroup("Particles")] public Slider particlesTrailsTurbulenceSlider = null;
    [BoxGroup("Particles")] public Slider particlesTrailsWidthSlider = null;
    [BoxGroup("Particles")] public Slider particlesTrailsCountSlider = null;
    [BoxGroup("Particles")] public Slider particlesTrailsLifetimeSlider = null;
    [BoxGroup("Particles")] public Slider particlesTrailsOpacitySlider = null;
    [BoxGroup("Particles")] public Switch particlesSmokeSwitch = null;
    [BoxGroup("Particles")] public Slider particlesSmokeOpacitySlider = null;
    [BoxGroup("Particles")] public Switch particlesRippleSwitch = null;


    [BoxGroup("Background Image")] public Switch backgroundImageToggle = null;
    [BoxGroup("Background Image")] public MeshRenderer backgroundImageObject = null;
    [BoxGroup("Background Image")] public Slider backgroundImageOffsetX = null;
    [BoxGroup("Background Image")] public Slider backgroundImageOffsetY = null;
    [BoxGroup("Background Image")] public Switch backgroundPianoLinesSwitch = null;
    [BoxGroup("Background Image")] [SerializeField] private GameObject pianoLinesObject = null;
    [HideInInspector] public string backgroundImageTempPath;


    [BoxGroup("Webcam")] [SerializeField] private GameObject webcamObject = null;
    [BoxGroup("Webcam")] public Switch webcamToggle = null;
    [BoxGroup("Webcam")] public Slider zoomSlider = null;
    [BoxGroup("Webcam")] public Slider offsetXSlider = null;
    [BoxGroup("Webcam")] public Slider offsetYSlider = null;
    [BoxGroup("Webcam")] public Slider rotateSlider = null;
    [BoxGroup("Webcam")] public Slider stretchXSlider = null;
    [BoxGroup("Webcam")] public Slider stretchYSlider = null;
    [BoxGroup("Webcam")] [SerializeField] private float webcamCameraPositionY = 0.65f;
    [BoxGroup("Webcam")] [SerializeField] private float webcamTransitionLerpTime = 0.65f;
    [BoxGroup("Webcam")] [SerializeField] private GameObject webcamSelectionPannel = null;
    [BoxGroup("Webcam")] [SerializeField] private GameObject webcamItemPrefab = null;
    [BoxGroup("Webcam")] [SerializeField] private Transform webcamItemHolder = null;
    [HideInInspector] public string selectedWebcamDeviceName = "";
    private WebcamSettings webcam;
    private bool cameraRefreshed;
    [BoxGroup("Webcam")] public Switch webcamCropSwitch = null;
    [BoxGroup("Webcam")] public Slider webcamTopCropSlider = null;
    [BoxGroup("Webcam")] public Slider webcamBottomCropSlider = null;

    [BoxGroup("DarkFade")] [SerializeField] private GameObject darkFadeObject = null;
    [BoxGroup("DarkFade")] public Switch darkFadeSwitch = null;
    [BoxGroup("DarkFade")] public Slider darkFadeOpacitySlider = null;

    private Vector3 defaultCameraPosition;

    private Essentials essentials;

    #endregion

    #region Main Methods

    private void Start()
    {
        essentials = transform.root.GetComponent<Essentials>();

        defaultCameraPosition = Camera.main.transform.position;

        webcam = essentials.webcamSettings;
        cameraRefreshed = false;

        Invoke("OnValueChange_UpdateParticlesSettings", 0.005f);
    }

    private void Update()
    {
        DisplayMenuBar();
        NotesSettings();
        LaserSettings();
        WebcamSettings();
        BackgroundImageSettings();
    }

    #endregion

    #region Public Methods
    public void OnValueChange_UpdateParticlesSettings()
    {
        //This method is used in the toggle click event and on start -> that's why it's public
        foreach (GameObject go in essentials.particleEmitter)
        {
            ParticleManager particleManager = go.GetComponent<ParticleManager>();

            switch(particlesEnabledSwitch.isOn)
            {
                case true:
                    particleManager.particles.SetActive(true);
                    particleManager.particles.GetComponent<ParticleSystem>().Play();

                    ParticleSystem particleSystem = particleManager.particles.GetComponent<ParticleSystem>();
                    ParticleSystem.MainModule particleMain = particleSystem.main;
                    ParticleSystem.EmissionModule particleEmission = particleSystem.emission;
                    ParticleSystem.VelocityOverLifetimeModule particleVelocityOverLifetime = particleSystem.velocityOverLifetime;
                    ParticleSystem.NoiseModule particleNoise = particleSystem.noise;

                    particleNoise.frequency = particlesTurbulenceFrequencySlider.value;
                    particleNoise.separateAxes = true;
                    particleNoise.strengthX = particlesTurbulenceSpreadSlider.value;
                    
                    //The note count is changed on the NoteSpawner script
                    particleMain.startSize = particlesSizeSlider.value;
                    particleVelocityOverLifetime.speedModifier = particlesSpeedSlider.value;
                    particleManager.particles.GetComponent<ParticleSystemRenderer>().material.SetColor("_BaseMap", new Color(1, 1, 1, particlesOpacitySlider.value));
                    particleMain.startLifetime = particlesLifetimeSlider.value;
                    break;
                case false:
                    particleManager.particles.SetActive(false);
                    particleManager.particles.GetComponent<ParticleSystem>().Stop();
                    break;
            }

            switch (particlesTrailsSwitch.isOn)
            {
                case true:
                    particleManager.trail.SetActive(true);
                    particleManager.trail.GetComponent<ParticleSystem>().Play();

                    ParticleSystem particleSystem = particleManager.trail.GetComponent<ParticleSystem>();
                    ParticleSystem.MainModule particleMain = particleSystem.main;
                    ParticleSystem.EmissionModule particleEmission = particleSystem.emission;
                    ParticleSystem.VelocityOverLifetimeModule particleVelocityOverLifetime = particleSystem.velocityOverLifetime;
                    ParticleSystem.TrailModule particleNoise = particleSystem.trails;

                    particleVelocityOverLifetime.speedModifier = particlesSpeedSlider.value;
                    particleNoise.widthOverTrail = particlesTrailsWidthSlider.value;
                    particleManager.trail.GetComponent<ParticleSystemRenderer>().material.SetColor("_BaseMap", new Color(1, 1, 1, particlesTrailsOpacitySlider.value));

                    break;
                case false:
                    particleManager.trail.SetActive(false);
                    particleManager.trail.GetComponent<ParticleSystem>().Stop();
                    break;
            }

            particleManager.smoke.SetActive(particlesSmokeSwitch.isOn);
            if (!particlesSmokeSwitch.isOn) { particleManager.smoke.GetComponent<ParticleSystem>().Emit(0); }

            particleManager.halfRipple.SetActive(particlesRippleSwitch.isOn);
            if (!particlesRippleSwitch.isOn) { particleManager.halfRipple.GetComponent<ParticleSystem>().Emit(0); }
        }
    }

    public void OnClick_ClearBackgroundImage()
    {
        backgroundImageTempPath = "";
        backgroundImageObject.material.SetTexture("_BaseMap", null);
        backgroundImageObject.material.SetColor("_BaseMap", new Color(0, 0, 0, 0));
        backgroundImageOffsetX.value = 1;
        backgroundImageOffsetY.value = 1;
    }

    public void OnClick_SelectBackgroundImage()
    {
        //Allow all the possible images extensions
        ExtensionFilter[] extensions = new[] {
            new ExtensionFilter("Image Files", "png", "jpg", "jpeg" ),
        };

        //Open the file browser to select the picture
        string[] path = StandaloneFileBrowser.OpenFilePanel("Choose an Image...", "", extensions, false);

        if(path.Length > 0)
            ApplyImageToBackground(path[0]);
    }

    public void ApplyImageToBackground(string path)
    {
        if (File.Exists(path))
        {
            backgroundImageTempPath = path;
            byte[] bytes = File.ReadAllBytes(path);
            Texture2D texture = new Texture2D(1, 1);
            texture.LoadImage(bytes);
            backgroundImageObject.material.SetTexture("_BaseMap", texture);
            backgroundImageObject.material.SetColor("_Color", Color.white);
        }
        else
        {
            Debug.Log("The saved path do not lead to an image");
        }
    }

    public void ChangeToGrandPianoScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);

        //Stop the camera when changing to the 3d piano scene because it somehow doesn't turn off automatically
        essentials.webcamSettings.StopWebcam();
        essentials.jsonReader.SaveData();
    }

    public void OnClick_RefreshAvailableWebcam()
    {
        //When set to false -> camera UI list get refreshed
        cameraRefreshed = false;
    }

    public void OnClick_SetDesignOpenPannel(int id)
    {
        if(pannelList[id].activeSelf)
        {
            pannelList[id].SetActive(false);
            return;
        }

        for (int i = 0; i < pannelList.Length; i++)
        {
            pannelList[i].SetActive(i == id);
        }
    }

    public void OnClick_QuitGame()
    {
        Application.Quit();
    }

    #endregion

    #region Private Methods

    private void DisplayMenuBar()
    {
        bool barStatus;
        barStatus = true;
        Vector3 screenPoint = Input.mousePosition;


        //Check if the mouse position on X is smaller than a threshold
        barStatus = Camera.main.ScreenToWorldPoint(screenPoint).x < -7.7f ? true : false;

        if (barStatus)
        {
            sideBarObject.transform.localPosition = new Vector3(Mathf.Lerp(sideBarObject.transform.localPosition.x, visiblePositionX, lerpTime * Time.deltaTime), 
                                                                sideBarObject.transform.localPosition.y, 
                                                                sideBarObject.transform.localPosition.z);
        }
        else
        {
            sideBarObject.transform.localPosition = new Vector3(Mathf.Lerp(sideBarObject.transform.localPosition.x, hiddenPositionX, lerpTime * Time.deltaTime), 
                                                                sideBarObject.transform.localPosition.y,
                                                                sideBarObject.transform.localPosition.z);
        }
    }

    private void NotesSettings()
    {
        essentials.gameManger.glidingSpeed = notesGlidingSpeedSlider.value;
        essentials.gameManger.whiteNotesWidth = notesWhiteWidthSlider.value;
        essentials.gameManger.blackNotesWidth = notesBlackWidthSlider.value;
    }

    private void LaserSettings()
    {
        essentials.laserSettings.laserObject.SetActive(laserSwitch.isOn);
        essentials.laserSettings.laserOpacity = laserOpacitySlider.value;
    }

    private void BackgroundImageSettings()
    {
        backgroundImageObject.gameObject.SetActive(backgroundImageToggle.isOn);
        backgroundImageObject.material.SetTextureOffset("_BaseMap", new Vector2(backgroundImageOffsetX.value, backgroundImageOffsetY.value));

        pianoLinesObject.SetActive(backgroundPianoLinesSwitch.isOn);
    }

    private void WebcamSettings()
    {
        //Turn on/off objects based on the UI elments states
        webcamObject.SetActive(webcamToggle.isOn);
        webcamSelectionPannel.SetActive(webcamToggle.isOn);
        darkFadeObject.SetActive(darkFadeSwitch.isOn);
        webcam.cropObject.SetActive(webcamCropSwitch.isOn);

        if (webcamToggle.isOn)
        {
            //Change the main camera position
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position,
                                                          new Vector3(Camera.main.transform.position.x, webcamCameraPositionY, Camera.main.transform.position.z),
                                                          webcamTransitionLerpTime * Time.deltaTime);

            //Change the opacity of the dark fade according to the slider
            SpriteRenderer darkFadeImage = darkFadeObject.GetComponent<SpriteRenderer>();
            darkFadeImage.color = new Vector4(darkFadeImage.color.r, darkFadeImage.color.g, darkFadeImage.color.b, darkFadeOpacitySlider.value);

            if(!cameraRefreshed)
            {
                //Clear the old items
                foreach (Transform child in webcamItemHolder) { Destroy(child.gameObject); }

                //Display all the current webcam items
                foreach (WebCamDevice item in webcam.GetWebcamDevices())
                {
                    GameObject go = Instantiate(webcamItemPrefab);
                    go.transform.SetParent(webcamItemHolder);
                    go.GetComponent<WebcamItem>().SetItem(item.name);
                    cameraRefreshed = true;
                }
            }

            //Change values based on sliders
            webcam.webcamZoom = zoomSlider.value;
            webcam.offsetX = offsetXSlider.value;
            webcam.offsetY = offsetYSlider.value;
            webcam.rotate = rotateSlider.value;
            webcam.stretchX = stretchXSlider.value;
            webcam.stretchY = stretchYSlider.value;
            webcam.topCropHeight = webcamTopCropSlider.value;
            webcam.bottomCropHeight = webcamBottomCropSlider.value;
        }
        else
        {
            essentials.laserSettings.laserObject.SetActive(false);

            //Reset the main camera position
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, defaultCameraPosition, webcamTransitionLerpTime * Time.deltaTime);
        }
    }

    #endregion
}
