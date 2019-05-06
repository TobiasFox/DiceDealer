using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AutoSpawnButton : MonoBehaviour
{
    [SerializeField] private AutoSpawnSlider autoSpawnSlider;
    [SerializeField] private TextMeshProUGUI buttonPriceText;
    
    private Button button;
    private bool isAutoSpawnActivated;
    private DiceSpawner diceSpawner;
    private GameScore gameScore;

    private void Awake()
    {
        diceSpawner = FindObjectOfType<DiceSpawner>();
        gameScore = FindObjectOfType<GameScore>();
        button = GetComponent<Button>();
    }

    private void Start()
    {
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
        diceSpawner.ActivateAutoSpawn();
        gameScore.BuyUpgrade();
        UpdateButtonText();
    }

    private void UpdateButtonText()
    {
        buttonPriceText.text = gameScore.Upgrade.price.ToString();
    }

    public void CheckBuyingUpgrade(int score)
    {
        if (score > gameScore.Upgrade.price)
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