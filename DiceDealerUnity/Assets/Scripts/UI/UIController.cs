using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    private static GameObject INSTANCE;
    
    private Text scoreText;
    private AutoSpawnSlider autoSpawnSlider;
    private AutoSpawnButton autoSpawnButton;

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

    public void SetScoreTextField(Text scoreText)
    {
        this.scoreText = scoreText;
    }

    public void SetAutoSpawnButton(AutoSpawnButton autoSpawnButton)
    {
        this.autoSpawnButton = autoSpawnButton;
    }

    public void SetAutoSpawnSlider(AutoSpawnSlider autoSpawnSlider)
    {
        this.autoSpawnSlider = autoSpawnSlider;
    }
}