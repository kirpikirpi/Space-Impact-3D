using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : MonoBehaviour, IDamageLogic
{
    public int hp;
    public int ep;
    
    protected bool isDestroyed = false;
    protected bool movementDisabled = false;

    public GameObject muzzle;

    protected Rigidbody rb;

    private int collisionDamage = 15;
    public LayerMask collisionLayers;

    public GameObject OffenseModulePrefab;
    public GameObject DefenseModulePrefab;

    public IOffenseModule OffenseModule;
    public IDefenseModule DefenseModule;


    public virtual void ApplyDamage(int dmg)
    {
        hp -= dmg;
        if (hp <= 0)
        {
            hp = 0;
            OnDestroy();
        }
        else OnHit();
    }

    public virtual void OnHit()
    {
        throw new System.NotImplementedException();
    }

    public virtual void OnDestroy()
    {
        throw new System.NotImplementedException();
    }

    public void SetupModules()
    {
        if (OffenseModulePrefab == null)
        {
            throw new Exception("Offensive module not attached! " + gameObject.tag);
        }

        if (DefenseModulePrefab == null)
        {
            throw new Exception("Defensive module not attached! " + gameObject.tag);
        }

        if (muzzle == null)
        {
            throw new Exception("No muzzle assigned! " + gameObject.tag);
        }

        rb = gameObject.GetComponent<Rigidbody>();
        
        if (rb == null)
        {
            throw new Exception("no rigidbody attached! " + gameObject.tag);
        }

        OffenseModulePrefab = Instantiate(OffenseModulePrefab, transform.position, Quaternion.identity,
            gameObject.transform);
        DefenseModulePrefab = Instantiate(DefenseModulePrefab, transform.position, Quaternion.identity,
            gameObject.transform);

        OffenseModule = OffenseModulePrefab.GetComponent<IOffenseModule>();
        DefenseModule = DefenseModulePrefab.GetComponent<IDefenseModule>();
        
        OffenseModule.Setup(muzzle);
    }
    
    public void SetupModulesWithSpeed(float movementSpeed)
    {
        if (OffenseModulePrefab == null)
        {
            throw new Exception("Offensive module not attached! " + gameObject.tag);
        }

        if (DefenseModulePrefab == null)
        {
            throw new Exception("Defensive module not attached! " + gameObject.tag);
        }

        if (muzzle == null)
        {
            throw new Exception("No muzzle assigned! " + gameObject.tag);
        }

        rb = gameObject.GetComponent<Rigidbody>();
        
        if (rb == null)
        {
            throw new Exception("no rigidbody attached! " + gameObject.tag);
        }

        OffenseModulePrefab = Instantiate(OffenseModulePrefab, transform.position, Quaternion.identity,
            gameObject.transform);
        DefenseModulePrefab = Instantiate(DefenseModulePrefab, transform.position, Quaternion.identity,
            gameObject.transform);

        OffenseModule = OffenseModulePrefab.GetComponent<IOffenseModule>();
        DefenseModule = DefenseModulePrefab.GetComponent<IDefenseModule>();
        
        OffenseModule.Setup(muzzle, movementSpeed);
    }


    //layer check: layermask == (layermask | (1 << layer))
    void OnCollisionEnter(Collision collision)
    {
        IDamageLogic target = collision.gameObject.GetComponent<IDamageLogic>();
        bool isHittable = collisionLayers == (collisionLayers | (1 << collision.gameObject.layer));
        if (target != null && isHittable)
        {
            target.ApplyDamage(collisionDamage);
        }
    }
}