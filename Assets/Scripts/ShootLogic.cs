using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootLogic : MonoBehaviour, IOffenseModule
{
    public int epPerShot = 5;
    public float timeBetweenShots = 0.2f;
    private float timeToNextShot = 0;
    private bool shootingPossible;

    public GameObject projectile;


    void Update()
    {
        if (Time.time > timeToNextShot)
        {
            shootingPossible = true;
        }
        else
        {
            shootingPossible = false;
        }
    }


    public int ActivateOffense(int ep, GameObject muzzle)
    {
        if (ep >= epPerShot && projectile != null && muzzle != null && shootingPossible)
        {
            GameObject newProjectile = Instantiate(projectile, muzzle.transform.position, Quaternion.identity);
            newProjectile.transform.rotation = muzzle.transform.rotation;
            timeToNextShot = Time.time + timeBetweenShots;
            return ep - epPerShot;
        }
        return ep;
    }
}