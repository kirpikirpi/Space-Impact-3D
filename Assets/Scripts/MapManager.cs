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
    private float unitsBetweenCompleteSegments = 1.5f;
    private float segmentSpaceing = 0.1f;

    private List<GameObject> activeSegments;
    private float firstElementZ;
    private float lastElementZ;

    private int numEnemies = 50;
    private float enemySpawnOffset = 2f;

    private float playerSpeed = 0.08f;
    private float playerCameraOffset = 8.5f;

    void Start()
    {
        /*
        Vector3 playerSpawnPos = new Vector3(0, -2.5f, playerCameraOffset);
        Instantiate(PlayerGameObject, playerSpawnPos, Quaternion.identity);

        enemyHolder = new GameObject("enemy holder");
        Quaternion enemyRotation = Quaternion.Euler(0, 180, 0);
        for (int i = 0; i < numEnemies; i++)
        {
            Vector3 randomZ = new Vector3(0, -2.5f, i * 40 + 20);
            Instantiate(EnemyGameObject, randomZ, enemyRotation, enemyHolder.transform);
        }
        */

        GameObject mainCamera = new GameObject("Main Camera");
        mainCamera.AddComponent<Camera>();

        BuildMap();
        
    }

    void FixedUpdate()
    {
        mapHolder.transform.position = new Vector3(mapHolder.transform.position.x, mapHolder.transform.position.y,
            mapHolder.transform.position.z - playerSpeed);
        
        firstElementZ = activeSegments[0].transform.GetChild(0).transform.position.z;

        if (firstElementZ <= 0)
        {
            GameObject firstElement = activeSegments[0];
            Vector3 offset = new Vector3(0,0,unitsBetweenCompleteSegments);
            firstElement.transform.position = activeSegments[activeSegments.Count - 1].transform.position + offset;
            activeSegments.RemoveAt(0);
            activeSegments.Add(firstElement);
        }
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

    GameObject BuildSegment(float zPos)
    {
        GameObject circularSegment = new GameObject("circular segment");
        circularSegment.transform.parent = mapHolder.transform;

        for (float j = 0; j < 2 * Mathf.PI; j += segmentSpaceing)
        {
            Vector3 segmentPos = new Vector3(Mathf.Cos(j) * tunnelRadius, Mathf.Sin(j) * tunnelRadius, zPos);
            GameObject currentElement = Instantiate(MapSegmentPrefab,segmentPos, Quaternion.identity, circularSegment.transform);
            currentElement.gameObject.SetActive(true);
            currentElement.transform.position = segmentPos;
            currentElement.transform.parent = circularSegment.transform;
        }

        return circularSegment;
    }

    void BuildMap()
    {
        mapHolder = new GameObject("Map Holder");
        mapHolder.transform.parent = transform;
        activeSegments = new List<GameObject>();

        for (float i = 0; i < tunnelLength; i += unitsBetweenCompleteSegments)
        {
            GameObject currentSegement = BuildSegment(i);
            activeSegments.Add(currentSegement);
        }
    }
}