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

    public GameObject mapSegmentPrefab;
    private int poolSize = 1000; //stable up to 2000 units max
    Queue<GameObject> mapSegmentPool = new Queue<GameObject>();

    void Start()
    {
        FillPool(mapSegmentPrefab, poolSize);
    }

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

    public int FillPool(GameObject prefab, int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject newPoolObject = Instantiate(prefab, transform.position, Quaternion.identity);
            mapSegmentPool.Enqueue(newPoolObject);
            newPoolObject.SetActive(false);
        }

        return mapSegmentPool.Count;
    }
}
