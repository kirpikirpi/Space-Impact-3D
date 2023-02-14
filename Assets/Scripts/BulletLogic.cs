using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLogic : MonoBehaviour
{
    private int dmgValue = 3;
    private int collisionDamage = 50;
    private float projectileSpeed = 30f;
    public float cumulativeMuzzleVelocity; //public needed to change velocity in prefab

    private float range = 100f;
    private Vector3 startPoint;
    private Vector3 currentPoint;

    protected Rigidbody rb;
    private bool movementPossible;


    public void AdaptMuzzleVelocity(float movementSpeed)
    {
        cumulativeMuzzleVelocity = movementSpeed + projectileSpeed;
    }
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        if (rb == null)
        {
            movementPossible = false;
            throw new Exception("No Rigidbody attached to Projectile!");
        }

        movementPossible = true;

        startPoint = transform.position;
    }

    void OnCollisionEnter(Collision collision)
    {
        IDamageLogic target = collision.gameObject.GetComponent<IDamageLogic>();
        if (target != null)
        {
            target.ApplyDamage(dmgValue);
            cumulativeMuzzleVelocity = cumulativeMuzzleVelocity / 4;
            IDamageLogic bulletDamageLogic = gameObject.GetComponent<IDamageLogic>();
            bulletDamageLogic.ApplyDamage(50);
        }
    }

    void FixedUpdate()
    {
        if (movementPossible) MoveProjectile();
        CheckDistance();
    }


    protected virtual void MoveProjectile()
    {
        Vector3 pos = transform.position + transform.forward * Time.deltaTime * cumulativeMuzzleVelocity;
        rb.MovePosition(pos);
    }

    void CheckDistance()
    {
        currentPoint = transform.position;
        float difference = (currentPoint - startPoint).sqrMagnitude;
        if (difference > range * range)
        {
            Destroy(gameObject);
        }
    }
    
    
}