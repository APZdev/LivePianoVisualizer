using UnityEngine;
using System.Collections;
using System.IO;

public class JsonReader : MonoBehaviour
{
    private string path;
    private string jsonContent;
    private Parameters parameters;

    private MenuManager menuManager;
    private Essentials essentials;

    void Start()
    {
        essentials = transform.root.GetComponent<Essentials>();
        menuManager = GetComponent<Essentials>().menuManager;

        if (Application.isEditor)
        {
            path = Application.dataPath + "/Ressources/appSettings.json";
        }
        else
        {
            path = Application.dataPath + "/appSettings.json";
            //Put the file in the root folder, not the "<NameOfProject>_Data" folder
            path = path.Replace("/LivePianoVisualizer_Data", "");
        }

        LoadData();
    }

    private void OnApplicationQuit()
    {
        SaveData();
    }

    public void LoadData()
    {
        jsonContent = File.ReadAllText(path);
        parameters = JsonUtility.FromJson<Parameters>(jsonContent);

        menuManager.laserSwitch.isOn = parameters.LaserEnabled;
        menuManager.laserOpacitySlider.value = parameters.LaserOpacity;
        essentials.laserColorPicker.selectedColor = parameters.LaserColor;

        essentials.notesColorPicker.selectedColor = parameters.NotesColor;
        menuManager.notesRgbSwitch.isOn  = parameters.RgbEnabled;
        menuManager.notesGlidingSpeedSlider.value = parameters.NotesGlidingSpeed;
        menuManager.notesWhiteWidthSlider.value = parameters.NotesWhiteWidth;
        menuManager.notesBlackWidthSlider.value = parameters.NotesBlackWidth;

        menuManager.particlesEnabledSwitch.isOn = parameters.ParticlesEnabled;
        essentials.particlesColorPicker.selectedColor = parameters.ParticlesColor;
        menuManager.particlesEnabledSwitch.isOn = parameters.ParticlesEnabled;
        menuManager.particlesTurbulenceFrequencySlider.value = parameters.ParticlesTurbulenceFrequencySlider;
        menuManager.particlesTurbulenceSpreadSlider.value = parameters.ParticlesTurbulenceSpreadSlider;
        menuManager.particlesCountSlider.value = parameters.ParticlesCountSlider;
        menuManager.particlesSizeSlider.value = parameters.ParticlesSizeSlider;
        menuManager.particlesSpeedSlider.value = parameters.ParticlesSpeedSlider;
        menuManager.particlesOpacitySlider.value = parameters.ParticlesOpacitySlider;
        menuManager.particlesLifetimeSlider.value = parameters.ParticlesLifetimeSlider;
        menuManager.particlesTrailsSwitch.isOn = parameters.ParticlesTrailsSwitch;
        menuManager.particlesTrailsSpeedSlider.value = parameters.ParticlesTrailsSpeedSlider;
        menuManager.particlesTrailsTurbulenceSlider.value = parameters.ParticlesTrailsTurbulenceSlider;
        menuManager.particlesTrailsWidthSlider.value = parameters.ParticlesTrailsWidthSlider;
        menuManager.particlesTrailsCountSlider.value = parameters.ParticlesTrailsCountSlider;
        menuManager.particlesTrailsLifetimeSlider.value = parameters.ParticlesTrailsLifetimeSlider;
        menuManager.particlesTrailsOpacitySlider.value = parameters.ParticlesTrailsOpacitySlider;
        menuManager.particlesSmokeSwitch.isOn = parameters.ParticlesSmokeSwitch;
        menuManager.particlesSmokeOpacitySlider.value = parameters.ParticlesSmokeOpacitySlider;
        menuManager.particlesRippleSwitch.isOn = parameters.ParticlesRippleSwitch;

        menuManager.backgroundImageToggle.isOn = parameters.BackgroundImageEnabled;
        menuManager.ApplyImageToBackground(parameters.BackgroundImagePath);
        menuManager.backgroundPianoLinesSwitch.isOn = parameters.BackgroundPianoLines;
        menuManager.backgroundImageOffsetX.value = parameters.BackgroundOffsetX;
        menuManager.backgroundImageOffsetY.value = parameters.BackgroundOffsetY;

        menuManager.webcamToggle.isOn = parameters.WebcamEnabled;
        essentials.webcamSettings.SelectWebcamDevice(parameters.WebcamDeviceName);
        menuManager.zoomSlider.value  = parameters.WebcamZoom;
        menuManager.offsetXSlider.value = parameters.WebcamOffsetX;
        menuManager.offsetYSlider.value = parameters.WebcamOffsetY;
        menuManager.rotateSlider.value = parameters.WebcamRotate;
        menuManager.stretchXSlider.value = parameters.WebcamStretchX;
        menuManager.stretchYSlider.value = parameters.WebcamStretchY;
        menuManager.webcamCropSwitch.isOn = parameters.WebcamCropEnabled;
        menuManager.webcamTopCropSlider.value = parameters.WebcamTop;
        menuManager.webcamBottomCropSlider.value = parameters.WebcamBottom;

        menuManager.darkFadeSwitch.isOn = parameters.WebcamDarkFade;
        menuManager.darkFadeOpacitySlider.value = parameters.WebcamDarkFadeOpacity;
    }

