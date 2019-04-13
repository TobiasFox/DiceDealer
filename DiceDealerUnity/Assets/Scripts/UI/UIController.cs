using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    [SerializeField] private AutoSpawnSlider autoSpawnSlider;
    [SerializeField] private AutoSpawnButton autoSpawnButton;
    private static GameObject INSTANCE;

    private void Awake()
    {
        if (INSTANCE == null)
        {
            INSTANCE = gameObject;
            DontDestroyOnLoad(gameObject);
            return;
        }
        Destroy(gameObject);
    }

    public void UpdateScore(int score)
    {
        scoreText.text = score.ToString();
        autoSpawnButton.CheckBuyingUpgrade(score);
    }

    public void SetAutoSpawnSliderValue(float sliderValue)
    {
        autoSpawnSlider.SetSliderValue(sliderValue);
    }
    
    public void SetAutoSpawnSliderMinMax(float min, float max)
    {
        autoSpawnSlider.SetSliderMinMax(min, max);
    }

    
}
