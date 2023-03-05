using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyShip : Spaceship
{
    private float maxDetectionDistance = 60f;
    private float movementSpeed = 30; //30
    public LayerMask engagebleTargets;
    public LayerMask friendlyShips;
    public ParticleSystem ParticleSystemOnDestroy;

    public EnemyAI ai;
    private bool isShooting;
    private float distanceToFriendlyShips = 2f;

    private float timeBetweenShots = 1.25f;
    private float timeToNextShot = 0;


    void Start()
    {
        hp = 5;
        ep = 25;
        SetupModulesWithSpeed(movementSpeed);
        ai = GetComponent<EnemyAI>();
        if (ai == null) throw new Exception("No AI attached to: " + gameObject.name);
        //movementDisabled = true;
    }

    void Update()
    {
        if (Time.time > timeToNextShot && !isDestroyed && isShooting)
        {
            ep = OffenseModule.ActivateOffense(ep, 0, null);
            timeToNextShot = Time.time + timeBetweenShots;
        }
    }

    void FixedUpdate()
    {
        if (isDestroyed || ai == null) return;
        Enums.AlertState alertState = ai.AssesAlertState();
        switch (alertState)
        {
            case Enums.AlertState.Scouting:
                print("Scouting");
                MovementSystem(); //ToDo: more elaborate movement system
                break;
            case Enums.AlertState.Suspicious:
                print("Suspicious"); //move to formation space faster
                break;
            case Enums.AlertState.CombatMode:
                isShooting = true;
                print("Combat Mode"); //shoot
                break;
        }
    }


    void MovementSystem()
    {
        RaycastHit hit;
        if (!Physics.Raycast(transform.position, transform.forward, out hit, distanceToFriendlyShips, friendlyShips))
        {
            if (!movementDisabled)
            {
                Vector3 pos = transform.position + transform.forward * Time.deltaTime * movementSpeed;
                rb.MovePosition(pos);
            }
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
        //ToDo: callback to ai, alert ping

        //particle system
        //Pooler.instance.PushPool(gameObject);
    }

    public override void OnHit()
    {
        //ep = OffenseModule.ActivateOffense(ep);
    }
}