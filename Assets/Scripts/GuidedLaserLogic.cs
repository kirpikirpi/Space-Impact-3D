using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidedLaserLogic : BulletLogic
{
    private GameObject currentTarget;

    public override  void SetTargetLockOn(GameObject target)
    {
        currentTarget = target;
    }
    
    protected override void MoveProjectile()
    {
        //Todo exterminate look at bug
        
        transform.LookAt(currentTarget.transform);
        Vector3 pos = transform.position + transform.forward * Time.deltaTime * projectileInfo.speed;
        rb.MovePosition(pos);
        
        /*
        Vector3 direction = currentTarget.transform.position - transform.position;
        Vector3 pos = transform.position + direction * Time.deltaTime * 1; 
        rb.MovePosition(pos);*/
    }
    
    
    void FixedUpdate()
    {
        if (movementPossible) MoveProjectile();
        CheckDistance();
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
}
