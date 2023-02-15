using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootLogic : MonoBehaviour, IOffenseModule
{
    public int epPerShot = 1;
    public int epPerSecondaryFireShot = 10;
    
    public float timeBetweenShots = 0.15f;
    private float timeToNextShot = 0;
    private bool shootingPossible;
    private float movementSpeed;

    public GameObject standardProjectile;
    public GameObject secondaryFireProjectile;

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

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 100))
        {
            //UISingleton.instance.SetCrosshairPosition(hit.point);
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
        if (ep >= epPerSecondaryFireShot && secondaryFireProjectile != null && muzzle != null && shootingPossible)
        {
            GameObject newProjectile = Instantiate(secondaryFireProjectile, muzzle.transform.position, Quaternion.identity);
            newProjectile.transform.rotation = muzzle.transform.rotation;
            timeToNextShot = Time.time + timeBetweenShots;

            return ep - epPerSecondaryFireShot;
        }
        if (ep < epPerSecondaryFireShot)
        {
            ep = ActivateOffense(ep);
        }

        return ep;
    }

    public int ActivateAlternativeOffense(int ep, GameObject target)
    {
        throw new NotImplementedException();
    }
}