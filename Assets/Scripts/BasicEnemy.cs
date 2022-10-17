using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : Spaceship
{
    public GameObject muzzle;
    private float maxDetectionDistance = 30f;
    private float movementSpeed = 0.0025f;
    public LayerMask engagebleTargets;

    void Start()
    {
        hp = 100;
        ep = 50;
        if (OffenseModulePrefab == null)
        {
            throw new Exception("Offensive module not attached to enemy ship!");
        }

        if (DefenseModulePrefab == null)
        {
            throw new Exception("Defensive module not attached to enemy ship!");
        }

        if (muzzle == null)
        {
            throw new Exception("No muzzle assigned in enemy ship!");
        }

        OffenseModulePrefab = Instantiate(OffenseModulePrefab, transform.position, Quaternion.identity,
            gameObject.transform);
        DefenseModulePrefab = Instantiate(DefenseModulePrefab, transform.position, Quaternion.identity,
            gameObject.transform);

        OffenseModule = OffenseModulePrefab.GetComponent<IOffenseModule>();
        DefenseModule = DefenseModulePrefab.GetComponent<IDefenseModule>();
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