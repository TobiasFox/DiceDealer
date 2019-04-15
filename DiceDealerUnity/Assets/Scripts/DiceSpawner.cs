using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

public class DiceSpawner : MonoBehaviour
{
    [SerializeField] private float autoSpawnWaitTime;

    public float AutoSpawnWaitTime
    {
        get => autoSpawnWaitTime;
        set => autoSpawnWaitTime = value;
    }

    [SerializeField] Vector3 randomForcePower;
    [SerializeField] private bool isAutoSpawn;
    [SerializeField] private AutoSpawnConfiguration[] autoSpawnPoints;
    [SerializeField] private PoolName autoSpawnDiceType = PoolName.D6;
    private UIController uiController;
    private Transform spawnpoint;
    private float currentAutoSpawnValue;
    private AudioManager audioManager;

    [Serializable]
    private struct AutoSpawnConfiguration
    {
        public Transform spawnPoint;
        public Vector3 forceMin;
        public Vector3 forceMax;
    }

    private void Start()
    {
        spawnpoint = transform.GetChild(0);
        uiController = FindObjectOfType<UIController>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    public void ActivateAutoSpawn()
    {
        uiController.SetAutoSpawnSliderMinMax(0, autoSpawnWaitTime);
        isAutoSpawn = true;
    }

    public void DeactivateAutoSpawn()
    {
        isAutoSpawn = false;
    }

    private void Update()
    {
        if (isAutoSpawn)
        {
            AutoSpawnCube();
        }

        if (Input.GetKey(KeyCode.Space) || IsTouched())
        {
            SpawnCube();
        }
    }

    private void AutoSpawnCube()
    {
        currentAutoSpawnValue = Mathf.MoveTowards(currentAutoSpawnValue, autoSpawnWaitTime, Time.deltaTime);

        if (currentAutoSpawnValue >= autoSpawnWaitTime)
        {
            var autoSpawnPoint = autoSpawnPoints[Random.Range(0, autoSpawnPoints.Length)];
            SpawnCube(autoSpawnPoint.spawnPoint.position, autoSpawnPoint.forceMin, autoSpawnPoint.forceMax);
            audioManager.Play("AutoSpawn");

            currentAutoSpawnValue = 0;
            uiController.SetAutoSpawnSliderMinMax(0, autoSpawnWaitTime);
        }

        uiController.SetAutoSpawnSliderValue(autoSpawnWaitTime - currentAutoSpawnValue);
    }

    private bool IsTouched()
    {
        var touchCount = Input.touchCount > 0;
        if (!touchCount)
        {
            return false;
        }

        var touch = Input.GetTouch(0);
        return (!EventSystem.current.currentSelectedGameObject &&
                touch.phase == TouchPhase.Ended);
    }

    private int diceCounter;

    private void SpawnCube(Vector3 position, Vector3 forceMin, Vector3 forceMax)
    {
        var forceVector = new Vector3(Random.Range(forceMin.x, forceMax.x), 0,
            Random.Range(forceMin.z, forceMax.z));

        Dice.Roll(autoSpawnDiceType, autoSpawnDiceType + "-" + RandomColor(), position, forceVector);
    }

    public void SpawnCube()
    {
        SpawnCube(spawnpoint.position, -randomForcePower, randomForcePower);
        audioManager.spawnAudioSource.pitch = Random.Range(0.75f, 1.05f);
        audioManager.Play("Spawn");
    }

    private string RandomColor()
    {
        var color = "blue";
        var c = System.Convert.ToInt32(Random.value * 6);
        switch (c)
        {
            case 0:
                color = "red";
                break;
            case 1:
                color = "green";
                break;
            case 2:
                color = "blue";
                break;
            case 3:
                color = "yellow";
                break;
            case 4:
                color = "white";
                break;
            case 5:
                color = "black";
                break;
        }

        return color;
    }
}