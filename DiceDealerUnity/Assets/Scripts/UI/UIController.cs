using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    private static GameObject INSTANCE;
    
    private TextMeshProUGUI comboText;
    private TextMeshProUGUI scoreText;
    private AutoSpawnSlider autoSpawnSlider;
    private AutoSpawnButton autoSpawnButton;
    public GameObject statisticsPanel;

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

    public void SetScoreTextField(TextMeshProUGUI scoreText)
    {
        this.scoreText = scoreText;
    }
    
    public void SetComboTextField(TextMeshProUGUI comboText)
    {
        this.comboText = comboText;
        comboText.text = "";
    }

    public void SetAutoSpawnButton(AutoSpawnButton autoSpawnButton)
    {
        this.autoSpawnButton = autoSpawnButton;
    }

    public void SetAutoSpawnSlider(AutoSpawnSlider autoSpawnSlider)
    {
        this.autoSpawnSlider = autoSpawnSlider;
    }

    internal void UpdateStatistics(int[] diceEyeCount)
    {
        for(int i = 0; i < statisticsPanel.transform.childCount; i++)
        {
            Transform child = statisticsPanel.transform.GetChild(i);
            TextMeshProUGUI text = child.GetComponent<TextMeshProUGUI>();
            text.text = diceEyeCount[i+1].ToString() +"x";
        }
    }

    public void ShowCombo(float comboMultiplier)
    {
//        Debug.Log("COMBO:  " + comboMultiplier);
        comboText.text = "COMBO:  " + comboMultiplier;
    }
}