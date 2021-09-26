using UnityEngine;

public class NoteBehaviour : MonoBehaviour
{
    private Essentials essentials;

    [HideInInspector] public bool hasFinished;

    void Start()
    {
        essentials = transform.root.GetComponent<Essentials>();
        hasFinished = false;
    }

    void FixedUpdate()
    {
        if (hasFinished)
        {
            transform.position += new Vector3(0f, essentials.gameManger.glidingSpeed, 0f);
            Destroy(gameObject, essentials.gameManger.noteDestroyTime);
        }
    }
}
