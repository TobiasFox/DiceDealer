using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AutoSpawnMultiplier : MonoBehaviour
{

    //[SerializeField] private ParticleSystem activateParticleSystem;
    [SerializeField] private AutoSpawnSlider autoSpawnSlider;
    [SerializeField] private float UpgradePriceMultiplier = 0.8f;

    private Button button;
    private bool isAutoSpawnActivated;
    private DiceSpawner diceSpawner;
    private GameScore gameScore;
    private TextMeshProUGUI buttonPriceText;
    private int price = 100;

    private void Start()
    {
        diceSpawner = FindObjectOfType<DiceSpawner>();
        gameScore = FindObjectOfType<GameScore>();
        buttonPriceText = GetComponentInChildren<TextMeshProUGUI>();
        button = GetComponent<Button>();
        var uiController = FindObjectOfType<UIController>();
    }


    private void UpdateButtonText(string upgradePrice)
    {
        buttonPriceText.text = upgradePrice;
    }

    public void BuyUpgrade()
    {
        bool purchaceSuccsessful = gameScore.BuyUpgrade(price);
        if (purchaceSuccsessful)
        {
            diceSpawner.UpgradeAutospawnCount();
            price += (int)(price * UpgradePriceMultiplier);
            UpdateButtonText(price.ToString());
        }
    }


}
