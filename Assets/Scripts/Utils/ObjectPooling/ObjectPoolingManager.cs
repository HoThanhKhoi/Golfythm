using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolingManager : Singleton<ObjectPoolingManager>
{
    [System.Serializable]
    public class Pool
    {
        public string name;
        public GameObject prefab;
        public int size;
    }

    [SerializeField] private List<Pool> poolList;
    private Dictionary<string, Queue<GameObject>> poolDictionary = new Dictionary<string, Queue<GameObject>>();

    private void Start()
    {
        SetUpAllPool();
    }

    private void SetUpAllPool()
    {
        GameObject pooling = new GameObject("Pooling");

        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        foreach (Pool pool in poolList)
        {
            GameObject parentGameObject = new GameObject(pool.name + " parent");
            parentGameObject.transform.SetParent(pooling.transform);

            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab, parentGameObject.transform);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.name, objectPool);
        }
    }

    public GameObject SpawnFromPool(string poolName, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(poolName))
        {
            Debug.LogWarning("Pool with type " + poolName + " doesn't exist");
            return null;
        }


        GameObject objectToSpawn = poolDictionary[poolName].Dequeue();
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        poolDictionary[poolName].Enqueue(objectToSpawn);

        return objectToSpawn;
    }
}