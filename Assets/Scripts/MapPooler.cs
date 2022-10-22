using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPooler : MonoBehaviour
{
    #region Singleton

    public static MapPooler instance;

    void Awake()
    {
        if (instance != null)
        {
            return;
        }

        instance = this;
    }

    #endregion

    private int poolSize;
    Queue<GameObject> mapSegmentPool = new Queue<GameObject>();
    private GameObject objectPoolParent;


    /**
     * get an object from the pool
     */
    public GameObject PopPool()
    {
        if (mapSegmentPool.Count <= 0) return null;
        GameObject currentObject = mapSegmentPool.Dequeue();
        return currentObject;
    }

    /**
     * push a game object on end of queue
     */
    public void PushPool(GameObject objectToPush)
    {
        mapSegmentPool.Enqueue(objectToPush);
    }

    /**
     * return how much percent of the pool are left. Between 1.0f and 0.0f
     */
    public float GetPoolFillstate()
    {
        int currentSize = mapSegmentPool.Count;
        float fillPercentage = (float) currentSize / poolSize;

        return fillPercentage;
    }

    /**
     * fills the object pool with deactivated game objects
     */
    public int FillPool(GameObject prefab, int count)
    {
        objectPoolParent = new GameObject("Object Pool");
        for (int i = 0; i < count; i++)
        {
            GameObject newPoolObject =
                Instantiate(prefab, transform.position, Quaternion.identity, objectPoolParent.transform);
            mapSegmentPool.Enqueue(newPoolObject);
            newPoolObject.SetActive(false);
        }

        poolSize = count;
        return mapSegmentPool.Count;
    }

    public int FillPool(GameObject prefab, GameObject parent, int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject newPoolObject = Instantiate(prefab, transform.position, Quaternion.identity, parent.transform);
            mapSegmentPool.Enqueue(newPoolObject);
            newPoolObject.SetActive(false);
        }

        poolSize = count;

        return mapSegmentPool.Count;
    }
}