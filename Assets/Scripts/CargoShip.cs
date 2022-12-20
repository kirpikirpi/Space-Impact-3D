using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargoShip : Spaceship
{
    private float maxDetectionDistance = 60f;
    private float movementSpeed = 30f;
    public LayerMask engagebleTargets;
    public LayerMask friendlyShips;

    public GameObject shield;
    public ParticleSystem ParticleSystemOnDestroy;

    private float timeBetweenShots = 1.25f;
    private float timeToNextShot = 0;
    private bool targetDetected = false;


    void Start()
    {
        hp = 75;
        ep = 50;
        SetupModulesWithSpeed(movementSpeed);
        shield = Instantiate(shield, transform.position, Quaternion.identity);
        shield.transform.Rotate(Vector3.left,90f);
    }


    void Update()
    {
        shield.transform.position = transform.position;
    }


    void FixedUpdate()
    {
        MovementSystem();
        if (ep <= 0) shield.SetActive(false);
    }

    public override void OnDestroy()
    {
        isDestroyed = true;
        UISingleton.instance.ActivateHitmarker();
        Collider shipCollider = gameObject.GetComponent<Collider>();
        MeshRenderer shipRenderer = gameObject.GetComponent<MeshRenderer>();
        shipCollider.enabled = false;
        shipRenderer.enabled = false;
        if (ParticleSystemOnDestroy != null) ParticleSystemOnDestroy.Play();
        movementSpeed = movementSpeed / 2;
        //Pooler.instance.PushPool(gameObject);
    }

    public override void OnHit()
    {
        //UISingleton.instance.ActivateHitmarker();
    }

    void MovementSystem()
    {
        if (!movementDisabled)
        {
            Vector3 pos = transform.position + transform.forward * Time.deltaTime * movementSpeed;
            rb.MovePosition(pos);
        }
    }
}