using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public GameObject PlayerGameObject;
    public GameObject EnemyGameObject;
    public Camera mainCamera;
    private Pooler pooler;
    private FormationBuilder _formationBuilder;

    private GameObject mapHolder;

    private int numEnemies = 50;
    private float enemySpawnOffset = 20f;
    private float enemySpawnZ = 100f;
    private bool spawnActive = true;

    private float playerCameraOffset = 7.5f;

    void Start()
    {
        Vector3 playerSpawnPos = new Vector3(0, -5f, playerCameraOffset * 2);
        Instantiate(PlayerGameObject, playerSpawnPos, Quaternion.identity);
        
        if(mainCamera == null) throw new Exception("no Camera attached to map manager!");
        mainCamera.transform.position = new Vector3(0, 0, playerCameraOffset);
        mainCamera.transform.Rotate(20f, 0, 0);

        FormationType[] spawnFormations = new[]
        {
            FormationType.Diamond,
            FormationType.Wedge,
            FormationType.Column,
            FormationType.StackLeft,
            FormationType.StackRight,
            FormationType.Vee,
            FormationType.Line,
            FormationType.FileLeft,
            FormationType.FileRight,
            FormationType.FormationX
        };
        StartCoroutine(SpawnEnemies(numEnemies, spawnFormations));
    }

    void SpawnEnemies(int num)
    {
        pooler = gameObject.AddComponent<Pooler>();
        pooler = Pooler.instance;
        pooler.FillPool(EnemyGameObject, num);

        //rotate the enemy ships so they face player
        for (int i = 0; i < num; i++)
        {
            GameObject enemyShip = pooler.PopPool();
            enemyShip.transform.Rotate(Vector3.up, 180);
            pooler.PushPool(enemyShip);
        }

        StartCoroutine(EnemySpawner());
    }

    IEnumerator SpawnEnemies(int num, FormationType[] formationTypes)
    {
        _formationBuilder = gameObject.AddComponent<FormationBuilder>();
        Vector3 enemySpawnPoint = new Vector3(0, -5f, enemySpawnZ);

        foreach (var type in formationTypes)
        {
            GameObject formation = _formationBuilder.SpawnFormation(EnemyGameObject, EnemyGameObject, enemySpawnPoint,
                type);
            formation.transform.Rotate(Vector3.up, 180);
            
            yield return new WaitForSeconds(4);
        }
    }

    IEnumerator EnemySpawner()
    {
        while (spawnActive)
        {
            float fillstate = pooler.GetPoolFillstate();
            if (fillstate > 0)
            {
                GameObject enemy = pooler.PopPool();
                enemy.transform.position = new Vector3(0, -5f, enemySpawnZ);
                yield return new WaitForSeconds(4);
            }
            else
            {
                spawnActive = false;
            }
        }
    }
}