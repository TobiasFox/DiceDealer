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
    [SerializeField] private GameObject tutorialScreen;

    private Button button;
    private bool isAutoSpawnActivated;
    private DiceSpawner diceSpawner;
    private GameScore gameScore;
    private int upgradePrice = 100;
    private int boughtUpgrades = 1;
    private string buttonDiceCountLabelText;

    private void Awake()
    {
        diceSpawner = FindObjectOfType<DiceSpawner>();
        gameScore = FindObjectOfType<GameScore>();
        button = GetComponent<Button>();

        upgradePrice = PlayerPrefs.GetInt(PlayerPrefsKey.AutoSpawnMultiplierPrice.ToString());
        if (upgradePrice <= 0)
        {
            upgradePrice = upgrade.price;
        }

        if (PlayerPrefs.HasKey(PlayerPrefsKey.AutoSpawnCount.ToString()))
        {
            boughtUpgrades = PlayerPrefs.GetInt(PlayerPrefsKey.AutoSpawnCount.ToString());
        }

        var buttonInteractable = boughtUpgrades > 1;
        button.interactable = buttonInteractable;
        if (buttonInteractable)
        {
            gameScore.tutorialModes[1] = TutorialMode.WAS_SHOWING;
        }

        buttonDiceCountLabelText = buttonDiceCountText.text;
    }

    private void Start()
    {
        UpdateButtonText();
    }

    private void UpdateButtonText()
    {
        buttonPriceText.text = upgradePrice.ToString();
        buttonDiceCountText.text = buttonDiceCountLabelText + boughtUpgrades;
    }

    public void BuyUpgrade()
    {
        bool purchaseSuccsessful = gameScore.BuyUpgrade(upgradePrice);
        if (purchaseSuccsessful)
        {
            diceSpawner.UpgradeAutospawnCount();
            boughtUpgrades++;
            upgradePrice += (int) (upgradePrice * upgrade.priceMultiplier);
            PlayerPrefs.SetInt(PlayerPrefsKey.AutoSpawnMultiplierPrice.ToString(), upgradePrice);
            UpdateButtonText();
            tutorialScreen.SetActive(false);
            gameScore.tutorialModes[1] = TutorialMode.WAS_SHOWING;
        }
    }

    public void CheckBuyingUpgrade(int score)
    {
        if (gameScore.tutorialModes[0] != TutorialMode.WAS_SHOWING)
        {
            button.interactable = false;
            return;
        }

        var UpgradeBuyable = score >= upgradePrice;

        if (gameScore.tutorialModes[1] == TutorialMode.HIDDEN && UpgradeBuyable)
        {
            tutorialScreen.SetActive(true);
            gameScore.tutorialModes[1] = TutorialMode.SHOWING;
        }

        button.interactable = UpgradeBuyable;
    }
}