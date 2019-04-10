using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoSpawnSlider : MonoBehaviour
{
    private Slider autoSpawnSlider;
    private bool isActiveAutoSpawn;
    private float spawnTime;
    [SerializeField] private Text timeTextField;
    [SerializeField] private float speedTime;

    void Awake()
    {
        autoSpawnSlider = GetComponent<Slider>();
        isActiveAutoSpawn = true;
    }

    void Update()
    {
        if (!isActiveAutoSpawn)
        {
            return;
        }

        autoSpawnSlider.value = Mathf.MoveTowards(autoSpawnSlider.value, spawnTime, Time.deltaTime * speedTime);
        timeTextField.text = (spawnTime - autoSpawnSlider.value).ToString("F1");

        if (autoSpawnSlider.value >= autoSpawnSlider.maxValue)
        {
            isActiveAutoSpawn = false;
        }
    }

    public void SetSpawnTime(float spawnTime)
    {
        this.spawnTime = spawnTime;
        autoSpawnSlider.value = 0;
        autoSpawnSlider.maxValue = spawnTime;
    }

    public void ActivateAutoSpawnSlider()
    {
        isActiveAutoSpawn = true;
    }

    public void DeactivateAutoSpawnSlider()
    {
        isActiveAutoSpawn = false;
    }
}