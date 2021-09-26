using UnityEngine;
using NaughtyAttributes;

public class CameraOrbit : MonoBehaviour
{
    private float xRot = 0f;
    private float yRot = 0f;
    private float currentCameraDistance = 5f;

    [BoxGroup("Mouse settings")] [SerializeField] private Transform target;
    [BoxGroup("Mouse settings")] [SerializeField] private float mouseSensitivity = 500f;
    [BoxGroup("Mouse settings")] [SerializeField] private float scrollSensitivity = 250f;
    [BoxGroup("Mouse settings")] [SerializeField] private float minCameraDistance = 3f;
    [BoxGroup("Mouse settings")] [SerializeField] private float maxCameraDistance = 50f;

    private void Start()
    {
        currentCameraDistance = 7f;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (target == null) return;

        xRot += Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        yRot += Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        currentCameraDistance -= Input.GetAxis("Mouse ScrollWheel") * scrollSensitivity * Time.deltaTime;

        currentCameraDistance = Mathf.Clamp(currentCameraDistance, minCameraDistance, maxCameraDistance);

        if (xRot > 89f)
            xRot = 89f;
        else if (xRot < -89f)
            xRot = -89f;

        transform.position = target.position + Quaternion.Euler(xRot, yRot, 0f) * (currentCameraDistance * -Vector3.back);
        transform.LookAt(target.position, Vector3.up);
    }
}
