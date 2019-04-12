using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoSpawnButton : MonoBehaviour
{
    [SerializeField] private Color activatedColor;
    [SerializeField] private Color deactivatedColor;
    [SerializeField] private Upgrade upgrade;

    private Image autoSpawnButtonImage;
    private bool isAutoSpawnActivated;
    private DiceSpawner diceSpawner;
    private GameScore gameScore;
    private Text buttonText;

    void Start()
    {
        diceSpawner = FindObjectOfType<DiceSpawner>();
        gameScore = FindObjectOfType<GameScore>();
        autoSpawnButtonImage = GetComponent<Image>();
        buttonText = GetComponentInChildren<Text>();
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

        autoSpawnButtonImage.color = isAutoSpawnActivated ? activatedColor : deactivatedColor;
    }

    public void BuyUpgrade()
    {
        diceSpawner.ActivateAutoSpawn();
        gameScore.BuyUpgrade(upgrade);
        UpdateButtonText();
    }

    private void UpdateButtonText()
    {
        buttonText.text = "Auto Spawner" + "\n" + upgrade.price;
    }
}