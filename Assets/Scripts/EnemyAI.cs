using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemyAI : MonoBehaviour, IAlertSystem
{
    public AIScriptable AiScriptableObject;

    private int alertLevel;
    private int maxAlertLevel = 100;
    private GameObject[] alliedGameObjects;
    private GameObject targetGameObject;

    private float timeToNextAlert;


    //Visual Spotting
    void OnTriggerEnter(Collider other)
    {
        if ((AiScriptableObject.enemyLayer & (1 << other.gameObject.layer)) != 0)
        {
            if (IsDetectable(other.gameObject, AiScriptableObject.visualSpottingAngle,
                AiScriptableObject.visualSpottingRadius))
            {
                targetGameObject = other.gameObject;
                IncreaseAlertLevel(5);
            }
        }

        if ((AiScriptableObject.spottableObjects & (1 << other.gameObject.layer)) != 0)
        {
            if (IsDetectable(other.gameObject, AiScriptableObject.peripheralViewAngle,
                AiScriptableObject.peripheralViewRadius))
            {
                IncreaseAlertLevel(1);
            }
        }
    }

    public int IncreaseAlertLevel(int alertValue)
    {
        alertLevel += alertValue;
        alertLevel = Mathf.Clamp(alertLevel, 0, maxAlertLevel);

        print("alert level: " + alertLevel);
        return alertLevel;
    }

    bool IsDetectable(GameObject target, float targetableAngle, float range)
    {
        if (target == null) return false;
        Vector3 targetDir = target.transform.position - transform.position;
        float angleToTarget = Vector3.Angle(targetDir, transform.forward);

        float distance = Vector3.Distance(transform.position, target.transform.position);

        return angleToTarget < targetableAngle && distance <= range;
    }

    public void SetCurrentTarget(GameObject currentTarget)
    {
        targetGameObject = currentTarget;
    }

    void AlertAllies()
    {
        //Todo: Get allied positions
        //create interface for increase alert level
        //use alert ping radius
    }

    //returns null if no target detected
    public GameObject GetCurrentTarget()
    {
        return null;
    }

    public Vector3 NextPosition()
    {
        return Vector3.zero;
    }

    GameObject[] GetAlliedPositions()
    {
        return null;
    }

    void Start()
    {
        alertLevel = 0;
        SphereCollider sphereCollider = gameObject.AddComponent<SphereCollider>();
        sphereCollider.radius = AiScriptableObject.visualSpottingRadius;
        sphereCollider.isTrigger = true;

        sphereCollider = gameObject.AddComponent<SphereCollider>();
        sphereCollider.radius = AiScriptableObject.peripheralViewRadius;
        sphereCollider.isTrigger = true;
    }

    void FixedUpdate()
    {
        bool targetIsInSight = IsDetectable(targetGameObject, AiScriptableObject.visualSpottingAngle,
            AiScriptableObject.visualSpottingRadius);

        if (Time.fixedTime > timeToNextAlert && targetIsInSight)
        {
            timeToNextAlert = Time.fixedTime + AiScriptableObject.detectionRate;
            IncreaseAlertLevel(1);
        }
    }
}