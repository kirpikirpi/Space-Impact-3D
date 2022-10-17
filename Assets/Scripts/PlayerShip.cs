using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : Spaceship
{
    private int startHealth = 100;
    private int startEnergy = 100;
    
    public GameObject muzzle;
    bool isShooting;
    bool isBlocking;
    private float speed = 0.005f;

    private float energyRegenerationTime = 0.5f;
    private int epRegenerationValue = 1;
    private float nextRegeneration = 0;
    
    void Start()
    {
        hp = 100;
        ep = 100;

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

        OffenseModulePrefab = Instantiate(OffenseModulePrefab, transform.position, Quaternion.identity, gameObject.transform);
        DefenseModulePrefab = Instantiate(DefenseModulePrefab, transform.position, Quaternion.identity, gameObject.transform);

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
            ep = DefenseModule.ActivateDefense(ep);
            isBlocking = true;
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            DefenseModule.DeactivateDefense();
            isBlocking = false;
        }

        float h = Input.GetAxisRaw("Horizontal");

        gameObject.transform.position = new Vector3 (transform.position.x + (h * speed), transform.position.y, 
            transform.position.z);

        if (Time.time > nextRegeneration && ep < startEnergy)
        {
            ep += epRegenerationValue;
            nextRegeneration = Time.time + energyRegenerationTime;
            print("Energy: " + ep);
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
