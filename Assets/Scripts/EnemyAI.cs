using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemyAI : MonoBehaviour, IAlertSystem
{
    public AIScriptable AiScriptableObject;

    private int alertLevel;
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
                IncreaseAlertLevel(AiScriptableObject.alertValueEnemySpotted);
            }
        }

        if ((AiScriptableObject.spottableObjects & (1 << other.gameObject.layer)) != 0)
        {
            if (IsDetectable(other.gameObject, AiScriptableObject.peripheralViewAngle,
                AiScriptableObject.peripheralViewRadius, 0))
            {
                IncreaseAlertLevel(AiScriptableObject.alertValueSuspiciousObjectSpotted);
            }
        }
    }

    //Visual spotting, player in view conus buildup
    void FixedUpdate()
    {
        bool targetIsInSight = IsDetectable(targetGameObject, AiScriptableObject.visualSpottingAngle,
            AiScriptableObject.visualSpottingRadius, 0);

        if (Time.fixedTime > timeToNextAlert && targetIsInSight)
        {
            timeToNextAlert = Time.fixedTime + AiScriptableObject.detectionRate;
            IncreaseAlertLevel(AiScriptableObject.alertValueEnemySpotted);
        }
    }

    public int IncreaseAlertLevel(int alertValue)
    {
        alertLevel += alertValue;
        alertLevel = Mathf.Clamp(alertLevel, 0, AiScriptableObject.combatModeLevel);

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

    void AlertAllies(int alertValue)
    {
        GameObject[] allies = GetAlliedPositions();
        if (allies.Length <= 0) return;

        foreach (var ally in allies)
        {
            bool detectable = IsDetectable(ally, AiScriptableObject.peripheralViewAngle,
                AiScriptableObject.alertPingRadius, 1);
            IAlertSystem alertSystem = ally.GetComponent<IAlertSystem>();
            if (alertSystem != null && detectable) alertSystem.IncreaseAlertLevel(alertValue);
        }
    }

    GameObject[] GetAlliedPositions()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, AiScriptableObject.visualSpottingRadius,
            AiScriptableObject.allyLayer);
        if (colliders.Length <= 0) return null;

        List<GameObject> result = new List<GameObject>();

        foreach (var ally in colliders)
        {
            if (!result.Contains(ally.gameObject))
            {
                result.Add(ally.gameObject);
            }
        }

        return result.ToArray();
    }

    public Enums.AlertState AssesAlertState()
    {
        if (alertLevel > AiScriptableObject.suspiciousLevel && alertLevel < AiScriptableObject.combatModeLevel)
            return Enums.AlertState.Suspicious;
        if (alertLevel >= AiScriptableObject.combatModeLevel) return Enums.AlertState.CombatMode;
        return Enums.AlertState.Scouting;
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

    public void SetCurrentTarget(GameObject currentTarget)
    {
        targetGameObject = currentTarget;
    }
}