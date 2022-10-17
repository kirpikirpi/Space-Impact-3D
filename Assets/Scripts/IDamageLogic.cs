using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageLogic
{
    public void ApplyDamage(int dmg);

    void OnHit();

    void OnDestroy();
}