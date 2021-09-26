using UnityEngine;
using UnityEngine.UI;

public class ColorSelection : MonoBehaviour
{
    public Color selectedColor;
    public bool currentSelected;

    private Essentials essentials;
    private Image selectedColorImage;

    void Start()
    {
        essentials = transform.root.GetComponent<Essentials>();
        selectedColorImage = GetComponent<Image>();
        selectedColorImage.color = selectedColor;
    }

    void Update()
    {
        currentSelected = essentials.colorPicker.colorSelection == this ? true : false;

        if (currentSelected)
        {
            selectedColor = essentials.colorPicker._color;
            selectedColorImage.color = selectedColor;

            //KeyCode test = essentials.menuManager.uiMenuActionMap["test"].performed;
            //Debug.Log(test);
            /*
            if (Input.GetButtonDown("Cancel"))
            {
                OnClick_OpenColorPicker();
            }
            */
        }
    }

    public void OnClick_OpenColorPicker()
    {
        currentSelected = essentials.colorPicker.colorSelection == this ? true : false;

        if (essentials.colorPickerObject.activeSelf && currentSelected)
        {
            //Turn off color picker
            essentials.colorPickerObject.SetActive(false);
            essentials.colorPicker.colorSelection = null;
        }
        else
        {
            //Turn on color picker
            essentials.colorPickerObject.SetActive(true);
            essentials.colorPickerObject.transform.position = Input.mousePosition;
            essentials.colorPicker.colorSelection = this;

            //Adjust color picker to the current color that wants to be changed
            essentials.colorPicker.R = selectedColorImage.color.r;
            essentials.colorPicker.G = selectedColorImage.color.g;
            essentials.colorPicker.B = selectedColorImage.color.b;
            essentials.colorPicker.RGBChanged();
            essentials.colorPicker.SendChangedEvent();
        }
    }
}
