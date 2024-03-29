﻿using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameScore : MonoBehaviour
{
    private static bool isInitialized;
    private static GameObject INSTANCE;

    [Tooltip("The maximum of eyes a dice can have to instantiate the eyes count array")] [SerializeField]
    private int maxDiceEyes = 6;

    [SerializeField] private Upgrade upgrade;
    [SerializeField] private Combos combos;
    [SerializeField] private float comboCollectWaitTime = 0.5f;

    private float comboCollectedTime;
    private int gameScore;
    private int[] diceEyeCount;
    private int[] activeDiceEyes;
    private UIController uiController;
    private float comboScore;
    private float resetWaitTime = 0.3f;
    public TutorialMode[] tutorialModes { get; } = new TutorialMode[2];

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        diceEyeCount = new int[maxDiceEyes];
        activeDiceEyes = new int[maxDiceEyes];
        uiController = FindObjectOfType<UIController>();
        combos.InitializeDiceComboDict();

        gameScore = PlayerPrefs.GetInt(PlayerPrefsKey.GameScore.ToString(), 0);
        Debug.Log("loaded score: " + gameScore);
        if (gameScore == 0)
        {
            uiController.ShowTutorial();
        }

        uiController.UpdateScore(gameScore);
    }

    private void Update()
    {
        if (comboScore > 0 && comboCollectedTime < Time.timeSinceLevelLoad - comboCollectWaitTime)
        {
            comboCollectedTime = Time.timeSinceLevelLoad;

            uiController.ShowCombo(comboScore);
            gameScore += (int) comboScore;

            comboScore = 0;
        }
    }

    public void AddScore(int diceEyes, float timeToSpawnFloatText, Vector3 floatTextPosition)
    {
        diceEyeCount[diceEyes] += 1;
        gameScore += diceEyes;
        PlayerPrefs.SetInt(PlayerPrefsKey.GameScore.ToString(), gameScore);

        uiController.ShowScoreFloatText(diceEyes, floatTextPosition, timeToSpawnFloatText);
        uiController.UpdateScore(gameScore);
        uiController.UpdateStatistics(activeDiceEyes);
    }

    public void AddActiveDieEyes(int value, float time)
    {
        activeDiceEyes[value] += 1;
        var comboList = combos.CheckCombo(value, activeDiceEyes);
        foreach (var diceCombo in comboList)
        {
            Debug.Log(diceCombo.comboPoints);
            comboScore += diceCombo.comboPoints;
        }

        StartCoroutine(RemoveActiveDieEyesAfterTime(value, time));
    }

    private IEnumerator RemoveActiveDieEyesAfterTime(int value, float time)
    {
        yield return new WaitForSeconds(time);
        RemoveActiveDieEyes(value);
    }

    private void RemoveActiveDieEyes(int value)
    {
        activeDiceEyes[value] -= 1;
        uiController.UpdateStatistics(activeDiceEyes);
    }

    public void ResetScore()
    {
        StartCoroutine(ResetGame());
    }

    private IEnumerator ResetGame()
    {
        yield return new WaitForSeconds(resetWaitTime);
        gameScore = 0;
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        SceneManager.LoadScene(0);
    }

    public bool BuyUpgrade(int price)
    {
        if (gameScore >= price)
        {
            gameScore -= price;
            PlayerPrefs.SetInt(PlayerPrefsKey.GameScore.ToString(), gameScore);
            uiController.UpdateScore(gameScore);
            return true;
        }

        return false;
    }

    public void AddLoadedGameScore(int score)
    {
        gameScore += score;
        PlayerPrefs.SetInt(PlayerPrefsKey.GameScore.ToString(), gameScore);
        uiController.UpdateScore(gameScore);
    }

    public int GetActiveDice()
    {
        return activeDiceEyes.Sum();
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            PlayerPrefs.SetString(PlayerPrefsKey.LastTimestamp.ToString(), DateTime.Now.ToBinary().ToString());
            PlayerPrefs.Save();
        }
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetString(PlayerPrefsKey.LastTimestamp.ToString(), DateTime.Now.ToBinary().ToString());
    }
}