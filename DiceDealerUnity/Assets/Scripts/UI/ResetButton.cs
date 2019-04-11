using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetButton : MonoBehaviour
{
    private GameScore gameScore;
//    private DiceSpawner

    void Start()
    {
        gameScore = FindObjectOfType<GameScore>();
    }

    public void ResetScore()
    {
        gameScore.ResetScore();
    }
}