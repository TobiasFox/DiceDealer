using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AutoSpawnButton : MonoBehaviour
{
    private Button button;
    private bool isAutoSpawnActivated;
    private DiceSpawner diceSpawner;
    private GameScore gameScore;
    private TextMeshProUGUI buttonPriceText;

    private void Start()
    {
        diceSpawner = FindObjectOfType<DiceSpawner>();
        gameScore = FindObjectOfType<GameScore>();
        buttonPriceText = GetComponentInChildren<TextMeshProUGUI>();
        button = GetComponent<Button>();
        var uiController = FindObjectOfType<UIController>();
        uiController.SetAutoSpawnButton(this);
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
        button.interactable = false;
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
        }
    }
    
}