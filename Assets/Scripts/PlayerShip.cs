using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : Spaceship
{
    private int startHealth = 100;
    private int startEnergy = 25;
    private float bulletSpeed = 30f;
    
    bool isShooting;
    bool isBlocking;

    private float energyRegenerationTime = 0.5f;
    private int epRegenerationValue = 1;
    private float nextRegeneration = 0;

    private float horizontalSpeed = 8;
    private float horizontalMovement;

    void Start()
    {
        hp = startHealth;
        ep = startEnergy;

        SetupModulesWithSpeed(bulletSpeed);
    }

    void Update()
    {
        UISingleton.instance.SetStats(ep.ToString(),hp.ToString());
        if(isDestroyed) return;
        if (Input.GetKeyDown(KeyCode.W) && !isBlocking)
        {
            ep = OffenseModule.ActivateOffense(ep);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            int newEp = DefenseModule.ActivateDefense(ep);
            ep = newEp <= startEnergy*2 ? newEp : startEnergy;
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

        horizontalMovement = Input.GetAxisRaw("Horizontal") * horizontalSpeed;


        RegenerateEnergy();
        
    }

    void FixedUpdate()
    {
        Vector3 currentPos = transform.position;
        Vector3 pos = new Vector3(currentPos.x + horizontalMovement * Time.deltaTime, currentPos.y, currentPos.z);
        rb.MovePosition(pos);
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
        isDestroyed = true;
        rb.constraints = RigidbodyConstraints.None;
        rb.useGravity = true;
    }

    public override void OnHit()
    {
    }
}