using UnityEngine;
using TMPro;

public class WebcamItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI webcamName;
    private WebCamDevice webcamDevice;
    private Essentials essentials;

    void Start()
    {
        essentials = transform.root.GetComponent<Essentials>();
    }

    public void SetItem(string name)
    {
        webcamName.text = name;
    }

    public void OnClick_SelectCamera()
    {
        essentials.webcamSettings.SelectWebcamDevice(webcamName.text);
    }
}
