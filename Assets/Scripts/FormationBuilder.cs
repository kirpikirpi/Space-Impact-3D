using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormationBuilder : MonoBehaviour
{
    Formations _formations = new Formations();
    
    void Start()
    {
        PrintFormation(_formations.getFormationColumn());
        PrintFormation(_formations.getFormationFile());
        PrintFormation(_formations.getFormationLine());
        PrintFormation(_formations.getFormationWedge());
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
