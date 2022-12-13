using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldLogic : MonoBehaviour, IDefenseModule
{
    public LayerMask blockableLayer;
    public GameObject shieldPrefab;
    private GameObject shieldGameObject;
    
    public Color colorIdle;
    public Color perfectBlockColor;
    public Color earlyBlockColor;
    Renderer shieldRenderer;
    private bool colorChangable = true;

    private float lateBlockRadius = 2.5f;
    private float perfectBlockRadius = 3f;

    private float earlyBlockTime = 0.25f;
    private float lastShieldActivateTime = 0;
    private bool shieldActive;

    private int shieldEnergyCost = 5;
    private int shieldEnergyYield = 10;

    void Start()
    {
        if (shieldPrefab == null)
        {
            throw new Exception("No shield prefab attached to shield module!");
        }

        if (shieldGameObject == null)
        {
            shieldGameObject = Instantiate(shieldPrefab, transform.position, Quaternion.identity);
            shieldGameObject.transform.localScale =
                new Vector3(2 * perfectBlockRadius, 2 * perfectBlockRadius, 2 * perfectBlockRadius);
            shieldGameObject.SetActive(false);
            shieldGameObject.transform.parent = gameObject.transform;
            shieldRenderer = shieldGameObject.GetComponent<Renderer>();
        }
    }

    public int ActivateDefense(int energy)
    {
        if (energy < shieldEnergyCost) return energy;
        if (!shieldActive)
        {
            shieldActive = true;
            lastShieldActivateTime = Time.time;
        }


        if (shieldGameObject != null) shieldGameObject.SetActive(true);


        int maxColliders = 10;

        Collider[] hitColliders = new Collider[maxColliders];
        int NumColliders =
            Physics.OverlapSphereNonAlloc(transform.position, perfectBlockRadius, hitColliders, blockableLayer);

        for (int i = 0; i < NumColliders; i++)
        {
            Vector3 bulletPos = hitColliders[i].transform.position;
            Vector3 shieldMiddlePoint = transform.position;

            float distance = Vector3.Distance(shieldMiddlePoint, bulletPos);

            if (distance > lateBlockRadius)
            {
                float time = lastShieldActivateTime + earlyBlockTime;
                if (lastShieldActivateTime + earlyBlockTime > Time.time)
                {
                    //perfect block
                    if(colorChangable) StartCoroutine(ChangeShieldColor(perfectBlockColor));
                    energy += shieldEnergyYield;
                }
                else
                {
                    //early block
                    if(colorChangable) StartCoroutine(ChangeShieldColor(earlyBlockColor));
                    energy -= shieldEnergyCost;
                }
            }
            else if (distance < lateBlockRadius)
            {
                //late block
                continue;
            }

            hitColliders[i].gameObject.SetActive(false);
        }

        return energy;
    }

    public void DeactivateDefense()
    {
        if (shieldGameObject != null)
        {
            shieldGameObject.SetActive(false);
        }

        lastShieldActivateTime = 0;
        shieldActive = false;
    }

    IEnumerator ChangeShieldColor(Color shieldColor)
    {
        if (shieldRenderer != null) shieldRenderer.material.SetColor("_Color", shieldColor);
        colorChangable = false;
        yield return new WaitForSeconds(0.3f);
        if (shieldRenderer != null) shieldRenderer.material.SetColor("_Color", colorIdle);
        colorChangable = true;
    }
}