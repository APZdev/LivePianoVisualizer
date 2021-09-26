using UnityEngine;
using UnityEngine.UI;

public class Switch : MonoBehaviour
{
    public bool isOn;
    public bool interactable;
    [SerializeField] private float toggleLerpTime = 0.1f;
    [SerializeField] private Color bodyOnColor;
    [SerializeField] private Color bodyOffColor;
    [SerializeField] private Color handleOnColor;
    [SerializeField] private Color handleOffColor;
    [SerializeField] private float handleOnPosition = 17.2f;
    [SerializeField] private float handleOffPosition =-17.2f;
    [SerializeField] private Image toggleBody;
    [SerializeField] private Image toggleHandle;

    private Button button;

    void Start()
    {
        button = GetComponent<Button>();
        UpdateToggleColor(isOn, 0);
    }

    void Update()
    {
        button.interactable = interactable;
        UpdateToggleColor(isOn, toggleLerpTime);
    }

    public void OnClick_ChangeState()
    {
        isOn = !isOn;
    }

    public void UpdateToggleColor(bool state, float lerpTime)
    {
        if(state)
        {
            if (toggleHandle.color != handleOnColor)
                toggleHandle.color = Color.Lerp(toggleHandle.color, handleOnColor, lerpTime);

            if(toggleBody.color != bodyOnColor)
                toggleBody.color = Color.Lerp(toggleBody.color, bodyOnColor, lerpTime);

            if (toggleHandle.transform.localPosition != new Vector3(handleOffPosition, 0, 0))
                toggleHandle.transform.localPosition = Vector3.Lerp(toggleHandle.transform.localPosition, new Vector3(handleOffPosition, 0, 0), lerpTime);
        }
        else
        {
            if (toggleHandle.color != handleOffColor)
                toggleHandle.color = Color.Lerp(toggleHandle.color, handleOffColor, lerpTime);

            if (toggleBody.color != bodyOffColor)
                toggleBody.color = Color.Lerp(toggleBody.color, bodyOffColor, lerpTime);

            if(toggleHandle.transform.localPosition != new Vector3(handleOnPosition, 0, 0))
                toggleHandle.transform.localPosition = Vector3.Lerp(toggleHandle.transform.localPosition, new Vector3(handleOnPosition, 0, 0), lerpTime);
        }
    }

}
