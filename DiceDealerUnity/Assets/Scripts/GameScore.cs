using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScore : MonoBehaviour
{
    private static GameObject INSTANCE;

    [Tooltip("The maximum of eyes a dice can have to instantiate the eyes count array")]
    [SerializeField]
    private int maxDiceEyes = 6;

    private int gameScore = 0;
    private int[] diceEyeCount;
    private UIController uiController;
    private DiceSpawner diceSpawner;

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
        diceEyeCount = new int[maxDiceEyes];
        uiController = FindObjectOfType<UIController>();
        diceSpawner = FindObjectOfType<DiceSpawner>();
    }

    public void AddScore(int diceEyes)
    {
        diceEyeCount[diceEyes] += 1;
        gameScore += diceEyes;

        uiController.UpdateScore(gameScore);
        //Display gamescore
        //Debug.Log(diceEyes + " was countet: " + diceEyeCount[diceEyes] +"x");
        //Debug.Log("Gamescore: " + gameScore);
    }

    public void ResetScore()
    {
        gameScore = 0;
        uiController.UpdateScore(gameScore);

        var activeDices = FindObjectsOfType<DiceRepooler>();
        if (activeDices != null)
        {
            foreach (var activeDice in activeDices)
            {
                activeDice.ResetDice();
            }
        }
    }

    public void BuyUpgrade(Upgrade upgrade)
    {
        if (gameScore >= upgrade.price)
        {
            gameScore -= upgrade.price;
            diceSpawner.autoSpawnWaitTime *= upgrade.upgradeMultiplier;
            upgrade.CalculateNextUpgradePrice();
        }
    }
}