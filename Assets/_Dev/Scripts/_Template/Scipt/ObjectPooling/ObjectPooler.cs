using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{

    [System.Serializable]
    public class Pool
    {
        public string name;
        public GameObject prefab;
        public int size;
        public GameObject customContainerPrefab;
    }
    public List<Pool> pools;
    public Dictionary<string,Queue<GameObject>> poolDics;

    public void Awake()
    {
        poolDics = new Dictionary<string,Queue<GameObject>>();
        foreach(var pool in pools)
        {
            GameObject container;
            if(pool.customContainerPrefab)
            {
                container = Instantiate(pool.customContainerPrefab, transform);
            }
            else
            {
                container = new GameObject(pool.name+" Container");
            }
            container.transform.parent = transform;
            poolDics[pool.name] = new Queue<GameObject>();
            for(int i = 0;i < pool.size; i++)
            {
                GameObject go = Instantiate(pool.prefab,container.transform);
                go.name = pool.name;
                go.SetActive(false);
                poolDics[pool.name].Enqueue(go);
            }
        }
    }

    [Button]
    public GameObject SpawnFromPool(string name,Vector3 position, Quaternion quaternion)
    {
        if (!poolDics.ContainsKey(name))    
        {
            Debug.LogError("There is no key name: " + name);
            return null;
        }
        GameObject go = poolDics[name].Dequeue();
        go.transform.position = position;
        go.transform.rotation = quaternion;
        go.SetActive(true);
        go.GetComponent<IPooler>()?.OnSpawn();
        return go;
    }

    public void DespawnObject(GameObject go)
    {
        if (!poolDics.ContainsKey(go.name))
        {
            Debug.LogError("There is no key name: " + go.name);
        }
        go.SetActive(false);
        go.GetComponent<IPooler>()?.OnDeSpawn();
        poolDics[go.name].Enqueue(go);  
    }
}
