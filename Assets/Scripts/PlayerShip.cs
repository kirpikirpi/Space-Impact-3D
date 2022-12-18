using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : Spaceship
{
    public ParticleSystem speedSystem;

    private int startHealth = 100;
    private int startEnergy = 25;
    private int maxEnergy = 100;
    private float bulletSpeed = 30f;

    private float secondaryFireInputTime = 0.4f;
    private float currentImputTime;

    bool isShooting;
    bool isBlocking;

    private float energyRegenerationTime = 1f;
    private int epRegenerationValue = 1;
    private float nextRegeneration = 0;

    private PlayerMovement _movement;

    void Start()
    {
        hp = startHealth;
        ep = startEnergy;

        SetupModulesWithSpeed(bulletSpeed);
        _movement = gameObject.AddComponent<PlayerMovement>();
        _movement.Setup(rb);

        if (speedSystem != null) speedSystem = Instantiate(speedSystem, transform.position, Quaternion.identity);
    }

    void Update()
    {
        UISingleton.instance.SetStats(ep.ToString(), hp.ToString());
        if (isDestroyed) return;

        if (Input.GetKeyDown(KeyCode.W))
        {
            currentImputTime = Time.time + secondaryFireInputTime;
        }

        if (Input.GetKeyUp(KeyCode.W) && !isBlocking)
        {
            if (Time.time < currentImputTime)
            {
                ep = OffenseModule.ActivateOffense(ep);
            }
            else
            {
                ep = OffenseModule.ActivateAlternativeOffense(ep);
            }
        }

        if (Input.GetKey(KeyCode.Space))
        {
            int newEp = DefenseModule.ActivateDefense(ep);
            ep = Mathf.Clamp(newEp, 0, maxEnergy);
            isBlocking = true;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            DefenseModule.DeactivateDefense();
            isBlocking = false;
        }

        RegenerateEnergy();
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
        _movement.SetActive(false);
        if (speedSystem != null) speedSystem.Pause();
    }

    public override void OnHit()
    {
    }
}