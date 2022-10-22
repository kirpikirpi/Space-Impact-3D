using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : Spaceship
{
    private int startHealth = 100;
    private int startEnergy = 100;

    public GameObject muzzle;
    private Rigidbody rb;
    bool isShooting;
    bool isBlocking;
    private float speed = 5f;

    private float energyRegenerationTime = 0.5f;
    private int epRegenerationValue = 1;
    private float nextRegeneration = 0;

    void Start()
    {
        hp = startHealth;
        ep = startEnergy;

        if (OffenseModulePrefab == null)
        {
            throw new Exception("Offensive module not attached!");
        }

        if (DefenseModulePrefab == null)
        {
            throw new Exception("Defensive module not attached!");
        }

        if (muzzle == null)
        {
            throw new Exception("No muzzle assigned in player ship!");
        }

        rb = gameObject.GetComponent<Rigidbody>();

        if (rb == null)
        {
            throw new Exception("no rigidbody attached to player ship!");
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
        if (Input.GetKeyDown(KeyCode.Space) && !isBlocking)
        {
            ep = OffenseModule.ActivateOffense(ep, muzzle);
        }

        if (Input.GetKey(KeyCode.E))
        {
            int newEp = DefenseModule.ActivateDefense(ep);
            ep = newEp <= startEnergy ? newEp : startEnergy;
            isBlocking = true;
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            DefenseModule.DeactivateDefense();
            isBlocking = false;
        }

        RegenerateEnergy();
    }

    void FixedUpdate()
    {
        Vector3 m_Input = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        rb.MovePosition(transform.position + m_Input * Time.deltaTime * speed);
    }

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
        gameObject.SetActive(false);
    }

    public override void OnHit()
    {
    }
}