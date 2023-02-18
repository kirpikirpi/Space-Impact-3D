using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLogic : MonoBehaviour,IProjectile
{
    //protected int dmgValue = 3;
    //private int collisionDamage = 50;
    //private float projectileSpeed = 30f;
    
    float cumulativeMuzzleVelocity;

    //private float range = 200f;
    protected ProjectileInfo projectileInfo;
    
    protected Vector3 startPoint;
    private Vector3 currentPoint;

    protected Rigidbody rb;
    protected bool movementPossible;

    private GameObject origin;


    public void SetProjectileParameters(ProjectileInfo projectileInfo)
    {
        this.projectileInfo = projectileInfo;
    }

    public void AdaptMuzzleVelocity(float movementSpeed)
    {
        if(projectileInfo == null) throw new Exception("Projectile not set up correctly!!");
        cumulativeMuzzleVelocity = movementSpeed + projectileInfo.speed;
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

    public virtual void SetTargetLockOn(GameObject target)
    {
        return;
    }

    void OnCollisionEnter(Collision collision)
    {
        if(projectileInfo == null) throw new Exception("Projectile not set up correctly!!");
        IDamageLogic target = collision.gameObject.GetComponent<IDamageLogic>();
        if (target != null)
        {
            target.ApplyDamage(projectileInfo.collisionDamage);
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
        if(projectileInfo == null) throw new Exception("Projectile not set up correctly!!");
        currentPoint = transform.position;
        float difference = (currentPoint - startPoint).sqrMagnitude;
        if (difference > projectileInfo.range * projectileInfo.range)
        {
            Destroy(gameObject);
        }
    }
    
    
}