using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoSpawnSlider : MonoBehaviour
{
    private Slider autoSpawnSlider;
    [SerializeField] private Text timeTextField;

    private void Awake()
    {
        autoSpawnSlider = GetComponent<Slider>();
    }

    private void Start()
    {
        var uiController = FindObjectOfType<UIController>();
        uiController.SetAutoSpawnSlider(this);
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
}