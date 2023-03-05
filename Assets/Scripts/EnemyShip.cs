using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyShip : Spaceship
{
    private float maxDetectionDistance = 60f;
    private float movementSpeedScouting = 30; //30
    private float movementSpeedSuspicious = 40; 
    private float movementSpeedCombat = 60;
    public LayerMask engagebleTargets;
    public LayerMask friendlyShips;
    public ParticleSystem ParticleSystemOnDestroy;

    public EnemyAI ai;
    private bool isShooting;
    private float distanceToFriendlyShips = 1f;
    private float distanceToPlayer = 40f;

    private float timeBetweenShots = 1.25f;
    private float timeToNextShot = 0;


    void Start()
    {
        hp = 5;
        ep = 25;
        SetupModules();
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
                print("Scouting" + gameObject.name);
                MovementSystem(movementSpeedScouting, ai.GetCurrentTarget());
                break;
            case Enums.AlertState.Suspicious:
                MovementSystem(movementSpeedScouting, ai.GetCurrentTarget());
                print("Suspicious" + gameObject.name);
                break;
            case Enums.AlertState.CombatMode:
                bool isInEnemyRange = MovementSystem(movementSpeedCombat, ai.GetCurrentTarget());
                isShooting = isInEnemyRange;
                print("Combat Mode" + gameObject+name); //shoot
                break;
        }
    }


    bool MovementSystem(float speed, GameObject targetPos)
    {
        bool isInEnemyRange = false;
        RaycastHit hit;
        if (!Physics.Raycast(transform.position, transform.forward, out hit, distanceToFriendlyShips, friendlyShips))
        {
            if (targetPos != null)
            {
                float distance = Vector3.Distance(transform.position, targetPos.transform.position);
                isInEnemyRange = distance <= distanceToPlayer;
            }

            if (!movementDisabled && !isInEnemyRange)
            {
                Vector3 pos = transform.position + transform.forward * Time.deltaTime * speed;
                rb.MovePosition(pos);
            }
        }

        return isInEnemyRange;
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

        //ai.OnDeathPing();  ToDo: this broken, needs fixing
    }

    public override void OnHit()
    {
        //ep = OffenseModule.ActivateOffense(ep);
    }
}