using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDefenseModule
{
    int ActivateDefense(int energy);
    void SetDefenseEffects(int energy, bool active);
}
