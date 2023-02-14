using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDamageSystem : MonoBehaviour, IDamageLogic
{
    public int hp = 1;

    public ParticleSystem onDestroyEffect;


    public void ApplyDamage(int dmg)
    {
        hp -= dmg;
        if (hp <= 0) OnDestroy();
        else OnHit();
    }

    public void OnHit()
    {
    }

    public void OnDestroy()
    {
        MeshRenderer renderer = gameObject.GetComponent<MeshRenderer>();
        Collider collider = gameObject.GetComponent<Collider>();
        if (renderer != null) renderer.enabled = false;
        collider.enabled = false;
        if (onDestroyEffect != null) onDestroyEffect.Play();
    }
}