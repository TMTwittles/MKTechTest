using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;
using Slider = UnityEngine.UI.Slider;
using Toggle = UnityEngine.UI.Toggle;

public class AccessibilityMenu : ColoredMenu
{
    [Header("Change color UI Elements")] 
    [SerializeField] private Dropdown selectColorDropdown;
    [SerializeField] private Image previewColorImage;
    [SerializeField] private Slider sliderR;
    [SerializeField] private Slider sliderG;
    [SerializeField] private Slider sliderB;
    [SerializeField] private Button resetColorButton;
    [SerializeField] private Button applyColorButton;

    [Header("Background color UI Elements")]
    [SerializeField] private Dropdown selectBackgroundDropdown;
    [SerializeField] private Toggle enableDynamicBackgroundToggle;

    [Header("UI Elements")]
    [SerializeField] private Button backButton;

    private Color customSliderColor;
    private string selectedColorName;
    private Color selectedColor;

    void Awake()
    {
        customSliderColor = new Color(1.0f, 1.0f, 1.0f);

        selectBackgroundDropdown.onValueChanged.AddListener(delegate { SelectBackgroundValueChanged(); });
        selectColorDropdown.onValueChanged.AddListener(delegate { SelectColorValueChanged(); } );
        
        sliderR.onValueChanged.AddListener(delegate { SliderColorChanged(); });
        sliderG.onValueChanged.AddListener(delegate { SliderColorChanged(); });
        sliderB.onValueChanged.AddListener(delegate { SliderColorChanged(); });
        resetColorButton.onClick.AddListener(delegate { OnPressResetColor(); });
        applyColorButton.onClick.AddListener(delegate { OnPressApplyColor(); });
        backButton.onClick.AddListener(delegate { OnPressBack(); });
        enableDynamicBackgroundToggle.onValueChanged.AddListener(delegate {OnToggleChange();});

        selectColorDropdown.options = new List<Dropdown.OptionData>();

        foreach (ColorData colorData in visualRandomColors.ColorList)
        {
            selectColorDropdown.options.Add(new Dropdown.OptionData(colorData.ColorName));
            selectBackgroundDropdown.options.Add(new Dropdown.OptionData(colorData.ColorName));
        }

        InitializeColorGroups();
    }

    void OnEnable()
    {
        UpdateColorGroupsRandomly();
        selectedColorName = selectColorDropdown.options[selectColorDropdown.value].text;
        UpdateSliders(data.CustomRandomColors.GetColorData(selectedColorName).ColorRGB);
        enableDynamicBackgroundToggle.isOn = data.EnableDynamicBackground;

        string currentBackgroundColorName = GameManager.Instance.BackgroundColorData.ColorName;
        for (int i = 0; i < selectBackgroundDropdown.options.Count; i++)
        {
            if (selectBackgroundDropdown.options[i].text == currentBackgroundColorName)
            {
                selectBackgroundDropdown.value = i;
                break;
            }
        }
    }

    // Simple method to handle updating sliders
    private void UpdateSliders(Color color)
    {
        customSliderColor.r = color.r;
        customSliderColor.g = color.g;
        customSliderColor.b = color.b;
        sliderR.value = color.r;
        sliderG.value = color.g;
        sliderB.value = color.b;
        previewColorImage.color = customSliderColor;
    }

    // Event called when new color is selected from the drop down
    private void SelectColorValueChanged()
    {
        selectedColorName = selectColorDropdown.options[selectColorDropdown.value].text;
        UpdateSliders(data.CustomRandomColors.GetColorData(selectedColorName).ColorRGB);
    }

    // Event called when any slider has been toggled/moved. Changes the preview image to match slider values.
    void SliderColorChanged()
    {
        customSliderColor.r = sliderR.value;
        customSliderColor.g = sliderG.value;
        customSliderColor.b = sliderB.value;
        previewColorImage.color = customSliderColor;
    }

    // Event called when new background color has been selected.
    void SelectBackgroundValueChanged()
    {
        selectedColorName = selectBackgroundDropdown.options[selectBackgroundDropdown.value].text;
        GameManager.Instance.UpdateBackgroundColor(data.CustomRandomColors.GetColorData(selectedColorName));
        UpdateColorGroupsRandomly();
    }

    // Resets the selected back to its initial color.
    void OnPressResetColor()
    {
        selectedColorName = selectColorDropdown.options[selectColorDropdown.value].text;
        selectedColor = data.DefaultRandomColors.GetColorData(selectedColorName).ColorRGB;
        data.CustomRandomColors.SetColorData(selectedColorName, selectedColor);
        UpdateSliders(selectedColor);
        GameManager.Instance.UpdateBackgroundColor(GameManager.Instance.BackgroundColorData);
        UpdateColorGroupsRandomly();
    }

    // Applies the slider values to the selected color. 
    void OnPressApplyColor()
    {
        selectedColorName = selectColorDropdown.options[selectColorDropdown.value].text;
        data.CustomRandomColors.SetColorData(selectedColorName, customSliderColor);
        GameManager.Instance.UpdateBackgroundColor(GameManager.Instance.BackgroundColorData);
        UpdateColorGroupsRandomly();
    }

    // Event called when toggle is pressed.
    void OnToggleChange()
    {
        data.EnableDynamicBackground = enableDynamicBackgroundToggle.isOn;
    }

    void OnPressBack()
    {
        // It is very expensive instantiating this menu, thus best to just disable it. 
        GameManager.Instance.GoBack(true);
    }




}
