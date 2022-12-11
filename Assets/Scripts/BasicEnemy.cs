using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BasicEnemy : Spaceship
{
    private float maxDetectionDistance = 60f;
    private float movementSpeed = 30f;
    public LayerMask engagebleTargets;

    private float timeBetweenShots = 1.25f;
    private float timeToNextShot = 0;
    private bool targetDetected = false;

    void Start()
    {
        hp = 5;
        ep = 25;
        SetupModulesWithSpeed(movementSpeed);
    }

    void Update()
    {
        if (Time.time > timeToNextShot && targetDetected)
        {
            ep = OffenseModule.ActivateOffense(ep);
            timeToNextShot = Time.time + timeBetweenShots;
        }
    }

    void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, maxDetectionDistance, engagebleTargets))
        {
            targetDetected = true;
        }
        else
        {
            targetDetected = false;
        }

        if (!isDestroyed)
        {
            Vector3 pos = transform.position + transform.forward * Time.deltaTime * movementSpeed;
            rb.MovePosition(pos);
        }
    }

    public override void OnDestroy()
    {
        rb.constraints = RigidbodyConstraints.None; //error why todo???!
        rb.useGravity = true;
        isDestroyed = true;
        //Pooler.instance.PushPool(gameObject);
    }

    public override void OnHit()
    {
        //ep = OffenseModule.ActivateOffense(ep);
    }
}