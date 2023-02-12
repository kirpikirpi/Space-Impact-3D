using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : Spaceship
{
    public ParticleSystem speedSystem;
    private Vector3 particleSystemPos;

    private int startHealth = 100;
    private int startEnergy = 30;
    private int maxEnergy = 100;
    private float bulletSpeed = 30f;

    private float secondaryFireInputTime = 0.4f;
    private float currentImputTime;

    bool isShooting;
    bool isBlocking;

    private float energyRegenerationTime = 0.8f;
    private int epRegenerationValue = 1;
    private float nextRegeneration = 0;

    private PlayerMovement _movement;
    private TargetSystem playerTargetingSystem;

    void Start()
    {
        hp = startHealth;
        ep = startEnergy;

        SetupModulesWithSpeed(bulletSpeed);
        _movement = gameObject.AddComponent<PlayerMovement>();
        _movement.Setup(rb);

        if (playerTargetingSystem == null) playerTargetingSystem = gameObject.AddComponent<TargetSystem>();

        particleSystemPos = transform.position;
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

        speedSystem.transform.parent = null;
        speedSystem.transform.position = particleSystemPos;
    }

    public override void OnHit()
    {
    }
}