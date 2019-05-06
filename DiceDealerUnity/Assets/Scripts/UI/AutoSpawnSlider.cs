using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoSpawnSlider : MonoBehaviour
{
    private Slider autoSpawnSlider;
    [SerializeField] private Text timeTextField;
    [SerializeField] private Color enableColor;
    [SerializeField] private Color enableColorBackground;
    [SerializeField] private Color disableColor;
    [SerializeField] private Color disableColorBackground;
    [SerializeField] private Image fillImage;
    [SerializeField] private Image backgroundImage;
    
    private void Awake()
    {
        autoSpawnSlider = GetComponent<Slider>();
    }

    public void SetSliderValue(float sliderValue)
    {
        autoSpawnSlider.value = sliderValue;
        timeTextField.text = (autoSpawnSlider.value).ToString("F1");
    }

    public void SetSliderMinMax(float min, float max)
    {
        autoSpawnSlider.minValue = min;
        autoSpawnSlider.maxValue = max;
    }

    public void SetEnableColor()
    {
        fillImage.color = enableColor;
        backgroundImage.color = enableColorBackground;
    }
    
    public void SetDisableColor()
    {
        fillImage.color = disableColor;
        backgroundImage.color = disableColorBackground;
    }
    
}