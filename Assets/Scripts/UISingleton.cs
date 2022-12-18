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
    
    private bool hitmarkerNotActive = true;
    public RawImage hitmarker;
    public RawImage crosshair;

    public void SetStats(String energy, String health)
    {
        hp.text = "HP: " + health;
        ep.text = "Energy: " + energy;
    }

    void Start()
    {
        if(hitmarker == null) return;
        hitmarker.gameObject.SetActive(false);
        hitmarker.transform.SetParent(crosshair.transform);
    }

    public void SetCrosshairPosition(Vector2 currentPos)
    {
        return;
        Vector3 pos = Camera.main.WorldToScreenPoint(new Vector3(currentPos.x,0,0));
        print("current point: "+currentPos + " calculated point: "+pos);
        crosshair.rectTransform.anchoredPosition = pos;
    }

    public void ActivateHitmarker()
    {
        if(hitmarker == null) return;
        if (hitmarkerNotActive) StartCoroutine(Hitmarker());
    }

    IEnumerator Hitmarker()
    {
        hitmarkerNotActive = false;
        hitmarker.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        hitmarkerNotActive = true;
        hitmarker.gameObject.SetActive(false);
    }
}