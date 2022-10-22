using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public GameObject PlayerGameObject;
    public GameObject EnemyGameObject;

    public GameObject MapSegmentPrefab;
    private GameObject mapHolder;

    private float tunnelRadius = 5f;
    private float tunnelLength = 100f;
    private float unitsBetweenCompleteSegments = 1.5f;
    private float segmentSpaceing = 0.1f;
    
    private int numEnemies = 15;
    private float enemySpawnOffset = 2f;

    private float playerSpeed = 0.01f;
    private float playerCameraOffset = 8.5f;

    void Start()
    {
        Vector3 playerSpawnPos = new Vector3(0,0,playerCameraOffset);
        Instantiate(PlayerGameObject, playerSpawnPos, Quaternion.identity);
        
        Quaternion enemyRotation = Quaternion.Euler(0,180,0);
        for (int i = 0; i < numEnemies; i++)
        {
            Instantiate(EnemyGameObject, GenerateRandomPosition(), enemyRotation);
        }
        
        mapHolder = new GameObject("Map Holder");
        mapHolder.transform.parent = transform;
        
        
        GameObject mainCamera = new GameObject("Main Camera");
        mainCamera.AddComponent<Camera>();

        for (float i = 0; i < tunnelLength; i+= unitsBetweenCompleteSegments)
        {
            for (float j = 0; j < 2 * Mathf.PI; j += segmentSpaceing)
            {
                Vector3 segmentPos = new Vector3(Mathf.Cos(j) * tunnelRadius, Mathf.Sin(j) * tunnelRadius, i);
                Instantiate(MapSegmentPrefab, segmentPos, Quaternion.identity, mapHolder.transform);
            }
        }
    }

    void FixedUpdate()
    {
        mapHolder.transform.position = new Vector3(mapHolder.transform.position.x,
            mapHolder.transform.position.y, mapHolder.transform.position.z - playerSpeed);
    }

    Vector3 GenerateRandomPosition()
    {
        float spawnRadius = tunnelRadius - enemySpawnOffset;
        float x = Random.Range(-spawnRadius, spawnRadius);
        float y = Random.Range(-spawnRadius, spawnRadius);
        float z = Random.Range(15f, tunnelLength);
        
        return new Vector3(x,y,z);
    }
}
