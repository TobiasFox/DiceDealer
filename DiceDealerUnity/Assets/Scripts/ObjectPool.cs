using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ObjectPool : MonoBehaviour
{
    private Dictionary<PoolName, Queue<GameObject>> PoolDictionary { get; set; } =
        new Dictionary<PoolName, Queue<GameObject>>();

    [SerializeField] private List<Pool> pools;
    private readonly Dictionary<PoolName, Pool> poolInfos = new Dictionary<PoolName, Pool>();

    [Serializable]
    private class Pool
    {
        public PoolName poolName;
        public GameObject prefab;
        public int maxSize;
    }

    private void Awake()
    {
        foreach (var pool in pools)
        {
            poolInfos.Add(pool.poolName, pool);
            var objQueue = new Queue<GameObject>();
            for (var i = 0; i < pool.maxSize; i++)
            {
                var obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objQueue.Enqueue(obj);
            }

            PoolDictionary.Add(pool.poolName, objQueue);
        }
    }

    public GameObject GetOrInstantiateDice(PoolName poolName)
    {
        return GetOrInstantiateDice(poolName, Vector3.zero, Quaternion.identity);
    }


    public GameObject GetOrInstantiateDice(PoolName poolName, Vector3 position, Quaternion rotation)
    {
        var poolQueue = PoolDictionary[poolName];

        if (poolQueue.Count > 0)
        {
            var dice = PoolDictionary[poolName].Dequeue();
            dice.transform.position = position;
            dice.transform.rotation = rotation;
            dice.SetActive(true);

            dice.GetComponent<DiceRepooler>().isInPool = false;

            return dice;
        }

        var pool = poolInfos[poolName];
        if (pool == null)
        {
            return null;
        }

        var obj = Instantiate(pool.prefab, position, rotation);
        PoolDictionary[pool.poolName].Enqueue(obj);

        return obj;
    }

    public void EnqueueGameObject(PoolName poolName, GameObject obj)
    {
        var pool = poolInfos[poolName];
        if (pool == null)
        {
            return;
        }

        obj.SetActive(false);
        PoolDictionary[poolName].Enqueue(obj);
    }
}