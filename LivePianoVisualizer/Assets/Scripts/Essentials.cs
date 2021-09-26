using UnityEngine;
using NaughtyAttributes;
using System.Collections.Generic;
using HSVPicker;

public class Essentials : MonoBehaviour
{
    [BoxGroup("Color")] public ColorManager colorManager;
    [BoxGroup("Color")] public GameObject colorPickerObject;
    [BoxGroup("Color")] public ColorPicker colorPicker;
    [BoxGroup("Color")] public ColorSelection notesColorPicker;
    [BoxGroup("Color")] public ColorSelection laserColorPicker;
    [BoxGroup("Color")] public ColorSelection particlesColorPicker;

    [BoxGroup("Essentials")] public GameManager gameManger;
    [BoxGroup("Essentials")] public MidiInputManager midiInputManager;
    [BoxGroup("Essentials")] public WebcamSettings webcamSettings;
    [BoxGroup("Essentials")] public LaserSettings laserSettings;
    [BoxGroup("Essentials")] public MenuManager menuManager;

    [BoxGroup("Serialization")] public JsonReader jsonReader;

    [BoxGroup("Arduino")] public ArduinoDataHandler arduinoDataHandler;

    public List<GameObject> particleEmitter = new List<GameObject>();
}
