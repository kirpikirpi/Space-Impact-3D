using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/AI Scriptable Object", order = 2)]
public class AIScriptable : ScriptableObject
{
    public LayerMask enemyLayer;
    public LayerMask allyLayer;
    public LayerMask spottableObjects;
    
    public float visualSpottingRadius;
    public float visualSpottingAngle;
    public float peripheralViewAngle;
    public float peripheralViewRadius;
    public float detectionRate;
    public float alertPingRadius;
    public float alertPingTimeBetweenPings;

    public int suspiciousLevel;
    public int combatModeLevel;
    public int alertValueEnemySpotted;
    public int alertValueSuspiciousObjectSpotted;
    public int alertValueOnShipDestroyed;
    public float cooldownTime;
}