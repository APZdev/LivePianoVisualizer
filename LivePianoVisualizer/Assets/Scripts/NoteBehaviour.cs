using UnityEngine;

public class NoteBehaviour : MonoBehaviour
{
    private Essentials essentials;

    [HideInInspector] public bool hasFinished;

    private void Start()
    {
        essentials = transform.root.GetComponent<Essentials>();
        hasFinished = false;
    }

    private void Update()
    {
        if (hasFinished)
        {
            transform.position += new Vector3(0f, essentials.gameManger.glidingSpeed, 0f) * Time.deltaTime * 100f;
            Destroy(gameObject, essentials.gameManger.noteDestroyTime);
        }
    }
}
