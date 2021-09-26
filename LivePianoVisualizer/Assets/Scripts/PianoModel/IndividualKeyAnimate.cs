using UnityEngine;
using MidiJack;

public class IndividualKeyAnimate : MonoBehaviour
{

    public int keyID;
    public float pedalLerpTime;
    public Quaternion keyFinalRotation;

    private Quaternion keysInitialRotation;
    private bool keyState;

    void Start()
    {
        keyState = false;
        keysInitialRotation = transform.localRotation;
    }

    void Update() => AnimateKeys();

    private void AnimateKeys()
    {
        keyState = MidiMaster.GetKey(keyID + 21) > 0 ? true : false;

        if (keyState)
            transform.localRotation = Quaternion.Slerp(transform.localRotation, keyFinalRotation, pedalLerpTime * Time.deltaTime);
        else
            transform.localRotation = Quaternion.Slerp(transform.localRotation, keysInitialRotation, pedalLerpTime * Time.deltaTime);
    }
}

