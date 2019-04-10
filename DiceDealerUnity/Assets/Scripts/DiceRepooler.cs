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

    public void RepoolGameobject()
    {
        if (!isInPool)
        {
            isInPool = true;
            StartCoroutine(ReepolObjectAfterTime());
            //objectPool.EnqueueGameObject(PoolName.D6, gameObject);
        }
    }

    private IEnumerator ReepolObjectAfterTime()
    {
        yield return new WaitForSeconds(repoolWaitTime);
        objectPool.EnqueueGameObject(PoolName.D6, gameObject);
    }
}
