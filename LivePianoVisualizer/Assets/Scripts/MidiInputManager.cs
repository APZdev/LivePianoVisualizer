using System.Collections.Generic;
using UnityEngine;

public class MidiInputManager : MonoBehaviour
{
    //The list is public because if we had them using FindChild() the order is fucked up somehow in the build
    public List<GameObject> spawnPoint = new List<GameObject>();

    public List<GameObject> visualKeyboardKeys = new List<GameObject>();
    private GameObject[] keyboardNotes = new GameObject[128];

    void Awake()
    {
        GiveNotesIndex();
    }

    void GiveNotesIndex()
    {
        //Give notes an index only supported on the piano range
        for(int i = 0; i < keyboardNotes.Length; i++)
        {
            if(i >= 21 && i <= 108)
                spawnPoint[i - 21].GetComponent<NoteSpawner>().noteNumber = i;
        }
    }
}
