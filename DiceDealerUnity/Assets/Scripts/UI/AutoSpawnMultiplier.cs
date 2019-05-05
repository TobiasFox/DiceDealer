using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AutoSpawnMultiplier : MonoBehaviour
{
    [SerializeField] private float UpgradePriceMultiplier = 0.8f;
    [SerializeField] private TextMeshProUGUI buttonPriceText;
    [SerializeField] private TextMeshProUGUI buttonDiceCountText;

    private Button button;
    private bool isAutoSpawnActivated;
    private DiceSpawner diceSpawner;
    private GameScore gameScore;
    private int price = 100;
    private int boughtUpgrades = 0;
    private string buttonDiceCountLabelText;

    private void Start()
    {
        diceSpawner = FindObjectOfType<DiceSpawner>();
        gameScore = FindObjectOfType<GameScore>();
        button = GetComponent<Button>();
        var uiController = FindObjectOfType<UIController>();
        uiController.SetAutoSpawnMultiplier(this);
        buttonDiceCountLabelText = buttonDiceCountText.text;
        buttonDiceCountText.text += 0;
    }


    private void UpdateButtonText(string upgradePrice)
    {
        buttonPriceText.text = upgradePrice;
        buttonDiceCountText.text = buttonDiceCountLabelText + ++boughtUpgrades;
    }

    public void BuyUpgrade()
    {
        bool purchaceSuccsessful = gameScore.BuyUpgrade(price);
        if (purchaceSuccsessful)
        {
            diceSpawner.UpgradeAutospawnCount();
            price += (int) (price * UpgradePriceMultiplier);
            UpdateButtonText(price.ToString());
            button.interactable = false;
        }
    }

    public void CheckBuyingUpgrade(int score)
    {
        if (score >= price)
        {
            button.interactable = true;
        }
    }
}