using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceSpawner : MonoBehaviour
{
    [SerializeField] private GameObject dicePrefab;
    [SerializeField] Vector3 randomForcePower;
    private Transform spawnpoint;
    private ObjectPool objectPool;


    // Start is called before the first frame update
    void Start()
    {
        spawnpoint = transform.GetChild(0);
        objectPool = FindObjectOfType<ObjectPool>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space) || (Input.touchCount>0 && Input.GetTouch(0).phase==TouchPhase.Ended))
        {
            SpawnCube();
        }
    }

    public void SpawnCube()
    {
        GameObject dice = objectPool.GetOrInstantiateDice(PoolName.D6, spawnpoint.position, Quaternion.identity);
        var rb = dice.GetComponent<Rigidbody>();
        if (!rb)
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