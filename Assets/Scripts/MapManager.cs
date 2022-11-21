using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public GameObject PlayerGameObject;
    public GameObject EnemyGameObject;
    private GameObject enemyHolder;

    public GameObject MapSegmentPrefab;
    private GameObject mapHolder;

    private float tunnelRadius = 6f;
    private float tunnelLength = 100f;
    private Vector3 tunnelEndPos;
    private float unitsBetweenCompleteSegments = 1.5f;
    private float segmentSpaceing = 0.2f;

    private List<GameObject> activeSegments;
    private float firstElementZ;
    private float lastElementZ;

    private int numEnemies = 50;
    private float enemySpawnOffset = 2f;

    private float playerSpeed = 0.2f;
    private float playerCameraOffset = 7.5f;

    void Start()
    {
        
        Vector3 playerSpawnPos = new Vector3(0, -1.5f, playerCameraOffset*2);
        Instantiate(PlayerGameObject, playerSpawnPos, Quaternion.identity);

        
        enemyHolder = new GameObject("enemy holder");
        Quaternion enemyRotation = Quaternion.Euler(0, 180, 0);
        for (int i = 0; i < numEnemies; i++)
        {
            Vector3 randomZ = new Vector3(0, -1.5f, i * 40 + 20);
            //Instantiate(EnemyGameObject, randomZ, enemyRotation, enemyHolder.transform);
        }
        

        GameObject mainCamera = new GameObject("Main Camera");
        mainCamera.AddComponent<Camera>();
        mainCamera.transform.position = new Vector3(0, 0, playerCameraOffset);
        
    }

    void Update()
    {
        
    }

    Vector3 GenerateRandomPosition()
    {
        float spawnRadius = tunnelRadius - enemySpawnOffset;
        float x = Random.Range(-spawnRadius, spawnRadius);
        float y = Random.Range(-spawnRadius, spawnRadius);
        float z = Random.Range(15f, tunnelLength);

        return new Vector3(x, y, z);
    }

}