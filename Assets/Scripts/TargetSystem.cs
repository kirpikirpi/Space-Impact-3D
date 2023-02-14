using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSystem : MonoBehaviour
{
    private float detectionRadius;
    private float targetableAngle;
    public LayerMask detectableObjects;
    private int currentTargetIndex = 0;

    void Start()
    {
        detectionRadius = 100f;
        targetableAngle = 45f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            //GetCurrentTarget();
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

    public Collider GetCurrentTarget()
    {
        Collider[] currentTargets = TargetsInTargetableAngle();
        if (currentTargets.Length <= 0) return null;
        int index = currentTargetIndex % currentTargets.Length;
        currentTargetIndex += 1;
        UISingleton.instance.SetCrosshairPosition(currentTargets[index].transform.position);
        return currentTargets[index];
    }
}