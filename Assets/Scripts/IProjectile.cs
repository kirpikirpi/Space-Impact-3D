using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IProjectile
{
    public void AdaptMuzzleVelocity(float movementSpeed);

    public void SetAgressingEntety(GameObject agressor);

    public GameObject GetAgressor();

    void SetTargetLockOn(GameObject target);
}
