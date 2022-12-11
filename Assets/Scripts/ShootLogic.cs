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
    private float movementSpeed;

    public GameObject standardProjectile;
    public GameObject alternativeProjectile;

    private GameObject muzzle;
    private bool setupComplete = false;


    void Update()
    {
        if(!setupComplete) return;
        if (Time.time > timeToNextShot)
        {
            shootingPossible = true;
        }
        else
        {
            shootingPossible = false;
        }
    }

    public void Setup(GameObject origin)
    {
        muzzle = origin;
        setupComplete = true;
    }
    public void Setup(GameObject origin, float movementSpeed)
    {
        muzzle = origin;
        BulletLogic bulletLogic = standardProjectile.GetComponent<BulletLogic>();
        bulletLogic.AdaptMuzzleVelocity(movementSpeed);
        this.movementSpeed = movementSpeed;
        setupComplete = true;
    }

    public int ActivateOffense(int ep)
    {
        if (ep >= epPerShot && standardProjectile != null && muzzle != null && shootingPossible)
        {
            GameObject newProjectile = Instantiate(standardProjectile, muzzle.transform.position, Quaternion.identity);
            newProjectile.transform.rotation = muzzle.transform.rotation;
            timeToNextShot = Time.time + timeBetweenShots;
            return ep - epPerShot;
        }

        return ep;
    }

    public int ActivateAlternativeOffense(int ep)
    {
        if (ep >= epPerShot && standardProjectile != null && muzzle != null && shootingPossible)
        {
            GameObject newProjectile = Instantiate(alternativeProjectile, muzzle.transform.position, Quaternion.identity);
            newProjectile.transform.rotation = muzzle.transform.rotation;
            timeToNextShot = Time.time + timeBetweenShots;
            return ep - epPerShot;
        }

        return ep;
    }
}