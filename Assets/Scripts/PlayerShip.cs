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

    private float secondaryFireInputTime = 0.6f; //salvo length
    private float currentImputTime;

    bool isShooting;
    bool isBlocking;
    private bool isRevengeShooting;

    private float energyRegenerationTime = 0.8f;
    private int epRegenerationValue = 1;
    private float nextRegeneration = 0;

    private PlayerMovement _movement;
    public TargetSystem playerTargetingSystem;
    private GameObject currentTarget;


    void Start()
    {
        hp = startHealth;
        ep = startEnergy;

        SetupModules();

        _movement = gameObject.AddComponent<PlayerMovement>();
        _movement.Setup(rb);

        if (playerTargetingSystem == null) throw new Exception("no target system attached to player!!");

        particleSystemPos = transform.position;
    }

    void Update()
    {
        UISingleton.instance.SetStats(ep.ToString(), hp.ToString());
        if (isDestroyed) return;

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            currentImputTime = Time.time + secondaryFireInputTime;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && !isBlocking)
        {
            if (isRevengeShooting)
            {
                ep = OffenseModule.ActivateOffense(ep, 1, currentTarget);
                isRevengeShooting = false;
                UISingleton.instance.DeactivateLockOn();
            }
            else if (Time.time < currentImputTime && !isRevengeShooting)
            {
                ep = OffenseModule.ActivateOffense(ep, 0, currentTarget); //ToDo: update current target
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            GameObject possibleTarget = playerTargetingSystem.SelectTarget();
            if (possibleTarget != null)
            {
                currentTarget = possibleTarget;
            }
        }


        if (Input.GetKey(KeyCode.Space))
        {
            int initialEp = ep;
            BlockInfo blockInfo = DefenseModule.ActivateDefense(ep);
            int newEp = blockInfo.energySpent;
            ep = Mathf.Clamp(newEp, 0, maxEnergy);
            isBlocking = true;
            if (newEp > initialEp)
            {
                currentTarget = blockInfo.aggressor;
                bool destroyed = currentTarget.GetComponent<Spaceship>().IsDestroyed();

                if (currentTarget == null || destroyed)
                {
                    currentTarget = playerTargetingSystem.GetCurrentTarget().gameObject;
                    ep = OffenseModule.ActivateOffense(ep, 1, currentTarget);
                }
                else
                {
                    playerTargetingSystem.SetCurrentTarget(currentTarget);
                    UISingleton.instance.ActivateLockOn(currentTarget.transform.position);
                    isRevengeShooting = true;
                    StartCoroutine(ResetRevengeLockOn());
                }
            }
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

    IEnumerator ResetRevengeLockOn()
    {
        yield return new WaitForSeconds(1.5f); //Todo: fix lock on cooldown
        isRevengeShooting = false;
        UISingleton.instance.DeactivateLockOn();
    }
}