using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOffense : MonoBehaviour, IOffenseModule
{
    public int epPerShot = 2;
    public int epPerSecondaryFireShot = 10;
    
    public float timeBetweenShots = 0.2f;
    private float timeToNextShot = 0;
    private bool shootingPossible;
    private float movementSpeed;

    public GameObject standardProjectile;
    public GameObject secondaryFireProjectile;

    private GameObject muzzle;
    private bool setupComplete = false;

    private TargetSystem targetSystem;
    
    
    public void Setup(GameObject origin)
    {
        muzzle = origin;
        if (targetSystem == null) targetSystem = gameObject.AddComponent<TargetSystem>();
        setupComplete = true;
    }

    public void Setup(GameObject origin, float movementSpeed)
    {
        muzzle = origin;
        if (targetSystem == null) targetSystem = gameObject.AddComponent<TargetSystem>();
        //BulletLogic bulletLogic = standardProjectile.GetComponent<BulletLogic>();
        //bulletLogic.AdaptMuzzleVelocity(movementSpeed);
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

        return ep;
    }
    public int ActivateAlternativeOffense(int ep, GameObject target)
    {
        if (ep >= epPerSecondaryFireShot && secondaryFireProjectile != null && muzzle != null && shootingPossible)
        {
            GameObject newProjectile = Instantiate(secondaryFireProjectile, muzzle.transform.position, Quaternion.identity);
            newProjectile.transform.rotation = muzzle.transform.rotation;
            timeToNextShot = Time.time + timeBetweenShots;

            return ep - epPerSecondaryFireShot;
        }

        return ep;
    }
}
