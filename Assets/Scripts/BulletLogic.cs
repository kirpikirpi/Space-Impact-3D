using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLogic : MonoBehaviour
{
    private int dmgValue = 10;
    private float muzzleVelocity = 3f;

    private Rigidbody rb;
    private bool movementPossible;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        if (rb == null)
        {
            movementPossible = false;
            throw new Exception("No Rigidbody attached to Projectile!");
        }

        movementPossible = true;
    }

    void OnCollisionEnter(Collision collision)
    {
        IDamageLogic target = collision.gameObject.GetComponent<IDamageLogic>();
        if (target != null)
        {
            target.ApplyDamage(dmgValue);
            gameObject.SetActive(false);
        }
    }

    void FixedUpdate()
    {
        if (movementPossible) MoveProjectile();
    }


    void MoveProjectile()
    {
        Vector3 pos = transform.position + transform.forward * Time.deltaTime * muzzleVelocity;
        rb.MovePosition(pos);
    }
}