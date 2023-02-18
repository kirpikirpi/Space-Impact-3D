using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOffense : MonoBehaviour, IOffenseModule
{
    public ProjectileInfo[] weaponSystems;
    
    private float timeToNextShot = 0;
    private bool shootingPossible;

    private GameObject muzzle;
    private bool setupComplete = false;

    private TargetSystem targetSystem;


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
        if (targetSystem == null) targetSystem = gameObject.AddComponent<TargetSystem>();
        setupComplete = true;
    }

    public void Setup(GameObject muzzle, float movementSpeed)
    {
        throw new System.NotImplementedException();
    }

    public int ActivateOffense(int ep, int weaponIndex, GameObject target)
    {
        ProjectileInfo currentWeapon = weaponSystems[weaponIndex % weaponSystems.Length];
        
        if (ep >= currentWeapon.energyPerShot && currentWeapon.projectileType != null && muzzle != null && shootingPossible)
        {
            GameObject newProjectile = Instantiate(currentWeapon.projectileType, muzzle.transform.position, Quaternion.identity);
            newProjectile.transform.rotation = muzzle.transform.rotation;
            timeToNextShot = Time.time + currentWeapon.timeBetweenShots;
            
            IProjectile projectile = newProjectile.GetComponent<IProjectile>();
            projectile.SetTargetLockOn(target);
            projectile.SetProjectileParameters(currentWeapon);

            return ep - currentWeapon.energyPerShot;
        }

        return ep;
    }
}