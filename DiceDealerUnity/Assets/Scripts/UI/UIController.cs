using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    [SerializeField] private AutoSpawnSlider autoSpawnSlider;
    private static GameObject INSTANCE;

    private void Awake()
    {
        if (INSTANCE == null)
        {
            INSTANCE = gameObject;
            DontDestroyOnLoad(gameObject);
            return;
        }
        Destroy(gameObject);
    }


    public void UpdateScore(int score)
    {
        scoreText.text = score.ToString();
    }

    public void ActivateAutoSpawnSlider(float spawnTime)
    {
        autoSpawnSlider.SetSpawnTime(spawnTime);
        autoSpawnSlider.ActivateAutoSpawnSlider();
    }
    
    public void ActivateAndResetAutoSpawnSlider(float spawnTime)
    {
        autoSpawnSlider.SetSpawnTime(spawnTime);
        autoSpawnSlider.ActivateAndResetAutoSpawnSlider();
    }

    public void DeactivateSpawnSlider()
    {
        autoSpawnSlider.DeactivateAutoSpawnSlider();
    }

    public float GetCurrentSpawnTime()
    {
        return autoSpawnSlider.GetCurrentSpawnTime();
    }
}
