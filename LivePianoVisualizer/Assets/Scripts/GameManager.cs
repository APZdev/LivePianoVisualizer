using UnityEngine;
using NaughtyAttributes;

public class GameManager : MonoBehaviour
{
    [BoxGroup("GameSettings")] public float glidingSpeed = 5f;
    [BoxGroup("GameSettings")] public float noteDestroyTime = 10f;
    [BoxGroup("GameSettings")] public float whiteNotesWidth = 0.5f;
    [BoxGroup("GameSettings")] public float blackNotesWidth = 0.25f;

    [BoxGroup("Prefabs")] public GameObject notePrefab;
    [BoxGroup("Prefabs")] public GameObject particleEmitterPrefab;
}
