using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootLogic : MonoBehaviour, IOffenseModule
{
    public ProjectileInfo[] weaponSystems;
    
    private float timeToNextShot = 0;
    private bool shootingPossible;
    private float movementSpeed;
    

    private GameObject muzzle;
    private bool setupComplete = false;


    void Update()
    {
        if (!setupComplete) return;
        if (Time.time > timeToNextShot)
        {
            shootingPossible = true;
        }
        else
        {
            shootingPossible = false;
        }
        
    }

    public void Setup(GameObject muzzle)
    {
        this.muzzle = muzzle;
        setupComplete = true;
    }

    public void Setup(GameObject muzzle, float movementSpeed)
    {
        this.muzzle = muzzle;
        this.movementSpeed = movementSpeed;
        setupComplete = true;
    }

    public int ActivateOffense(int ep, int weaponIndex, GameObject target)
    { 
        if(weaponSystems.Length<=0) throw new Exception("no weapon system!");
        ProjectileInfo currentWeapon = weaponSystems[weaponIndex];

        if (ep >= currentWeapon.energyPerShot && currentWeapon.projectileType != null && muzzle != null &&
            shootingPossible)
        {
            GameObject newProjectile = Instantiate(currentWeapon.projectileType, muzzle.transform.position,
                Quaternion.identity);
            newProjectile.transform.rotation = muzzle.transform.rotation;
            timeToNextShot = Time.time + currentWeapon.timeBetweenShots;

            IProjectile projectile = newProjectile.GetComponent<IProjectile>();
            projectile.SetTargetLockOn(target);
            projectile.SetAgressingEntety(transform.parent.gameObject);
            return ep - currentWeapon.energyPerShot;
        }

        return ep;
    }
}