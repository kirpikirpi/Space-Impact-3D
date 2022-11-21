using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public GameObject PlayerGameObject;
    public GameObject EnemyGameObject;
    private GameObject enemyHolder;

    private GameObject mapHolder;

    private int numEnemies = 50;
    private float enemySpawnOffset = 20f;
    
    private float playerCameraOffset = 7.5f;

    void Start()
    {
        
        Vector3 playerSpawnPos = new Vector3(0, -1.5f, playerCameraOffset*2);
        Instantiate(PlayerGameObject, playerSpawnPos, Quaternion.identity);

        
        enemyHolder = new GameObject("enemy holder");
        Quaternion enemyRotation = Quaternion.Euler(0, 180, 0);
        for (int i = 0; i < numEnemies; i++)
        {
            float zPos = enemySpawnOffset + enemySpawnOffset * i;
            Vector3 pos = GenerateRandomPosition(zPos);
            Instantiate(EnemyGameObject, pos, enemyRotation, enemyHolder.transform);
        }
        

        GameObject mainCamera = new GameObject("Main Camera");
        mainCamera.AddComponent<Camera>();
        mainCamera.transform.position = new Vector3(0, 0, playerCameraOffset);
        
        print("width: " + Screen.width + " height: " + Screen.height);
        
    }

    void Update()
    {
        
    }

    /*
     * creates random pos on a specific point on the z axis
     */
    Vector3 GenerateRandomPosition(float zPos)
    {
        float screenWidth = Screen.width/2;
        float screenHeight = Screen.height/2;
        float x = Random.Range(-screenWidth, screenWidth);
        float y = Random.Range(-screenHeight, screenHeight);
        float z = zPos;

        return new Vector3(x, y, z);
    }

}