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
                AiScriptableObject.visualSpottingRadius, 0))
            {
                targetGameObject = other.gameObject;
                IncreaseAlertLevel(5);
            }
        }

        if ((AiScriptableObject.spottableObjects & (1 << other.gameObject.layer)) != 0)
        {
            if (IsDetectable(other.gameObject, AiScriptableObject.peripheralViewAngle,
                AiScriptableObject.peripheralViewRadius, 0))
            {
                IncreaseAlertLevel(1);
                AlertAllies();
            }
        }
    }

    public int IncreaseAlertLevel(int alertValue)
    {
        alertLevel += alertValue;
        alertLevel = Mathf.Clamp(alertLevel, 0, maxAlertLevel);

        print("alert level: " + alertLevel + " " + transform.name);
        return alertLevel;
    }

    bool IsDetectable(GameObject target, float targetableAngle, float range, float minDistance)
    {
        if (target == null) return false;
        Vector3 targetDir = target.transform.position - transform.position;
        float angleToTarget = Vector3.Angle(targetDir, transform.forward);

        float distance = Vector3.Distance(transform.position, target.transform.position);

        return angleToTarget < targetableAngle && distance <= range && distance >= minDistance;
    }

    public void SetCurrentTarget(GameObject currentTarget)
    {
        targetGameObject = currentTarget;
    }

    void AlertAllies()
    {
        GameObject[] allies = GetAlliedPositions();
        foreach (var ally in allies)
        {
            bool detectable = IsDetectable(ally, AiScriptableObject.peripheralViewAngle,
                AiScriptableObject.alertPingRadius, 1);
            IAlertSystem alertSystem = ally.GetComponent<IAlertSystem>();
            if (alertSystem != null && detectable) alertSystem.IncreaseAlertLevel(1);
        }
    }

    //returns null if no target detected
    public Collider GetCurrentTarget()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, AiScriptableObject.visualSpottingRadius,
            AiScriptableObject.enemyLayer);
        return colliders[0];
    }

    public Vector3 NextPosition()
    {
        return Vector3.zero;
    }

    GameObject[] GetAlliedPositions()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, AiScriptableObject.visualSpottingRadius,
            AiScriptableObject.allyLayer);

        GameObject[] result = new GameObject[colliders.Length];

        for (int i = 0; i < colliders.Length; i++)
        {
            result[i] = colliders[i].transform.parent.gameObject; //ToDo: just add the game object once
        }

        return result;
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
            AiScriptableObject.visualSpottingRadius, 0);

        if (Time.fixedTime > timeToNextAlert && targetIsInSight)
        {
            timeToNextAlert = Time.fixedTime + AiScriptableObject.detectionRate;
            IncreaseAlertLevel(1);
        }
    }
}