using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoSpawnButton : MonoBehaviour
{
    [SerializeField] private Color activatedColor;
    [SerializeField] private Color deactivatedColor;
    private Image autoSpawnButtonImage;
    private bool isAutoSpawnActivated;
    private DiceSpawner diceSpawner;

    void Awake()
    {
        diceSpawner = FindObjectOfType<DiceSpawner>();
        autoSpawnButtonImage = GetComponent<Image>();
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
}