using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

public class DiceSpawner : MonoBehaviour
{
    public float autoSpawnWaitTime { get; set; }
    [SerializeField] Vector3 randomForcePower;
    [SerializeField] private AutoSpawnConfiguration[] autoSpawnPoints;
    private UIController uiController;
    private Transform spawnpoint;
    private ObjectPool objectPool;
    private bool isAutoSpawn;
    private float currentAutoSpawnValue;


    [Serializable]
    private struct AutoSpawnConfiguration
    {
        public Transform spawnPoint;
        public Vector3 forceMin;
        public Vector3 forceMax;
    }

    private void Start()
    {
        autoSpawnWaitTime = 3f;
        spawnpoint = transform.GetChild(0);
        objectPool = FindObjectOfType<ObjectPool>();
        uiController = FindObjectOfType<UIController>();
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
            SpawnCube(spawnpoint.position, -randomForcePower, randomForcePower);
        }
    }

    private void AutoSpawnCube()
    {
            currentAutoSpawnValue = Mathf.MoveTowards(currentAutoSpawnValue, autoSpawnWaitTime, Time.deltaTime);

            if (currentAutoSpawnValue >= autoSpawnWaitTime)
            {
                var autoSpawnPoint = autoSpawnPoints[Random.Range(0, autoSpawnPoints.Length)];
                SpawnCube(autoSpawnPoint.spawnPoint.position, autoSpawnPoint.forceMin, autoSpawnPoint.forceMax);
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

    private void SpawnCube(Vector3 position, Vector3 forceMin, Vector3 forceMax)
    {
        GameObject dice = objectPool.GetOrInstantiateDice(PoolName.D6, position, Quaternion.identity);
        var rb = dice.GetComponent<Rigidbody>();
        if (!rb)
        {
            return;
        }

        Vector3 forceVector = new Vector3(Random.Range(forceMin.x, forceMax.x), forceMax.y,
            Random.Range(forceMin.z, forceMax.z));
        rb.AddForce(forceVector, ForceMode.Impulse);
        rb.AddRelativeTorque(forceVector, ForceMode.Impulse);
    }

    public void SpawnCube()
    {
        SpawnCube(spawnpoint.position, -randomForcePower, randomForcePower);
    }
}