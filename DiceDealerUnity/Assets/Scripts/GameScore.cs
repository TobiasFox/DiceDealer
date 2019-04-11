using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScore : MonoBehaviour
{
    [Tooltip("The maximum of eyes a dice can have to instantiate the eyes count array")]
    [SerializeField] private int maxDiceEyes = 6;
    private int gameScore = 0;
    private int[] diceEyeCount;
    private UIController uiController;

    private void Start()
    {
        diceEyeCount = new int[maxDiceEyes];
        uiController = FindObjectOfType<UIController>();
    }

    public void AddScore(int diceEyes)
    {
        diceEyeCount[diceEyes] += 1;
        gameScore += diceEyes;

        uiController.UpdateScore(gameScore);
        //Display gamescore
//        Debug.Log(diceEyes + " was countet: " + diceEyeCount[diceEyes] +"x");
//        Debug.Log("Gamescore: " + gameScore);
    }
}
