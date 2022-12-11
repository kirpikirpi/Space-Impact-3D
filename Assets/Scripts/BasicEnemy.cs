using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BasicEnemy : Spaceship
{
    private float maxDetectionDistance = 60f;
    private float movementSpeed = 0f;
    public LayerMask engagebleTargets;

    private float timeBetweenShots = 5f;
    private float timeToNextShot = 0;
    private float randomShotTime;
    private bool targetDetected = false;

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

        if (Time.time > timeToNextShot && targetDetected)
        {
            ep = OffenseModule.ActivateOffense(ep);
            timeToNextShot = Time.time + randomShotTime;
        }
    }

    void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, maxDetectionDistance, engagebleTargets))
        {
            targetDetected = true;
            Debug.DrawLine(transform.position,
                hit.point, Color.red);
        }
        else
        {
            targetDetected = false;
        }
    }

    public override void OnDestroy()
    {
        //rb.constraints = RigidbodyConstraints.None; throws errors, why? todo
        //rb.useGravity = true;
        Pooler.instance.PushPool(gameObject);
    }

    public override void OnHit()
    {
        ep = OffenseModule.ActivateOffense(ep);
    }
}