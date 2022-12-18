using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormationBuilder : MonoBehaviour
{
    Formations _formations = new Formations();
    private float xAxisLength = 10;
    float zAxisLength = 10;

    void Start()
    {
    }

    public GameObject SpawnFormation(GameObject leadShip, GameObject escortShips, Vector3 position, FormationType type)
    {
        GameObject lead = new GameObject("Formation");
        bool[,] formation = _formations.GetFormationType(type);
        float xOffsetPerUnit = xAxisLength / formation.GetLength(0);
        float zOffsetPerUnit = zAxisLength / formation.GetLength(1);

        float xIterationStartingPoint =-xAxisLength / 2;
        float zIterationStartingPoint = -zAxisLength/ 2;
        
        Vector3 iterationStartingPoint =
            new Vector3(xIterationStartingPoint, 0, zIterationStartingPoint);

        lead.transform.position = position;

        for (int x = 0; x < formation.GetLength(0); x++)
        {
            for (int z = 0; z < formation.GetLength(1); z++)
            {
                if (formation[x, z])
                {
                    Vector3 spawnPos = new Vector3(x * xOffsetPerUnit +1, position.y, z * zOffsetPerUnit+1);
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