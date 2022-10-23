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
    private float timeToNextShot = 0;
    private float randomShotTime;

    void Start()
    {
        hp = 5;
        ep = 25;
        randomShotTime = Random.Range(0, 10f);
       
        SetupModules();
    }

    void Update()
    {
        gameObject.transform.position = new Vector3(transform.position.x,
            transform.position.y,
            transform.position.z - movementSpeed);
        
        if (Time.time > timeToNextShot)
        {
            ep = OffenseModule.ActivateOffense(ep, muzzle);
            randomShotTime = Random.Range(0, 10f);
            timeToNextShot = Time.time + randomShotTime;
        }
    }

    void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, maxDetectionDistance, engagebleTargets))
        {
            ep = OffenseModule.ActivateOffense(ep, muzzle);
        }
    }

    public override void OnDestroy()
    {
        gameObject.SetActive(false);
    }

    public override void OnHit()
    {
        ep = OffenseModule.ActivateOffense(ep, muzzle);
    }
}