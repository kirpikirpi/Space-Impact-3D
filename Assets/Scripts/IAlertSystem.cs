using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAlertSystem
{
    public int IncreaseAlertLevel(int alertValue);
    public void SetCurrentTarget(GameObject currentTarget);
}
