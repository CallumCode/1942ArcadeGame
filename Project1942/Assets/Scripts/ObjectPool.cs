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

    public bool finishedPooling = false;
    void Awake()
    {
        instance = this;
    }


    // Use this for initiaslization
    void Start()
    {
        PoolObjects();
    }

   

    void PoolObjects()
    {
        poolList = new List<List<GameObject>>();

        int prefabCount = prefabList.Count;
        for (int i = 0; i < prefabCount; i++)
        {
            poolList.Add(GetPoolForPrefab(i));
        }
        finishedPooling = true;
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
        List<GameObject> pool = null;
        if (poolList != null)
        {
            pool = poolList[index];
        } 
        else
        {
            Debug.Log("poolList is null");
        }

        if (pool != null)
        {
            int objectCount = pool.Count;

            for (int i = 0; i < objectCount; i++)
            {
                if (pool[i].activeInHierarchy == false)
                {
                    return pool[i];
                }
            }
        }
        else
        {
            Debug.Log(" pool null" + prefabList[index].name);
        }


        if (inActiveObject == null)
        {
            inActiveObject = GetAdditionalObject(prefabList[index]);
            pool.Add(inActiveObject);
            Debug.Log("added " + prefabList[index]);  
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

    GameObject GetAdditionalObject(GameObject prefab)
    {
        GameObject pooledObject = Instantiate(prefab, transform.position, transform.rotation) as GameObject;
 
        pooledObject.name = prefab.name + "(pooled)";

        return pooledObject;
    }
    

}
