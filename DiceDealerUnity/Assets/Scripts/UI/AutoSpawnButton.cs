using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoSpawnButton : MonoBehaviour
{
    private Button button;
    private bool isAutoSpawnActivated;
    private DiceSpawner diceSpawner;
    private GameScore gameScore;
    private Text buttonText;

    private void Start()
    {
        diceSpawner = FindObjectOfType<DiceSpawner>();
        gameScore = FindObjectOfType<GameScore>();
        buttonText = GetComponentInChildren<Text>();
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
        buttonText.text = "Auto Spawner" + "\n" + gameScore.Upgrade.price;
    }

    public void CheckBuyingUpgrade(int score)
    {
        if (score > gameScore.Upgrade.price)
        {
            button.interactable = true;
        }
    }
    
}