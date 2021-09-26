using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Slider))]
public class SliderPercentage : MonoBehaviour
{
    private Slider targetedSlider;
    [SerializeField] TextMeshProUGUI sliderPercentageText;

    private void Awake()
    {
        targetedSlider = GetComponent<Slider>();
        targetedSlider.onValueChanged.AddListener(delegate
        {
            OnValueChangeUpdateText();
        });

        Invoke("OnValueChangeUpdateText", 0.005f);
    }



    public void OnValueChangeUpdateText()
    {
        float finalPercentage = targetedSlider.value / targetedSlider.maxValue * 100;
        
        sliderPercentageText.text = $"{(int)finalPercentage} %";
    }
}
