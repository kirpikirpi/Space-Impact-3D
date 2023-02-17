using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBasic : MonoBehaviour, IDamageLogic
{
    public int hp = 50;

    public void ApplyDamage(int dmg)
    {
        hp -= dmg;
        if (hp <= 0)
        {
            hp = 0;
            OnDestroy();
        }
        else OnHit();
    }

    public void OnHit()
    {
        UISingleton.instance.ActivateHitmarker();
    }

    public void OnDestroy()
    {
        gameObject.SetActive(false);
    }

    public bool IsDestroyed()
    {
        throw new System.NotImplementedException();
    }
}