using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public GameObject PlayerGameObject;
    public GameObject EnemyGameObject;

    public GameObject MapSegmentPrefab;
    private GameObject mapHolder;

    private float tunnelRadius = 15f;
    private float tunnelLength = 100f;
    private float unitsBetweenCompleteSegments = 1.5f;
    private float segmentSpaceing = 0.1f;
    
    private int numEnemies = 15;
    private float enemySpawnOffset = 5f;

    void Start()
    {
        Instantiate(PlayerGameObject, transform.position, Quaternion.identity);
        
        Quaternion enemyRotation = Quaternion.Euler(0,180,0);
        for (int i = 0; i < numEnemies; i++)
        {
            Instantiate(EnemyGameObject, GenerateRandomPosition(), enemyRotation);
        }
        
        mapHolder = new GameObject("Map Holder");
        mapHolder.transform.parent = transform;

        for (float i = 0; i < tunnelLength; i+= unitsBetweenCompleteSegments)
        {
            for (float j = 0; j < 2 * Mathf.PI; j += segmentSpaceing)
            {
                Vector3 segmentPos = new Vector3(Mathf.Cos(j) * tunnelRadius, Mathf.Sin(j) * tunnelRadius, i);
                Instantiate(MapSegmentPrefab, segmentPos, Quaternion.identity, mapHolder.transform);
            }
        }
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
