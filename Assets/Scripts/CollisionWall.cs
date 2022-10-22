using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionWall : MonoBehaviour
{
    public LayerMask collisionLayers;
    private int collisionDamage = 50;
    
    void OnCollisionEnter(Collision collision)
    {
        IDamageLogic target = collision.gameObject.GetComponent<IDamageLogic>();
        bool isHittable = collisionLayers == (collisionLayers | (1 << collision.gameObject.layer));
        if (target != null && isHittable)
        {
            target.ApplyDamage(collisionDamage);
        }
    }
}
