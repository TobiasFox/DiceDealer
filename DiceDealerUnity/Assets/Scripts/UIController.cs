using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    [SerializeField] private AutoSpawnSlider autoSpawnSlider;
    
    public void UpdateScore(int score)
    {
        scoreText.text = score.ToString();
    }

    public void StartAutoSpawnSlider(float spawnTime)
    {
        autoSpawnSlider.SetSpawnTime(spawnTime);
        autoSpawnSlider.ActivateAutoSpawnSlider();
    }

    public void DeactivateSpawnSlider()
    {
        autoSpawnSlider.DeactivateAutoSpawnSlider();
    }
}
