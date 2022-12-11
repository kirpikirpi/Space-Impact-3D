using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public GameObject PlayerGameObject;
    public GameObject EnemyGameObject;
    private Pooler pooler;

    private GameObject mapHolder;

    private int numEnemies = 50;
    private float enemySpawnOffset = 20f;
    
    private float playerCameraOffset = 7.5f;

    void Start()
    {
        
        Vector3 playerSpawnPos = new Vector3(0, -1.5f, playerCameraOffset*2);
        Instantiate(PlayerGameObject, playerSpawnPos, Quaternion.identity);

        pooler = gameObject.AddComponent<Pooler>();
        pooler = Pooler.instance;
        pooler.FillPool(EnemyGameObject, 5);

        GameObject enemy = pooler.PopPool();
        enemy.transform.position = new Vector3(0,0,30);
       
        
        GameObject mainCamera = new GameObject("Main Camera");
        mainCamera.AddComponent<Camera>();
        mainCamera.transform.position = new Vector3(0, 0, playerCameraOffset);

    }

    void Update()
    {
        
    }

}