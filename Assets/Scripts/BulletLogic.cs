using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLogic : MonoBehaviour
{
    protected int dmgValue = 3;
    private int collisionDamage = 50;
    private float projectileSpeed = 30f;
    public float cumulativeMuzzleVelocity; //public needed to change velocity in prefab

    private float range = 200f;
    protected Vector3 startPoint;
    private Vector3 currentPoint;

    protected Rigidbody rb;
    protected bool movementPossible;

    private GameObject origin;


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
    }

    public void SetAgressingEntety(GameObject agressor)
    {
        this.origin = agressor;
    }

    public GameObject GetAgressor()
    {
        return origin;
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

    protected void CheckDistance()
    {
        currentPoint = transform.position;
        float difference = (currentPoint - startPoint).sqrMagnitude;
        if (difference > range * range)
        {
            Destroy(gameObject);
        }
    }
    
    
}