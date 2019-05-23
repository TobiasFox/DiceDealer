using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AutoSpawnButton : MonoBehaviour
{
    [SerializeField] private Upgrade upgrade;
    [SerializeField] private GameObject tutorialScreen;

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
        else
        {
            gameScore.tutorialModes[0] = TutorialMode.WAS_SHOWING;
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
            tutorialScreen.SetActive(false);
            gameScore.tutorialModes[0] = TutorialMode.WAS_SHOWING;
        }
    }

    private void UpdateButtonText()
    {
        buttonPriceText.text = upgradePrice.ToString();
    }

    public void CheckBuyingUpgrade(int score)
    {
        var upgradeBuyable = score >= upgradePrice;

        if (gameScore.tutorialModes[0] == TutorialMode.HIDDEN)
        {
            if (upgradeBuyable)
            {
                tutorialScreen.SetActive(true);
                gameScore.tutorialModes[0] = TutorialMode.SHOWING;
            }

            button.interactable = upgradeBuyable;
            return;
        }

        button.interactable = upgradeBuyable && gameScore.tutorialModes[1] != TutorialMode.SHOWING;
    }
}