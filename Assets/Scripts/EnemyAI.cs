using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemyAI : MonoBehaviour, IAlertSystem
{
    public AIScriptable AiScriptableObject;

    private int alertLevel;
    private int maxAlertLevel = 10;
    private GameObject[] alliedGameObjects;
    private GameObject enemyGameObject;


    //Visual Spotting
    void OnTriggerEnter(Collider other)
    {
        if ((AiScriptableObject.enemyLayer & (1 << other.gameObject.layer)) != 0)
        {
            if (IsDetectable(other.gameObject, AiScriptableObject.visualSpottingAngle,
                AiScriptableObject.visualSpottingRadius))
            {
                enemyGameObject = other.gameObject;
                IncreaseAlertLevel(2); //ToDo: timer
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
        int result = Mathf.Clamp(alertValue, 0, maxAlertLevel);
        print("alert level: " + result);
        return result;
    }

    bool IsDetectable(GameObject target, float targetableAngle, float range)
    {
        Vector3 targetDir = target.transform.position - transform.position;
        float angleToTarget = Vector3.Angle(targetDir, transform.forward);
        
        //ToDo: add range

        return angleToTarget < targetableAngle;
    }

    public void SetCurrentTarget(GameObject currentTarget)
    {
        enemyGameObject = currentTarget;
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
        sphereCollider.center = transform.position;
        sphereCollider.radius = AiScriptableObject.visualSpottingRadius;
        sphereCollider.isTrigger = true;
    }
}