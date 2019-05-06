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
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private AutoSpawnSlider autoSpawnSlider;
    [SerializeField] private AutoSpawnButton autoSpawnButton;
    [SerializeField] private AutoSpawnMultiplier autoSpawnMultiplier;
    [SerializeField] private GameObject statisticsPanel;

    private FloatTextSpawner floatTextSpawner;
    private Camera camera;
    public float randomComboTextSpawn = 20;

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

    internal void UpdateStatistics(int[] diceEyeCount)
    {
        for (int i = 0; i < statisticsPanel.transform.childCount; i++)
        {
            Transform child = statisticsPanel.transform.GetChild(i);
            TextMeshProUGUI text = child.GetComponent<TextMeshProUGUI>();
            text.text = diceEyeCount[i + 1].ToString() + "x";
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
        Vector2 screenPos = new Vector2(Screen.width / 2, Screen.height / 2) +
                            Vector2.one * (UnityEngine.Random.insideUnitSphere * randomComboTextSpawn);
        floatTextSpawner.SpawnFloatingText(text, screenPos, 8);
    }
}