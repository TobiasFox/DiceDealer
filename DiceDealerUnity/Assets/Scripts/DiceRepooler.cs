using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceRepooler : MonoBehaviour
{
    public bool isInPool { get; set; }

    [SerializeField] private float repoolWaitTime = 1;

    private ObjectPool objectPool;
    private Coroutine repoolCoroutine;
    private float timeIntervall;
    private float spinTime = 5;
    private Rigidbody rb;
    public Vector3 spin { get; set; }
    public Vector3 moveVector { get; set; }

    private void Start()
    {
        objectPool = FindObjectOfType<ObjectPool>();
        rb = GetComponent<Rigidbody>();
        timeIntervall = Time.time + spinTime;
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
}