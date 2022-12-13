using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : Spaceship
{
    private int startHealth = 100;
    private int startEnergy = 25;
    private int maxEnergy = 100;
    private float bulletSpeed = 30f;

    bool isShooting;
    bool isBlocking;

    private float energyRegenerationTime = 0.5f;
    private int epRegenerationValue = 1;
    private float nextRegeneration = 0;

    private float horizontalSpeed = 8;
    private float horizontalConstraint = 5;
    private float horizontalMovement;
    private Vector3 spawnPosition;

    void Start()
    {
        hp = startHealth;
        ep = startEnergy;
        spawnPosition = transform.position;

        SetupModulesWithSpeed(bulletSpeed);
    }

    void Update()
    {
        UISingleton.instance.SetStats(ep.ToString(), hp.ToString());
        if (isDestroyed) return;
        if (Input.GetKeyDown(KeyCode.W) && !isBlocking)
        {
            ep = OffenseModule.ActivateOffense(ep);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            int newEp = DefenseModule.ActivateDefense(ep);
            ep = Mathf.Clamp(newEp, startEnergy, maxEnergy);
            isBlocking = true;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            DefenseModule.DeactivateDefense();
            isBlocking = false;
        }


        if (Input.GetKeyUp(KeyCode.Space))
        {
            DefenseModule.DeactivateDefense();
            isBlocking = false;
        }

        horizontalMovement = Input.GetAxisRaw("Horizontal");


        RegenerateEnergy();
    }

    void FixedUpdate()
    {
        Vector3 targetPos = Vector3.zero;
        Vector3 currentPos = transform.position;

        if (horizontalMovement > 0 || horizontalMovement < 0)
        {
            float xPos = currentPos.x + horizontalMovement * horizontalSpeed * Time.deltaTime;
            xPos = Mathf.Clamp(xPos, -horizontalConstraint, horizontalConstraint);
            targetPos = new Vector3(xPos, currentPos.y, currentPos.z);
        }
        else
        {
            Vector3 directionToTarget = spawnPosition - currentPos;
            float distanceToTarget = directionToTarget.magnitude;

            if (distanceToTarget <= 0.2f) targetPos = spawnPosition;
            else
            {
                float x = currentPos.x + directionToTarget.normalized.x * horizontalSpeed * Time.deltaTime;
                float y = currentPos.y + directionToTarget.normalized.y * horizontalSpeed * Time.deltaTime;
                float z = currentPos.z + directionToTarget.normalized.z * horizontalSpeed * Time.deltaTime;
                targetPos = new Vector3(x, y, z);
            }
        }

        rb.MovePosition(targetPos);
    }

    //regenerates to start energy
    void RegenerateEnergy()
    {
        if (Time.time > nextRegeneration && ep < startEnergy)
        {
            ep += epRegenerationValue;
            nextRegeneration = Time.time + energyRegenerationTime;
        }
    }


    public override void OnDestroy()
    {
        //gameObject.SetActive(false);
        isDestroyed = true;
        rb.constraints = RigidbodyConstraints.None;
        rb.useGravity = true;
    }

    public override void OnHit()
    {
    }
}