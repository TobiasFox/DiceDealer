using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AutoSpawnMultiplier : MonoBehaviour
{
    [SerializeField] private Upgrade upgrade;
    [SerializeField] private TextMeshProUGUI buttonPriceText;
    [SerializeField] private TextMeshProUGUI buttonDiceCountText;

    private Button button;
    private bool isAutoSpawnActivated;
    private DiceSpawner diceSpawner;
    private GameScore gameScore;
    private int upgradePrice = 100;
    private int boughtUpgrades = 0;
    private string buttonDiceCountLabelText;

    private void Awake()
    {

        upgradePrice = PlayerPrefs.GetInt(PlayerPrefsKey.AutoSpawnMultiplierPrice.ToString());
        if (upgradePrice <= 0)
        {
            upgradePrice = upgrade.price;
        }
        UpdateButtonText();
        diceSpawner = FindObjectOfType<DiceSpawner>();
        gameScore = FindObjectOfType<GameScore>();
        button = GetComponent<Button>();
        buttonDiceCountLabelText = buttonDiceCountText.text;
        buttonDiceCountText.text += 0;
    }

    private void UpdateButtonText()
    {
        buttonPriceText.text = upgradePrice.ToString();
        buttonDiceCountText.text = buttonDiceCountLabelText + ++boughtUpgrades;
    }

    public void BuyUpgrade()
    {
        bool purchaseSuccsessful = gameScore.BuyUpgrade(upgradePrice);
        if (purchaseSuccsessful)
        {
            diceSpawner.UpgradeAutospawnCount();
            upgradePrice += (int) (upgradePrice * upgrade.priceMultiplier);
            PlayerPrefs.SetInt(PlayerPrefsKey.AutoSpawnMultiplierPrice.ToString(), upgradePrice);
            UpdateButtonText();
        }
    }

    public void CheckBuyingUpgrade(int score)
    {
        button.interactable = score >= upgradePrice;
    }
}