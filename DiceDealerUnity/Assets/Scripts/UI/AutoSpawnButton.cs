using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AutoSpawnButton : MonoBehaviour
{
    [SerializeField] private AutoSpawnSlider autoSpawnSlider;
    [SerializeField] private TextMeshProUGUI buttonPriceText;
    [SerializeField] private Upgrade upgrade;

    private Button button;
    private bool isAutoSpawnActivated;
    private DiceSpawner diceSpawner;
    private GameScore gameScore;
    private int upgradePrice;

    private void Awake()
    {
        diceSpawner = FindObjectOfType<DiceSpawner>();
        gameScore = FindObjectOfType<GameScore>();
        button = GetComponent<Button>();
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey(PlayerPrefsKey.LastTimestamp.ToString()))
        {
            var lastTimestamp = PlayerPrefs.GetString(PlayerPrefsKey.LastTimestamp.ToString());
            var timeDiff = DateTime.Now - DateTime.FromBinary(Convert.ToInt64(lastTimestamp));

            if (PlayerPrefs.HasKey(PlayerPrefsKey.AutoSpawnWaitTime.ToString()))
            {
                var autoSpawnWaitTime = PlayerPrefs.GetFloat(PlayerPrefsKey.AutoSpawnWaitTime.ToString());
                var score = (int) Math.Ceiling(timeDiff.TotalSeconds / autoSpawnWaitTime);
                gameScore.AddLoadedGameScore(score);
                Debug.Log("added gameScore: " + score);
            }
        }

        upgradePrice = PlayerPrefs.GetInt(PlayerPrefsKey.AutoSpawnerPrice.ToString());
        if (upgradePrice <= 0)
        {
            upgradePrice = upgrade.price;
        }

        UpdateButtonText();
    }

    public void ChangeAutoSpawnState()
    {
        isAutoSpawnActivated = !isAutoSpawnActivated;

        if (isAutoSpawnActivated)
        {
            diceSpawner.ActivateAutoSpawn();
        }
        else
        {
            diceSpawner.DeactivateAutoSpawn();
        }
    }

    public void BuyUpgrade()
    {
        bool purchaseSuccsessfull = gameScore.BuyUpgrade(upgradePrice);
        if (purchaseSuccsessfull)
        {
            diceSpawner.ActivateAutoSpawn();
            diceSpawner.UpgradeAutospawnWaitTime(upgrade.upgradeMultiplier);
            upgradePrice += (int) (upgrade.priceMultiplier * upgradePrice);
            PlayerPrefs.SetInt(PlayerPrefsKey.AutoSpawnerPrice.ToString(), upgradePrice);
            UpdateButtonText();
        }
    }

    private void UpdateButtonText()
    {
        buttonPriceText.text = upgradePrice.ToString();
    }

    public void CheckBuyingUpgrade(int score)
    {
        if (score >= upgradePrice)
        {
            button.interactable = true;
            autoSpawnSlider.SetEnableColor();
        }
        else
        {
            button.interactable = false;
            autoSpawnSlider.SetDisableColor();
        }
    }
}