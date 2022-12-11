using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : Spaceship
{
    private int startHealth = 50;
    private int startEnergy = 50;
    private float bulletSpeed = 30f;
    
    bool isShooting;
    bool isBlocking;

    private float energyRegenerationTime = 0.5f;
    private int epRegenerationValue = 1;
    private float nextRegeneration = 0;

    void Start()
    {
        hp = startHealth;
        ep = startEnergy;

        SetupModulesWithSpeed(bulletSpeed);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isBlocking)
        {
            ep = OffenseModule.ActivateOffense(ep);
        }

        if (Input.GetKey(KeyCode.E))
        {
            int newEp = DefenseModule.ActivateDefense(ep);
            ep = newEp <= startEnergy*2 ? newEp : startEnergy;
            isBlocking = true;
            print("energy: " + ep + " hp: " + hp);
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            DefenseModule.DeactivateDefense();
            isBlocking = false;
        }

        RegenerateEnergy();
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
        //gameObject.SetActive(false);
        rb.constraints = RigidbodyConstraints.None;
        rb.useGravity = true;
    }

    public override void OnHit()
    {
        print("energy: " + ep + " hp: " + hp);
    }
}