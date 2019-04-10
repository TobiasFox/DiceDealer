using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DiceSpawner : MonoBehaviour
{
    [SerializeField] Vector3 randomForcePower;
    [SerializeField] private float autoSpawnWaitTime;
    private Transform spawnpoint;
    private ObjectPool objectPool;
    [SerializeField] bool toggleAutospawn = false;
    [SerializeField] private AutoSpawnConfiguration[] autoSpawnPoints;

    [Serializable]
    private struct AutoSpawnConfiguration
    {
        public Transform spawnPoint;
        public Vector3 forceMin;
        public Vector3 forceMax;
    }

    void Start()
    {
        spawnpoint = transform.GetChild(0);
        objectPool = FindObjectOfType<ObjectPool>();

        if(toggleAutospawn)
        {
            StartCoroutine(AutoSpawn());
        }
    }

    private IEnumerator AutoSpawn()
    {
        while (true)
        {
            var autoSpawnPoint = autoSpawnPoints[Random.Range(0,autoSpawnPoints.Length)];
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

            yield return new WaitForSeconds(autoSpawnWaitTime);
        }
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended))
        {
            SpawnCube();
        }
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