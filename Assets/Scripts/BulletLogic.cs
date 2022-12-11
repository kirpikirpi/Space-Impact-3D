using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLogic : MonoBehaviour
{
    public int dmgValue = 10;
    public float muzzleVelocity = 3f;
    
    private float range = 100f;
    private Vector3 startPoint;
    private Vector3 currentPoint;

    private Rigidbody rb;
    private bool movementPossible;


    public void AdaptMuzzleVelocity(float movementSpeed)
    {
        muzzleVelocity += movementSpeed;
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
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        if (movementPossible) MoveProjectile();
        CheckDistance();
    }


    void MoveProjectile()
    {
        Vector3 pos = transform.position + transform.forward * Time.deltaTime * muzzleVelocity;
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