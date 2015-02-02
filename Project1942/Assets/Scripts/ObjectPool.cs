using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class ObjectPool : MonoBehaviour
{


    public List<GameObject> prefabList;
    List<List<GameObject>> poolList;

    public List<int> poolSizeList;


    public int defaultPoolSize = 10;


    public static ObjectPool instance { get; private set; }

    public GameObject container;

    void Awake()
    {
        instance = this;
    }


    // Use this for initiaslization
    void Start()
    {
        PoolObjects();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void PoolObjects()
    {
        poolList = new List<List<GameObject>>();

        int prefabCount = prefabList.Count;
        for (int i = 0; i < prefabCount; i++)
        {
            poolList.Add(GetPoolForPrefab(i));
        }
    }


    List<GameObject> GetPoolForPrefab(int index)
    {
        List<GameObject> pool = new List<GameObject>();
        int numToPool = defaultPoolSize;
        if (index < poolSizeList.Count)
        {
            numToPool = poolSizeList[index];
        }

        for (int i = 0; i < numToPool; i++)
        {
            GameObject pooledObject = Instantiate(prefabList[index], transform.position, transform.rotation) as GameObject;
            pooledObject.name = prefabList[index].name + "(pooled)";
            pooledObject.transform.SetParent(container.transform);
            pooledObject.SetActive(false);
            pool.Add(pooledObject);
        }

        return pool;
    }


    public GameObject GetObject(string name)
    {
        GameObject pooledObject = null;
        int prefabCount = prefabList.Count;

        for (int i = 0; i < prefabCount; i++)
        {

            if (string.Compare(prefabList[i].name, name) == 0)
            {

                pooledObject = GetInactive(i);
                break;
            }


        }

        if (pooledObject != null)
        {
            pooledObject.transform.SetParent(null);
            pooledObject.SetActive(true); // prehaps do this after moving of pos
        }
        else
        {
            Debug.Log(name + " failed pool get");
            Debug.DebugBreak();

        }

        return pooledObject;
    }

    GameObject GetInactive(int index)
    {
        GameObject inActiveObject = null;
        List<GameObject> pool = poolList[index];


        int objectCount = pool.Count;

        for (int i = 0; i < objectCount; i++)
        {
            if (pool[i].activeInHierarchy == false)
            {
                return pool[i];
            }
        }

        if (inActiveObject == null)
        {
            Debug.Log("ran out of " + prefabList[index].name);
        }

        return inActiveObject;
    }

    public void ReturnObject(GameObject pooledObject)
    {
        if (pooledObject != null && pooledObject.activeInHierarchy == true)
        {
            pooledObject.transform.SetParent(container.transform);
            pooledObject.SetActive(false);
        }
    }
}
