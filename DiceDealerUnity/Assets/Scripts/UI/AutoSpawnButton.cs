﻿using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AutoSpawnButton : MonoBehaviour
{
    [SerializeField] private Upgrade upgrade;

    private TextMeshProUGUI buttonPriceText;
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
        buttonPriceText = GetComponentInChildren<TextMeshProUGUI>();
        
        upgradePrice = PlayerPrefs.GetInt(PlayerPrefsKey.AutoSpawnerPrice.ToString());
        if (upgradePrice <= 0)
        {
            upgradePrice = upgrade.price;
        }

        UpdateButtonText();
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
        button.interactable = score >= upgradePrice;
    }

    public bool IsAutoSpawnActive()
    {
        return diceSpawner.IsAutoSpawnActive();
    }
}