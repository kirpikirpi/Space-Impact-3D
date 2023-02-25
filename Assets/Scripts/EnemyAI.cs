using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public AIScriptable AiScriptableObject;

    private int alertLevel;
    private GameObject[] alliedGameObjects;
    private GameObject enemyGameObject;


    //Visual Spotting
    void OnTriggerEnter()
    {
        //Todo: check if in Enemy Layer Mask
        //use visual spotting angle and radius
        //increase alert level

        //Todo: check if in spottable Objects
        //peripheral view angle and radius
        //increase alert level
    }

    public int IncreaseAlertLevel(int alertValue)
    {
        return 0;
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