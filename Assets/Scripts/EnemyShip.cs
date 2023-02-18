using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyShip : Spaceship
{
    private float maxDetectionDistance = 60f;
    private float movementSpeed = 15f; //30
    public LayerMask engagebleTargets;
    public LayerMask friendlyShips;
    public ParticleSystem ParticleSystemOnDestroy;

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
        if (Time.time > timeToNextShot && targetDetected && !isDestroyed)
        {
            ep = OffenseModule.ActivateOffense(ep, 0, null);
            timeToNextShot = Time.time + timeBetweenShots;
        }
    }

    void FixedUpdate()
    {
        if (isDestroyed) return;
        TargetSystem();
        if (!targetDetected) MovementSystem();
    }

    void TargetSystem()
    {
        RaycastHit hit;
        if (!Physics.Raycast(transform.position, transform.forward, out hit, maxDetectionDistance, friendlyShips))
        {
            if (Physics.Raycast(transform.position, transform.forward, out hit, maxDetectionDistance, engagebleTargets))
                targetDetected = true;
        }
        else
        {
            targetDetected = false;
        }
    }

    void MovementSystem()
    {
        if (!movementDisabled)
        {
            Vector3 pos = transform.position + transform.forward * Time.deltaTime * movementSpeed;
            rb.MovePosition(pos);
        }
    }

    public override void OnDestroy()
    {
        isDestroyed = true;
        UISingleton.instance.ActivateHitmarker();
        Collider shipCollider = gameObject.GetComponent<Collider>();
        MeshRenderer shipRenderer = gameObject.GetComponent<MeshRenderer>();
        shipCollider.enabled = false;
        shipRenderer.enabled = false;
        ParticleSystemOnDestroy.Play();
        movementSpeed = movementSpeed / 2;
        //particle system
        //Pooler.instance.PushPool(gameObject);
    }

    public override void OnHit()
    {
        //ep = OffenseModule.ActivateOffense(ep);
    }
}