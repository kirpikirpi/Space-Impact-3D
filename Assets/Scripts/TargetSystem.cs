using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSystem : MonoBehaviour
{
    private float detectionRadius;
    private float targetableAngle;
    public LayerMask detectableObjects;
    private int currentTargetIndex = 0;
    private Collider currentTarget;

    void Start()
    {
        detectionRadius = 100f;
        targetableAngle = 45f;
    }

    void FixedUpdate()
    {
        SetCrosshairPos();
    }

    Collider[] GetTargetsInRadar()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius, detectableObjects);
        return colliders;
    }

    Collider[] TargetsInTargetableAngle()
    {
        Collider[] targets = GetTargetsInRadar();
        List<Collider> targetsInRange = new List<Collider>();
        int targetNum = 0;

        Vector3 spaceshipPos = transform.position;
        float spaceshipYrotation = transform.rotation.eulerAngles.y;
        foreach (var target in targets)
        {
            Vector3 targetDir = target.transform.position - transform.position;
            float angleToTarget = Vector3.Angle(targetDir, transform.forward);

            if (angleToTarget < targetableAngle)
            {
                targetsInRange.Add(target);
            }
        }

        return targetsInRange.ToArray();
    }

    void SetCrosshairPos()
    {
        //ToDo actualise pos (is targetable??)
        if (currentTarget != null) UISingleton.instance.SetCrosshairPosition(currentTarget.transform.position);
        else
        {
            //UISingleton.instance.SetCrosshairPosition(new Vector3(0, 0, 50));
        }
    }

    public Collider SelectTarget()
    {
        currentTargetIndex += 1;
        currentTarget = GetCurrentTarget();
        return currentTarget;
    }

    public Collider GetCurrentTarget()
    {
        Collider[] currentTargets = TargetsInTargetableAngle();
        if (currentTargets.Length <= 0)
        {
            currentTarget = null;
            return null;
        }

        int index = currentTargetIndex % currentTargets.Length;
        currentTarget = currentTargets[index];
        return currentTarget;
    }
}