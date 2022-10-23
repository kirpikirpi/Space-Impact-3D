using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : Spaceship
{
    private float maxDetectionDistance = 30f;
    private float movementSpeed = 0.025f;
    public LayerMask engagebleTargets;

    void Start()
    {
        hp = 5;
        ep = 25;
       
        SetupModules();
    }

    void Update()
    {
        gameObject.transform.position = new Vector3(transform.position.x,
            transform.position.y,
            transform.position.z - movementSpeed);
    }

    void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, maxDetectionDistance, engagebleTargets))
        {
            OffenseModule.ActivateOffense(ep, muzzle);
        }
    }

    public override void OnDestroy()
    {
        gameObject.SetActive(false);
    }

    public override void OnHit()
    {
        OffenseModule.ActivateOffense(ep, muzzle);
    }
}