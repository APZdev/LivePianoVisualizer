using UnityEngine;
using NaughtyAttributes;

public class ColorManager : MonoBehaviour
{
    private Essentials essentials;

    [BoxGroup("RGB Transition")] [SerializeField] private float colorLerpTime = 8f;
    [BoxGroup("RGB Transition")] [SerializeField] private Color[] colorList = null;
    [BoxGroup("RGB Transition")] public bool rgbFade;
    private float rgbTransitionTime;
    private int colorIndex;

    [SerializeField] private Material noteMaterial;

    private Color rgbColor;
    private Color noteColor;
    private Color particlesColor;
    private Color laserColor;

    Color tempColor;

    private float time;

    void Start()
    {
        essentials = transform.root.GetComponent<Essentials>();
    }

    private void FixedUpdate()
    {
        /*
        if (essentials.arduinoDataHandler.useArduino)
        {
            time += Time.fixedDeltaTime;
            if (time > 0.25f)
            {
                //Check if color has changed
                if (noteColor != tempColor)
                {
                    //Send 180 to tell the arduino it need to parse the color rgb values
                    ArduinoDataHandler.SendData(180);

                    //Send the rgb values one by one
                    ArduinoDataHandler.SendData((int)(noteColor.r * 255));
                    ArduinoDataHandler.SendData((int)(noteColor.g * 255));
                    ArduinoDataHandler.SendData((int)(noteColor.b * 255));
                    time = 0;
                    Debug.Log($"{(int)(noteColor.r * 255)}, {(int)(noteColor.g * 255)}, {(int)(noteColor.b * 255)}");
                }
                tempColor = noteColor;
            }
        }
        */
    }

    void Update()
    {
        if (essentials.menuManager.notesRgbSwitch.isOn)
        {
            RGB_Fade();
        }
        else
        {
            noteColor = essentials.notesColorPicker.selectedColor;
            laserColor = essentials.laserColorPicker.selectedColor;
            particlesColor = essentials.particlesColorPicker.selectedColor;
            SetColor(noteColor, particlesColor, laserColor);
        }
    }

    private void RGB_Fade()
    {
        //Apply color
        rgbColor = Color.Lerp(rgbColor, colorList[colorIndex], colorLerpTime * Time.deltaTime);

        //Count as the time past
        rgbTransitionTime = Mathf.Lerp(rgbTransitionTime, 1f, colorLerpTime * Time.deltaTime);
        if(rgbTransitionTime > 0.9f)
        {
            //Reset time
            rgbTransitionTime = 0f;
            //Change color
            colorIndex++;
            //Go to the bottom of list if reached the end
            colorIndex = (colorIndex >= colorList.Length) ? 0 : colorIndex;
        }
        SetColor(rgbColor, rgbColor, new Color(rgbColor.r, rgbColor.g, rgbColor.b, 10));
    }

    private void SetColor(Color noteColor, Color particlesColor, Color laserColor)
    {
        noteMaterial.SetColor("_EmissionColor", noteColor);
        essentials.laserSettings.laserMaterial.SetColor("_EmissionColor", laserColor);
        essentials.laserSettings.laserMaterial.SetColor("_BaseColor", new Color(255, 255, 255, essentials.laserSettings.laserOpacity));

        foreach (GameObject particle in essentials.particleEmitter)
        {
            ParticleManager particleManager = particle.GetComponent<ParticleManager>();

            //Get the particle system main module of each particles
            ParticleSystem.MainModule verticalSparklesParticle = particleManager.particles.GetComponent<ParticleSystem>().main;
            ParticleSystem.MainModule trailParticle = particleManager.trail.GetComponent<ParticleSystem>().main;
            ParticleSystem.MainModule halfRippleParticle = particleManager.halfRipple.GetComponent<ParticleSystem>().main;

            //Set the color of the particle
            Color finalParticlesColor = new Color(particlesColor.r, particlesColor.g, particlesColor.b, 255);
            verticalSparklesParticle.startColor = finalParticlesColor;
            trailParticle.startColor = finalParticlesColor;
            halfRippleParticle.startColor = finalParticlesColor;
        }
    }

    private string RoundNumber(float input)
    {
        if(input.ToString().Length == 1)
            return "00" + input;
        else if (input.ToString().Length == 2)
            return "0" + input;
        else
            return input.ToString();
    }
}
