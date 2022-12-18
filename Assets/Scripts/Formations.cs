using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Formations
{
    bool[,] formationX = new bool[5, 5];
    bool[,] wedge = new bool[5, 5];
    bool[,] line = new bool[5, 5];
    bool[,] column = new bool[5, 5];
    bool[,] vee = new bool[5, 5];
    bool[,] file = new bool[5, 5];
    bool[,] diamond = new bool[5, 5];

    public bool[,] getFormationX()
    {
        formationX[0, 0] = true;
        formationX[0, 4] = true;

        formationX[1, 1] = true;
        formationX[1, 3] = true;

        formationX[2, 2] = true;

        formationX[3, 1] = true;
        formationX[3, 3] = true;

        formationX[4, 0] = true;
        formationX[4, 4] = true;

        return formationX;
    }

    public bool[,] getFormationWedge()
    {
        wedge[0, 2] = true;

        wedge[1, 1] = true;
        wedge[1, 3] = true;

        wedge[2, 0] = true;
        wedge[2, 4] = true;

        return wedge;
    }

    public bool[,] getFormationLine()
    {
        for (int j = 0; j < line.GetLength(1); j++)
        {
            line[0, j] = true;
        }

        return line;
    }

    public bool[,] getFormationColumn()
    {
        for (int i = 0; i < column.GetLength(0); i++)
        {
            for (int j = 1; j < column.GetLength(1); j += 2)
            {
                if (i % 2 == 0)
                {
                    column[i, j] = true;
                    break;
                }

                if (j > 1) column[i, j] = true;
            }
        }

        return column;
    }

    public bool[,] getFormationFile()
    {
        for (int i = 0; i < file.GetLength(1); i++)
        {
            file[i, 0] = true;
        }

        return file;
    }
}