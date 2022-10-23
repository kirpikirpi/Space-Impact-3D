using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BasicEnemy : Spaceship
{
    private float maxDetectionDistance = 70f;
    private float movementSpeed = 0.025f;
    public LayerMask engagebleTargets;

    private float randomShotTimeRange = 10f;
    private float timeToNextShot = 0;
    private float randomShotTime;

    void Start()
    {
        hp = 5;
        ep = 25;
        randomShotTime = Random.Range(0, randomShotTimeRange);
       
        SetupModules();
    }

    void Update()
    {
        gameObject.transform.position = new Vector3(transform.position.x,
            transform.position.y,
            transform.position.z - movementSpeed);
        
        if (Time.time > timeToNextShot)
        {
            ep = OffenseModule.ActivateOffense(ep);
            randomShotTime = Random.Range(0, randomShotTimeRange);
            timeToNextShot = Time.time + randomShotTime;
        }
    }

    void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, maxDetectionDistance, engagebleTargets))
        {
            ep = OffenseModule.ActivateOffense(ep);
        }
    }

    public override void OnDestroy()
    {
        gameObject.SetActive(false);
    }

    public override void OnHit()
    {
        ep = OffenseModule.ActivateOffense(ep);
    }
}