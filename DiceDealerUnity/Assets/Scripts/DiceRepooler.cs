using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceRepooler : MonoBehaviour
{
    public bool isInPool { get; set; }

    [SerializeField] private float repoolWaitTime = 1;
    [SerializeField] private float waitTimeDiceFactor = 1;

    private ObjectPool objectPool;

    private void Start()
    {
        objectPool = FindObjectOfType<ObjectPool>();
    }

    public float RepoolGameobject(float totalDices = 0f)
    {
        if (!isInPool)
        {
            float factorizedWaitTime = (repoolWaitTime / (waitTimeDiceFactor * totalDices));
            float newRepoolTime = totalDices > 0 && factorizedWaitTime < repoolWaitTime ? factorizedWaitTime : repoolWaitTime;
            StartCoroutine(ReepolObjectAfterTime(newRepoolTime));
            return newRepoolTime;
        }
        return 0;
    }

    private IEnumerator ReepolObjectAfterTime(float repoolTime)
    {
        yield return new WaitForSeconds(repoolTime);
        isInPool = true;
        objectPool.EnqueueGameObject(PoolName.D6, gameObject);
    }

    
}

