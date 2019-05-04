using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameScore : MonoBehaviour
{
    private static GameObject INSTANCE;

    [Tooltip("The maximum of eyes a dice can have to instantiate the eyes count array")] [SerializeField]
    private int maxDiceEyes = 6;
    [SerializeField] private Upgrade upgrade;
    [SerializeField] private Combos combos;

    public Upgrade Upgrade
    {
        get => upgrade;
        set => upgrade = value;
    }

    private Upgrade originUpgrade;
    private int gameScore = 0;
    private int[] diceEyeCount;
    private int[] activeDiceEyes;
    private UIController uiController;
    private DiceSpawner diceSpawner;

    private void Awake()
    {
        if (INSTANCE == null)
        {
            INSTANCE = gameObject;
            //DontDestroyOnLoad(gameObject);
            originUpgrade = Instantiate(upgrade) as Upgrade;
            SceneManager.sceneLoaded += OnSceneLoaded;
            return;
        }

        Destroy(gameObject);
    }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        diceEyeCount = new int[maxDiceEyes];
        activeDiceEyes = new int[maxDiceEyes];
        uiController = FindObjectOfType<UIController>();
        diceSpawner = FindObjectOfType<DiceSpawner>();
        combos.InitializeDiceComboDict();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
    {
        if (scene.buildIndex == 0)
        {
            Init();
        }
    }

    public void AddScore(int diceEyes, float timeToSpawnFloatText, Vector3 floatTextPosition)
    {
        diceEyeCount[diceEyes] += 1;
        gameScore += diceEyes;

        uiController.ShowScoreFloatText(diceEyes, floatTextPosition, timeToSpawnFloatText);

        uiController.UpdateScore(gameScore);
        uiController.UpdateStatistics(activeDiceEyes);
        //Display gamescore
        //Debug.Log(diceEyes + " was countet: " + diceEyeCount[diceEyes] +"x");
        //Debug.Log("Gamescore: " + gameScore);
    }

    public void AddActiveDieEyes(int value, float time)
    {
        activeDiceEyes[value] += 1;
        var comboList = combos.CheckCombo(value, activeDiceEyes);
        foreach (var diceCombo in comboList)
        {
            //uicontroller add effect for combo
            uiController.ShowCombo(diceCombo.comboMultiplier);
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
        gameScore = 0;
        upgrade.price = originUpgrade.price;
        upgrade.priceMultiplier = originUpgrade.priceMultiplier;
        upgrade.upgradeMultiplier = originUpgrade.upgradeMultiplier;
        
        SceneManager.LoadScene(0);
    }

    public void BuyUpgrade()
    {
        if (gameScore >= upgrade.price)
        {
            gameScore -= upgrade.price;
            diceSpawner.AutoSpawnWaitTime *= upgrade.upgradeMultiplier;
            upgrade.CalculateNextUpgradePrice();
        }
    }

    private void OnApplicationQuit()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}