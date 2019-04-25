using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceRepooler : MonoBehaviour
{
    public bool isInPool { get; set; }

    [SerializeField] private float repoolWaitTime = 1;

    private ObjectPool objectPool;

    private void Start()
    {
        objectPool = FindObjectOfType<ObjectPool>();
    }

    public float RepoolGameobject()
    {
        if (!isInPool)
        {
            StartCoroutine(ReepolObjectAfterTime());
            return repoolWaitTime;
        }
        return 0;
    }

    private IEnumerator ReepolObjectAfterTime()
    {
        yield return new WaitForSeconds(repoolWaitTime);
        isInPool = true;
        objectPool.EnqueueGameObject(PoolName.D6, gameObject);
    }

    
}