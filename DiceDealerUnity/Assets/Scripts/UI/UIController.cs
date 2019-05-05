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
    private AutoSpawnMultiplier autoSpawnMultiplier;
    private FloatTextSpawner floatTextSpawner;
    private Camera camera;
    public float randomComboTextSpawn = 20;

    public GameObject statisticsPanel { get; set; }

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

    private void Start()
    {
        floatTextSpawner = GetComponent<FloatTextSpawner>();
        camera = Camera.main;
    }

    public void UpdateScore(int score)
    {
        scoreText.text = score.ToString();
        autoSpawnButton.CheckBuyingUpgrade(score);
        autoSpawnMultiplier.CheckBuyingUpgrade(score);
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
    
    public void SetAutoSpawnMultiplier(AutoSpawnMultiplier autoSpawnMultiplier)
    {
        this.autoSpawnMultiplier = autoSpawnMultiplier;
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

    internal void ShowScoreFloatText(int diceEyes, Vector3 position, float timeToSpawnFloatText)
    {
        string text = "+" + diceEyes;
        Vector2 screenPos = camera.WorldToScreenPoint(position);
        floatTextSpawner.SpawnFloatingTextAfterTime(text, screenPos, 1, timeToSpawnFloatText);
    }

    public void ShowCombo(float comboMultiplier)
    {
//        Debug.Log("COMBO:  " + comboMultiplier);
        string text = "COMBO:  " + comboMultiplier;
        Vector2 screenPos = new Vector2(Screen.width / 2, Screen.height / 2) + Vector2.one *(UnityEngine.Random.insideUnitSphere * randomComboTextSpawn);
        floatTextSpawner.SpawnFloatingText(text, screenPos, 8);
    }
}