using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDefenseModule
{
    BlockInfo ActivateDefense(int energy);
    void DeactivateDefense();
}
