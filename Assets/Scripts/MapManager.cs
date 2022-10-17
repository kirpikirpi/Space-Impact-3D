using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public GameObject PlayerGameObject;
    public GameObject EnemyGameObject;

    private float tunnelRadius = 15f;
    private float tunnelLength = 100f;
    
    private int numEnemies = 5;

    void Start()
    {
        Instantiate(PlayerGameObject, transform.position, Quaternion.identity);
        
        Quaternion enemyRotation = Quaternion.Euler(0,180,0);
        for (int i = 0; i < numEnemies; i++)
        {
            Instantiate(EnemyGameObject, GenerateRandomPosition(), enemyRotation);
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
