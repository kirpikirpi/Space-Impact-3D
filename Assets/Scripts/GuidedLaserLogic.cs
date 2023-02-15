using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidedLaserLogic : BulletLogic
{
    private GameObject currentTarget;
    
    public void SetTargetLockOn(GameObject target)
    {
        currentTarget = target;
    }
    
    protected override void MoveProjectile()
    {
        Vector3 direction = currentTarget.transform.position - transform.position;
        Vector3 pos = transform.position + direction * Time.deltaTime * cumulativeMuzzleVelocity; 
        rb.MovePosition(pos);
    }
}
