using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.SceneManagement;

public class GameScore : MonoBehaviour
{
    private static GameObject INSTANCE;

    [Tooltip("The maximum of eyes a dice can have to instantiate the eyes count array")] [SerializeField]
    private int maxDiceEyes = 6;

    [SerializeField] private Upgrade upgrade;

    public Upgrade Upgrade
    {
        get => upgrade;
        set => upgrade = value;
    }

    private Upgrade originUpgrade;
    private int gameScore = 0;
    private int[] diceEyeCount;
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
        uiController = FindObjectOfType<UIController>();
        diceSpawner = FindObjectOfType<DiceSpawner>();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
    {
        if (scene.buildIndex == 0)
        {
            Init();
        }
    }

    public void AddScore(int diceEyes)
    {
        diceEyeCount[diceEyes] += 1;
        gameScore += diceEyes;

        uiController.UpdateScore(gameScore);
        uiController.UpdateStatistics(diceEyeCount);
        //Display gamescore
        //Debug.Log(diceEyes + " was countet: " + diceEyeCount[diceEyes] +"x");
        //Debug.Log("Gamescore: " + gameScore);
    }

    public void ResetScore()
    {
        gameScore = 0;
        upgrade = originUpgrade;

        SceneManager.LoadScene(0);
        Init();
        uiController.UpdateScore(gameScore);

//        uiController.ResetAutoSpawner();
//
//        var activeDices = FindObjectsOfType<DiceRepooler>();
//        if (activeDices != null)
//        {
//            foreach (var activeDice in activeDices)
//            {
//                activeDice.ResetDice();
//            }
//        }
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