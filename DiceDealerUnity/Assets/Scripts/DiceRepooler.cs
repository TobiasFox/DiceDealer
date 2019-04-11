using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceRepooler : MonoBehaviour
{
    public bool isInPool { get; set; }

    [SerializeField] private float repoolWaitTime = 1;

    private ObjectPool objectPool;
    private Coroutine repoolCoroutine;


    private void Start()
    {
        objectPool = FindObjectOfType<ObjectPool>();
    }

    public void RepoolGameobject()
    {
        if (!isInPool)
        {
            repoolCoroutine = StartCoroutine(ReepolObjectAfterTime());
        }
    }

    private IEnumerator ReepolObjectAfterTime()
    {
        yield return new WaitForSeconds(repoolWaitTime);
        isInPool = true;
        objectPool.EnqueueGameObject(PoolName.D6, gameObject);
    }

    public void ResetDice()
    {
        if (repoolCoroutine != null)
        {
            StopCoroutine(repoolCoroutine);
        }

        if (!isInPool)
        {
            isInPool = true;
            objectPool.EnqueueGameObject(PoolName.D6, gameObject);
        }
    }
}