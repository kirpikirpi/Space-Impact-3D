using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISingleton : MonoBehaviour
{
    #region Singleton

    public static UISingleton instance;

    void Awake()
    {
        if (instance != null)
        {
            return;
        }

        instance = this;
    }

    #endregion

    public Text hp;
    public Text ep;

    public void SetStats(String energy, String health)
    {
        hp.text = "HP: " + health;
        ep.text = "Energy: " + energy;
    }
}