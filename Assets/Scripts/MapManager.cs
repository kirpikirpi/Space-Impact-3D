using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public GameObject PlayerGameObject;
    public GameObject EnemyGameObject;

    public GameObject MapSegmentPrefab;

    private float tunnelRadius = 15f;
    private float tunnelLength = 100f;
    private float unitsBetweenCompleteSegments = 2f;
    private float segmentSpaceing = 0.1f;
    
    private int numEnemies = 5;

    void Start()
    {
        Instantiate(PlayerGameObject, transform.position, Quaternion.identity);
        
        Quaternion enemyRotation = Quaternion.Euler(0,180,0);
        for (int i = 0; i < numEnemies; i++)
        {
            Instantiate(EnemyGameObject, GenerateRandomPosition(), enemyRotation);
        }

        for (float i = 0; i < tunnelLength; i+= unitsBetweenCompleteSegments)
        {
            for (float j = 0; j < 2 * Mathf.PI; j += segmentSpaceing)
            {
                Vector3 segmentPos = new Vector3(Mathf.Cos(j) * tunnelRadius, Mathf.Sin(j) * tunnelRadius, i);
                Instantiate(MapSegmentPrefab, segmentPos, Quaternion.identity);
            }
        }
    }

    Vector3 GenerateRandomPosition()
    {
        float x = Random.Range(-tunnelRadius, tunnelRadius);
        float y = Random.Range(-tunnelRadius, tunnelRadius);
        float z = Random.Range(15f, tunnelLength);
        
        return new Vector3(x,y,z);
    }
}