    public void SaveData()
    {
        parameters = JsonUtility.FromJson<Parameters>(jsonContent);

        parameters.LaserEnabled = menuManager.laserSwitch.isOn;
        parameters.LaserOpacity = menuManager.laserOpacitySlider.value;
        parameters.LaserColor = essentials.laserColorPicker.selectedColor;

        parameters.NotesColor = essentials.notesColorPicker.selectedColor;
        parameters.RgbEnabled = menuManager.notesRgbSwitch.isOn;
        parameters.NotesGlidingSpeed = menuManager.notesGlidingSpeedSlider.value;
        parameters.NotesWhiteWidth = menuManager.notesWhiteWidthSlider.value;
        parameters.NotesBlackWidth = menuManager.notesBlackWidthSlider.value;

        parameters.ParticlesColor = essentials.particlesColorPicker.selectedColor;
        parameters.ParticlesEnabled = menuManager.particlesEnabledSwitch.isOn;
        parameters.ParticlesTurbulenceFrequencySlider = menuManager.particlesTurbulenceFrequencySlider.value;
        parameters.ParticlesTurbulenceSpreadSlider = menuManager.particlesTurbulenceSpreadSlider.value;
        parameters.ParticlesCountSlider = menuManager.particlesCountSlider.value;
        parameters.ParticlesSizeSlider = menuManager.particlesSizeSlider.value;
        parameters.ParticlesSpeedSlider = menuManager.particlesSpeedSlider.value;
        parameters.ParticlesOpacitySlider = menuManager.particlesOpacitySlider.value;
        parameters.ParticlesLifetimeSlider = menuManager.particlesLifetimeSlider.value;
        parameters.ParticlesTrailsSwitch = menuManager.particlesTrailsSwitch.isOn;
        parameters.ParticlesTrailsSpeedSlider = menuManager.particlesTrailsSpeedSlider.value;
        parameters.ParticlesTrailsTurbulenceSlider = menuManager.particlesTrailsTurbulenceSlider.value;
        parameters.ParticlesTrailsWidthSlider = menuManager.particlesTrailsWidthSlider.value;
        parameters.ParticlesTrailsCountSlider = menuManager.particlesTrailsCountSlider.value;
        parameters.ParticlesTrailsLifetimeSlider = menuManager.particlesTrailsLifetimeSlider.value;
        parameters.ParticlesTrailsOpacitySlider = menuManager.particlesTrailsOpacitySlider.value;
        parameters.ParticlesSmokeSwitch = menuManager.particlesSmokeSwitch.isOn;
        parameters.ParticlesSmokeOpacitySlider = menuManager.particlesSmokeOpacitySlider.value;
        parameters.ParticlesRippleSwitch = menuManager.particlesRippleSwitch.isOn;

        parameters.BackgroundImageEnabled = menuManager.backgroundImageToggle.isOn;
        parameters.BackgroundImagePath = menuManager.backgroundImageTempPath;
        parameters.BackgroundPianoLines = menuManager.backgroundPianoLinesSwitch.isOn;
        parameters.BackgroundOffsetX = menuManager.backgroundImageOffsetX.value;
        parameters.BackgroundOffsetY = menuManager.backgroundImageOffsetY.value;


        parameters.WebcamEnabled = menuManager.webcamToggle.isOn;
        parameters.WebcamDeviceName = menuManager.selectedWebcamDeviceName;
        parameters.WebcamZoom = menuManager.zoomSlider.value;
        parameters.WebcamOffsetX = menuManager.offsetXSlider.value;
        parameters.WebcamOffsetY = menuManager.offsetYSlider.value;
        parameters.WebcamRotate = menuManager.rotateSlider.value;
        parameters.WebcamStretchX = menuManager.stretchXSlider.value;
        parameters.WebcamStretchY = menuManager.stretchYSlider.value;
        parameters.WebcamCropEnabled = menuManager.webcamCropSwitch.isOn;
        parameters.WebcamTop = menuManager.webcamTopCropSlider.value;
        parameters.WebcamBottom = menuManager.webcamBottomCropSlider.value;

        parameters.WebcamDarkFade = menuManager.darkFadeSwitch.isOn;
        parameters.WebcamDarkFadeOpacity = menuManager.darkFadeOpacitySlider.value;

        jsonContent = JsonUtility.ToJson(parameters, true);
        File.WriteAllText(path, jsonContent);
    }
}

[System.Serializable]
public class Parameters
{
    public bool LaserEnabled;
    public float LaserOpacity;
    public Color LaserColor;

    public Color NotesColor;
    public bool RgbEnabled;
    public float NotesGlidingSpeed;
    public float NotesBlackWidth;
    public float NotesWhiteWidth;

    public bool ParticlesEnabled;
    public Color ParticlesColor;
    public bool ParticlesEnabledSwitch;
    public float ParticlesTurbulenceFrequencySlider;
    public float ParticlesTurbulenceSpreadSlider;
    public float ParticlesCountSlider;
    public float ParticlesSizeSlider;
    public float ParticlesSpeedSlider;
    public float ParticlesOpacitySlider;
    public float ParticlesLifetimeSlider;
    public bool ParticlesTrailsSwitch;
    public float ParticlesTrailsSpeedSlider;
    public float ParticlesTrailsTurbulenceSlider;
    public float ParticlesTrailsWidthSlider;
    public float ParticlesTrailsCountSlider;
    public float ParticlesTrailsLifetimeSlider;
    public float ParticlesTrailsOpacitySlider;
    public bool ParticlesSmokeSwitch;
    public float ParticlesSmokeOpacitySlider;
    public bool ParticlesRippleSwitch;

    public bool BackgroundImageEnabled;
    public string BackgroundImagePath;
    public bool BackgroundPianoLines;
    public float BackgroundOffsetX;
    public float BackgroundOffsetY;

    public bool WebcamEnabled;
    public string WebcamDeviceName;
    public float WebcamZoom;
    public float WebcamOffsetX;
    public float WebcamOffsetY;
    public float WebcamRotate;
    public float WebcamStretchX;
    public float WebcamStretchY;
    public bool WebcamCropEnabled;
    public float WebcamTop;
    public float WebcamBottom;

    public bool WebcamDarkFade;
    public float WebcamDarkFadeOpacity;
}

