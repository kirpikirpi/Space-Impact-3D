using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDamageSystem : MonoBehaviour, IDamageLogic
{
   private int hp = 1;


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
      gameObject.SetActive(false);
   }
}
