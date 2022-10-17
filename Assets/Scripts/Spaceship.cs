using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : MonoBehaviour, IDamageLogic
{
    public int hp;
    public int ep;

    private int collisionDamage = 100;

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
    
    void OnCollisionEnter(Collision collision)
    {
        IDamageLogic target = collision.gameObject.GetComponent<IDamageLogic>();
        if (target != null)
        {
            target.ApplyDamage(collisionDamage);
        }
        ApplyDamage(collisionDamage);
    }
    
}
