using System.Collections.Generic;
using UnityEngine;
using MidiJack;

public class NoteSpawner : MonoBehaviour
{
    private MidiInputManager midiInputManager;
    private Essentials essentials;
    private ArduinoDataHandler arduinoDataHandler;

    private GameObject particleEmitter;

    private bool isPressed;
    private bool noteHasSpawned;
    private bool particleHasPlayed;

    private List<GameObject> continuousParticles = new List<GameObject>();
    private List<GameObject> playOnceParticles = new List<GameObject>();
    private List<GameObject> existantNotes = new List<GameObject>();

    public int noteNumber;
    public bool blackNote;

    private bool arduinoSendCheck;
    private float timeElapsed;

    #region Main Methods

    void Start()
    {
        essentials = transform.root.GetComponent<Essentials>();
        arduinoDataHandler = essentials.arduinoDataHandler;
        midiInputManager = essentials.midiInputManager;
        //Set this to false by default so that only black keys change state
        blackNote = false;

        BlackOrWhiteNote();
        SetupParticleSystem();
    }

    void Update()
    {
        if (MidiMaster.GetKey(noteNumber) > 0.0000000000001f)
        {
            isPressed = true; //If key is being pressed
        }
        else
        {
            isPressed = false; //If key is released
            noteHasSpawned = false;
        }

        CreateNoteObject();
        MoveNotesObject();
        UpdateParticlesStates();
        //Need to go back on this later, not really useful -> more for other peoples
        //UpdateKeyboardKeys();
        SendInputsToArduino();
    }

    #endregion

    void CreateNoteObject()
    {
        //Instantiate the note object based on the pressed note
        if (isPressed && !noteHasSpawned)
        {
            noteHasSpawned = true;
            //Spawn the note object
            var go = Instantiate(essentials.gameManger.notePrefab,
                                 new Vector3(midiInputManager.spawnPoint[noteNumber - 21].transform.position.x, -0.075f, 0f),
                                 Quaternion.identity, 
                                 midiInputManager.spawnPoint[noteNumber - 21].transform);
            go.GetComponent<SpriteRenderer>().size = new Vector2(NoteSize(), 0.2f);
            existantNotes.Add(go);
        }
    }

    private void MoveNotesObject()
    {
        //If at least a note exist
        if (existantNotes.Count > 0)
        {
            if (isPressed)
            {
                //make move only the top of the note
                //existantNotes[0].transform.localScale += new Vector3(0f, midiInputManager.glidingSpeed, 0f);
                existantNotes[0].GetComponent<SpriteRenderer>().size += new Vector2(0f , essentials.gameManger.glidingSpeed) * Time.deltaTime * 100;
                existantNotes[0].GetComponent<NoteBehaviour>().hasFinished = false;
            }
            else
            {
                //Remove it from the list to keep track only of the played note
                existantNotes[0].GetComponent<NoteBehaviour>().hasFinished = true;
                existantNotes.RemoveAt(0);
            }
        }
    }

    void SetupParticleSystem()
    {
        particleEmitter = Instantiate(essentials.gameManger.particleEmitterPrefab, new Vector3(transform.position.x, transform.position.y, -0.05f), Quaternion.identity, transform);//Spawn Particle Emitter
        essentials.particleEmitter.Add(particleEmitter);

        ParticleManager particleManager = particleEmitter.GetComponent<ParticleManager>();

        //Get every particle systems
        continuousParticles.Add(particleManager.particles);
        continuousParticles.Add(particleManager.trail);

        playOnceParticles.Add(particleManager.halfRipple);


        //Stop each particle systems
        foreach (GameObject particleSystemObject in continuousParticles)
        {
            particleSystemObject.GetComponent<ParticleSystem>().Stop();
        }
        foreach (GameObject particleSystemObject in playOnceParticles)
        {
            particleSystemObject.GetComponent<ParticleSystem>().Stop();
        }
    }

    private void UpdateParticlesStates()
    {
        if (isPressed)
        {
            //Spawn continuously
            for (int i = 0; i < continuousParticles.Count; i++)
            {
                //Start each particle systems
                if(continuousParticles[i].activeSelf)
                {
                    if(i == 0)
                    {
                        continuousParticles[i].GetComponent<ParticleSystem>().Emit((int)essentials.menuManager.particlesCountSlider.value);
                    }
                    else if(i == 1)
                    {
                        continuousParticles[i].GetComponent<ParticleSystem>().Emit((int)essentials.menuManager.particlesTrailsCountSlider.value);
                    }
                }

            } 

            //Spawn once
            if(!particleHasPlayed)
            {
                particleHasPlayed = true;
                foreach (GameObject particleSystemObject in playOnceParticles)
                {
                    if (particleSystemObject.activeSelf)
                    {
                        //Start each particle systems
                        particleSystemObject.GetComponent<ParticleSystem>().Emit(1);
                    }
                }
            }
        }
        else
        {
            if(particleHasPlayed)
            {
                foreach (GameObject particleSystemObject in playOnceParticles)
                {
                    particleSystemObject.GetComponent<ParticleSystem>().Stop();
                }
                particleHasPlayed = false;
            }

            //Stop each particle systems
            foreach (GameObject particleSystemObject in continuousParticles)
            {
                particleSystemObject.GetComponent<ParticleSystem>().Stop();
            }
        }
    }

    private void BlackOrWhiteNote()
    {
        for (int i = 0; i < Constants.blackNotesList.Length; i++)
        {
            if(noteNumber == Constants.blackNotesList[i])
            {
                blackNote = true;
                break;
            }
        }
    }

    private float NoteSize()
    {
        //Return the width 
        if(blackNote)
            return essentials.gameManger.blackNotesWidth;
        else
            return essentials.gameManger.whiteNotesWidth;
    }

    private void SendInputsToArduino()
    {
        if (essentials.arduinoDataHandler.useArduino)
        {
            if (isPressed)
            {
                //Send on a fixed interval, otherwise it fill the arduino buffer too fast and introduces latency
                timeElapsed += Time.deltaTime;
                if (timeElapsed > 0.025f)
                {
                    Debug.Log("pressed");
                    //Send note on signal
                    ArduinoDataHandler.SendData(noteNumber - 21);
                    arduinoDataHandler.noteStates[noteNumber - 21] = 1;
                    arduinoSendCheck = true;
                    timeElapsed = 0;
                }
            }
            else if(!isPressed && arduinoSendCheck)
            {
                //Send the note off signal
                ArduinoDataHandler.SendData(88 + (noteNumber - 21));
                arduinoSendCheck = false;
            }
        }
    }
}
