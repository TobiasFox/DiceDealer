using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameScore : MonoBehaviour
{
    private static bool isInitialized;
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

    private static Upgrade originUpgrade;
    private int gameScore = 0;
    private int[] diceEyeCount;
    private int[] activeDiceEyes;
    private UIController uiController;
    private DiceSpawner diceSpawner;

    private void Awake()
    {
        if (originUpgrade == null)
        {
            originUpgrade = Instantiate(upgrade) as Upgrade;
        }
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

        if (PlayerPrefs.HasKey(PlayerPrefsKey.LastTimestamp.ToString()))
        {
            var lastTimestamp = PlayerPrefs.GetString(PlayerPrefsKey.LastTimestamp.ToString());
            var timeDiff = DateTime.Now - DateTime.FromBinary(Convert.ToInt64(lastTimestamp));

            //TODO: load other variables from PlayerPrefs
            if (PlayerPrefs.GetInt(PlayerPrefsKey.AutoSpawnerUpgradeLevel.ToString(), 0) > 0)
            {
                var autoSpawnDuration = PlayerPrefs.GetFloat(PlayerPrefsKey.AutoSpawnerDuration.ToString(), 3);

                gameScore = (int) Math.Ceiling(timeDiff.TotalSeconds / autoSpawnDuration);

                Debug.Log("loaded gamescore: " + gameScore);
            }
        }

        uiController.UpdateScore(gameScore);
    }

    public void AddScore(int diceEyes, float timeToSpawnFloatText, Vector3 floatTextPosition)
    {
        diceEyeCount[diceEyes] += 1;
        gameScore += diceEyes;

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
            uiController.UpdateScore(gameScore);
        }
    }

    public bool BuyUpgrade(int price)
    {
        if (gameScore >= price)
        {
            gameScore -= price;
            uiController.UpdateScore(gameScore);
            return true;
        }

        return false;
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetString(PlayerPrefsKey.LastTimestamp.ToString(), DateTime.Now.ToBinary().ToString());
    }
}