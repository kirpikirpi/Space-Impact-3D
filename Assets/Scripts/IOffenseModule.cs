using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IOffenseModule
{
    void Setup(GameObject muzzle);

    public void Setup(GameObject muzzle, float movementSpeed);
    int ActivateOffense(int ep, int weaponIndex, GameObject target);
}