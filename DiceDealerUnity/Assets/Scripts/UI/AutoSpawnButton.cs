using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoSpawnButton : MonoBehaviour
{
    [SerializeField] private Upgrade upgrade;

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
        gameScore.BuyUpgrade(upgrade);
        UpdateButtonText();
        button.interactable = false;
    }

    private void UpdateButtonText()
    {
        buttonText.text = "Auto Spawner" + "\n" + upgrade.price;
    }

    public void CheckBuyingUpgrade(int score)
    {
        if (score > upgrade.price)
        {
            button.interactable = true;
        }
    }
}