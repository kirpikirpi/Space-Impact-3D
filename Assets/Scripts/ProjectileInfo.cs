using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SpawnManagerScriptableObject", order = 1)]
public class ProjectileInfo : ScriptableObject
{
    public GameObject projectileType;
    public float speed;
    public float damage;
    public int energyPerShot;
    public float timeBetweenShots;
    public float range;
    public int collisionDamage;
}