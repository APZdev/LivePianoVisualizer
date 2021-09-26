using System.Collections.Generic;
using UnityEngine;
using MidiJack;
using NaughtyAttributes;

public class PianoAnimations : MonoBehaviour
{

    [BoxGroup("Pedal Settings")] [SerializeField] private Quaternion pedalFinalRotation;
    [BoxGroup("Pedal Settings")] [SerializeField] private float pedalLerpTime;
    [BoxGroup("Pedal Settings")] [SerializeField] private GameObject sustainPedal;
    [BoxGroup("Pedal Settings")] [SerializeField] private GameObject sostenutoPedal;
    [BoxGroup("Pedal Settings")] [SerializeField] private GameObject unacordaPedal;
    public List<PedalState> pedalStates = new List<PedalState>();
    private Quaternion pedalInitialRotation;

    [BoxGroup("Piano Settings")] [SerializeField] private Quaternion keyFinalRotation;
    [BoxGroup("Piano Settings")] [SerializeField] private GameObject pianoRig;
    [BoxGroup("Piano Settings")] [SerializeField] private float keyLerpTime;
    private Transform[] pianoKeys = new Transform[88];
    private Quaternion keysInitialRotation;


    private void Start()
    {
        pedalInitialRotation = Quaternion.identity;

        //Pedals
        pedalStates.Add(new PedalState(sustainPedal, 64, false));
        pedalStates.Add(new PedalState(sostenutoPedal, 66, false));
        pedalStates.Add(new PedalState(unacordaPedal, 67, false));

        //Keys
        //Get keys
        for (int i = 0; i < pianoRig.transform.childCount; i++)
        {
            pianoKeys[i] = pianoRig.transform.GetChild(i);
            pianoKeys[i].gameObject.AddComponent(typeof(IndividualKeyAnimate));
            IndividualKeyAnimate individualKeyAnimate = pianoKeys[i].GetComponent<IndividualKeyAnimate>();
            individualKeyAnimate.keyID = i;
            individualKeyAnimate.pedalLerpTime = keyLerpTime;
            individualKeyAnimate.keyFinalRotation = keyFinalRotation;
        }

    }

    void Update() => AnimatePedals();

    private void AnimatePedals()
    {
        foreach (PedalState pedalState in pedalStates)
        {
            pedalState.pedalState = MidiMaster.GetKnob(pedalState.pedalID) == 1 ? true : false;

            if (pedalState.pedalState)
                pedalState.pedalObject.transform.localRotation = Quaternion.Slerp(pedalState.pedalObject.transform.localRotation, pedalFinalRotation, pedalLerpTime * Time.deltaTime);
            else
                pedalState.pedalObject.transform.localRotation = Quaternion.Slerp(pedalState.pedalObject.transform.localRotation, pedalInitialRotation, pedalLerpTime * Time.deltaTime);
        }
    }
}

public class PedalState
{
    public GameObject pedalObject { get; set; }
    public int pedalID { get; set; }
    public bool pedalState { get; set; }

    public PedalState(GameObject pedal ,int id, bool state)
    {
        pedalObject = pedal;
        pedalID = id;
        pedalState = state;
    }
}
