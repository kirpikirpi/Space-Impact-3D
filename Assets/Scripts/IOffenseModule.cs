using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IOffenseModule
{
    void Setup(GameObject origin);
    int ActivateOffense(int ep);

    int ActivateAlternativeOffense(int ep);
}