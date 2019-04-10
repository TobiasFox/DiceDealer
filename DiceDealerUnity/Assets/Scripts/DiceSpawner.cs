using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

public class DiceSpawner : MonoBehaviour
{
    [SerializeField] Vector3 randomForcePower;
    [SerializeField] private float autoSpawnWaitTime;
    [SerializeField] private AutoSpawnConfiguration[] autoSpawnPoints;
    [SerializeField] private UIController uiController;
    private bool isAutoSpawnActive;
    private Transform spawnpoint;
    private ObjectPool objectPool;
    [SerializeField] bool toggleAutospawn = false;
    private Coroutine activeAutoSpawnCoroutine;

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
        objectPool = FindObjectOfType<ObjectPool>();
        uiController = FindObjectOfType<UIController>();
    }

    public void ActivateAutoSpawn()
    {
        activeAutoSpawnCoroutine = StartCoroutine(AutoSpawn());
        isAutoSpawnActive = true;
    }

    public void DeactivateAutoSpawn()
    {
        StopCoroutine(activeAutoSpawnCoroutine);
        uiController.DeactivateSpawnSlider();
        isAutoSpawnActive = false;
    }

    private IEnumerator AutoSpawn()
    {
        uiController.ActivateAutoSpawnSlider(autoSpawnWaitTime);
        yield return new WaitForSeconds(autoSpawnWaitTime - uiController.GetCurrentSpawnTime());

        while (true)
        {
            var autoSpawnPoint = autoSpawnPoints[Random.Range(0, autoSpawnPoints.Length)];
            GameObject dice =
                objectPool.GetOrInstantiateDice(PoolName.D6, autoSpawnPoint.spawnPoint.position, Quaternion.identity);
            var rb = dice.GetComponent<Rigidbody>();
            if (rb)
            {
                Vector3 forceVector = new Vector3(Random.Range(autoSpawnPoint.forceMin.x, autoSpawnPoint.forceMax.x),
                    autoSpawnPoint.forceMin.y,
                    Random.Range(autoSpawnPoint.forceMin.z, autoSpawnPoint.forceMax.z));

                rb.AddForce(forceVector, ForceMode.Impulse);
                rb.AddRelativeTorque(forceVector, ForceMode.Impulse);
            }

            uiController.ActivateAndResetAutoSpawnSlider(autoSpawnWaitTime);
            yield return new WaitForSeconds(autoSpawnWaitTime);
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space) || IsTouched())
        {
            SpawnCube();
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

    public void SpawnCube()
    {
        GameObject dice = objectPool.GetOrInstantiateDice(PoolName.D6, spawnpoint.position, Quaternion.identity);
        var rb = dice.GetComponent<Rigidbody>();
        if (rb == null)
        {
            return;
        }

        rb.AddForce(
            new Vector3(Random.Range(-randomForcePower.x, randomForcePower.x), randomForcePower.y,
                Random.Range(-randomForcePower.z, randomForcePower.z)), ForceMode.Impulse);
        rb.AddRelativeTorque(
            new Vector3(Random.Range(-randomForcePower.x, randomForcePower.x), randomForcePower.y,
                Random.Range(-randomForcePower.z, randomForcePower.z)), ForceMode.Impulse);
    }
}