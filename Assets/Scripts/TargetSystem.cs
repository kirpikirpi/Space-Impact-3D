using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSystem : MonoBehaviour
{
    private float detectionRadius;
    private float targetableAngle;
    public LayerMask detectableObjects;
    private int currentTargetIndex = 0;
    private GameObject currentTarget;

    void Start()
    {
        detectionRadius = 100f;
        targetableAngle = 45f;
        if (UISingleton.instance != null) UISingleton.instance.SetCrosshairPosition(new Vector3(0, -10, 70));
    }

    void FixedUpdate()
    {
        SetCrosshairPos();
    }

    public void SetCurrentTarget(GameObject target)
    {
        currentTarget = target;
    }

    GameObject[] GetTargetsInRadar()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius, detectableObjects);

        List<GameObject> result = new List<GameObject>();

        foreach (var targets in colliders)
        {
            if (!result.Contains(targets.gameObject))
            {
                result.Add(targets.gameObject);
            }
        }


        return result.ToArray();
    }

    GameObject[] TargetsInTargetableAngle()
    {
        GameObject[] targets = GetTargetsInRadar();
        List<GameObject> targetsInRange = new List<GameObject>();
        int targetNum = 0;


        foreach (var target in targets)
        {
            if (IsTargetable(target.gameObject))
            {
                targetsInRange.Add(target);
            }
        }

        return targetsInRange.ToArray();
    }

    bool IsTargetable(GameObject target)
    {
        Vector3 targetDir = target.transform.position - transform.position;
        float angleToTarget = Vector3.Angle(targetDir, transform.forward);

        return angleToTarget < targetableAngle;
    }

    void SetCrosshairPos()
    {
        if (currentTarget != null)
        {
            bool isDestroyed = currentTarget.GetComponent<IDamageLogic>().IsDestroyed();
            if (IsTargetable(currentTarget.gameObject) && !isDestroyed)
                UISingleton.instance.SetCrosshairPosition(currentTarget.transform.position);
            else
            {
                UISingleton.instance.SetCrosshairPosition(new Vector3(0, -10, 70));
                currentTarget = null;
            }
        }
    }

    public GameObject SelectTarget()
    {
        currentTargetIndex += 1;
        currentTarget = GetCurrentTarget();
        return currentTarget;
    }

    public GameObject GetCurrentTarget()
    {
        GameObject[] currentTargets = TargetsInTargetableAngle();
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