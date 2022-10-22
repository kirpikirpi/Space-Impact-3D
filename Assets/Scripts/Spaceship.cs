using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : MonoBehaviour, IDamageLogic
{
    public int hp;
    public int ep;

    private int collisionDamage = 15;
    public LayerMask collisionLayers;

    public GameObject OffenseModulePrefab;
    public GameObject DefenseModulePrefab;

    public IOffenseModule OffenseModule;
    public IDefenseModule DefenseModule;
    


    public virtual void ApplyDamage(int dmg)
    {
        hp -= dmg;
        if (hp <= 0) OnDestroy();
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
