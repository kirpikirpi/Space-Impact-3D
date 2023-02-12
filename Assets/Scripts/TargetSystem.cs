using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSystem : MonoBehaviour
{
    private float detectionRadius;
    private float targetableAngle;
    public LayerMask detectableObjects;

    void Start()
    {
        detectionRadius = 100f;
        targetableAngle = 45f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Collider[] colliders = GetTargetsInRadar();
            TargetsInTargetableAngle();
            foreach (var collider in colliders)
            {
                print(collider.tag);
            }
        }
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

    Collider GetCurrentTarget()
    {
        return null;
    }
}