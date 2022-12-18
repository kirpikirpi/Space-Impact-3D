using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormationBuilder : MonoBehaviour
{
    Formations _formations = new Formations();
    private float xAxisLength = 15;
    float zAxisLength = 15;

    void Start()
    {
    }

    public GameObject SpawnFormation(GameObject leadShip, GameObject escortShips, Vector3 position, FormationType type)
    {
        GameObject lead = new GameObject("Formation");
        bool[,] formation = _formations.GetFormationType(type);
        float xOffsetPerUnit = xAxisLength / formation.GetLength(0);
        float zOffsetPerUnit = zAxisLength / formation.GetLength(1);

        float xIterationStartingPoint = -xAxisLength / 2;
        float zIterationStartingPoint = -zAxisLength / 2;

        Vector3 iterationStartingPoint =
            new Vector3(xIterationStartingPoint, 0, zIterationStartingPoint);

        lead.transform.position = position;

        for (int z = 0; z < formation.GetLength(0); z++)
        {
            for (int x = 0; x < formation.GetLength(1); x++)
            {
                if (formation[z, x])
                {
                    Vector3 spawnPos = new Vector3(x * xOffsetPerUnit + xOffsetPerUnit / 2, position.y,
                        z * zOffsetPerUnit + zOffsetPerUnit / 2);
                    spawnPos += iterationStartingPoint;
                    GameObject currentEscortShip =
                        Instantiate(escortShips, spawnPos, Quaternion.identity, lead.transform);
                }
            }
        }

        return lead;
    }

    void PrintFormation(bool[,] formation)
    {
        String matrix = "";

        for (int i = 0; i < formation.GetLength(0); i++)
        {
            for (int j = 0; j < formation.GetLength(1); j++)
            {
                if (formation[i, j])
                {
                    matrix += "X ";
                }
                else
                {
                    matrix += "0 ";
                }
            }

            matrix += "\n";
        }

        print(matrix);
    }
}