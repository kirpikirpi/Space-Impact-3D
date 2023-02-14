using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IOffenseModule
{
    void Setup(GameObject origin);

    public void Setup(GameObject origin, float movementSpeed);
    int ActivateOffense(int ep);

    int ActivateAlternativeOffense(int ep);
    
    int ActivateAlternativeOffense(int ep, GameObject target);
}