using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceSpawner : MonoBehaviour
{
    [SerializeField] private GameObject dicePrefab;
    [SerializeField] Vector3 randomForcePower;
    private Transform spawnpoint;

    // Start is called before the first frame update
    void Start()
    {
        spawnpoint = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            GameObject dice = Instantiate(dicePrefab, spawnpoint.position, Quaternion.identity);
            dice.GetComponent<Rigidbody>().AddForce(
                new Vector3(Random.Range(-randomForcePower.x, randomForcePower.x), randomForcePower.y, Random.Range(-randomForcePower.z, randomForcePower.z)), ForceMode.Impulse);
            dice.GetComponent<Rigidbody>().AddRelativeTorque(new Vector3(Random.Range(-randomForcePower.x, randomForcePower.x), randomForcePower.y, Random.Range(-randomForcePower.z, randomForcePower.z)), ForceMode.Impulse);
        }
    }
}
