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
    [SerializeField] Vector3 randomForcePower;
    [SerializeField] private bool isAutoSpawn;
    [SerializeField] private AutoSpawnConfiguration[] autoSpawnPoints;
    [SerializeField] private PoolName autoSpawnDiceType = PoolName.D6;
    private UIController uiController;
    private Transform spawnpoint;
    private float currentAutoSpawnValue;
    private AudioManager audioManager;
    private int autospawnCount = 1;


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
        LoadPlayerPrefs();
    }

    private void LoadPlayerPrefs()
    {
        if (PlayerPrefs.HasKey(PlayerPrefsKey.AutospawnWaitTime.ToString()))
        {
            autoSpawnWaitTime = PlayerPrefs.GetFloat(PlayerPrefsKey.AutospawnWaitTime.ToString());
            ActivateAutoSpawn();
        }
        if (PlayerPrefs.HasKey(PlayerPrefsKey.AutoSpawnCount.ToString()))
        {
            autospawnCount = PlayerPrefs.GetInt(PlayerPrefsKey.AutoSpawnCount.ToString());
        }

    }

    public void ActivateAutoSpawn()
    {
        if (uiController != null)
        {
            uiController.SetAutoSpawnSliderMinMax(0, autoSpawnWaitTime);
        }
        isAutoSpawn = true;
    }

    public void DeactivateAutoSpawn()
    {
        isAutoSpawn = false;
    }

    public void UpgradeAutospawnCount(int addedDices = 1)
    {
        autospawnCount += addedDices;
        PlayerPrefs.SetInt(PlayerPrefsKey.AutoSpawnCount.ToString(), autospawnCount);
    }

    public void UpgradeAutospawnWaitTime(float multiplier)
    {
        autoSpawnWaitTime *= multiplier;
        PlayerPrefs.SetFloat(PlayerPrefsKey.AutospawnWaitTime.ToString(), autoSpawnWaitTime);
    }

    private void Update()
    {
        if (isAutoSpawn)
        {
            AutoSpawnCubes(autospawnCount);
        }

        if (Input.GetKey(KeyCode.Space) || IsTouched())
        {
            SpawnCube();
        }
    }

    private void AutoSpawnCubes(int count)
    {
        currentAutoSpawnValue = Mathf.MoveTowards(currentAutoSpawnValue, autoSpawnWaitTime, Time.deltaTime);
        uiController.SetAutoSpawnSliderValue(currentAutoSpawnValue);

        if (currentAutoSpawnValue >= autoSpawnWaitTime)
        {
            var autoSpawnPoint = autoSpawnPoints[Random.Range(0, autoSpawnPoints.Length)];

            for (int i = 0; i < count; i++)
            {
                SpawnCube(autoSpawnPoint.spawnPoint.position, autoSpawnPoint.forceMin, autoSpawnPoint.forceMax);
            }
            audioManager.Play("AutoSpawn");

            currentAutoSpawnValue = 0;
            uiController.SetAutoSpawnSliderMinMax(0, autoSpawnWaitTime);
        }

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