using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FormationType
{
    FormationX,
    Wedge,
    Vee,
    Line,
    Column,
    FileLeft,
    FileRight,
    Diamond,
    StackLeft,
    StackRight
}

public class Formations
{
    public bool[,] GetFormationType(FormationType type)
    {
        bool[,] formation = new bool[5, 5];

        switch (type)
        {
            case FormationType.FormationX:
                formation = getFormationX();
                break;
            case FormationType.Wedge:
                formation = getFormationWedge();
                break;
            case FormationType.Vee:
                formation = getFormationVee();
                break;
            case FormationType.Line:
                formation = getFormationLine();
                break;
            case FormationType.Column:
                formation = getFormationColumn();
                break;
            case FormationType.FileLeft:
                formation = getFormationFileLeft();
                break;
            case FormationType.FileRight:
                formation = getFormationFileRight();
                break;
            case FormationType.Diamond:
                formation = getFormationDiamond();
                break;
            case FormationType.StackLeft:
                formation = getFormationStackLeft();
                break;
            case FormationType.StackRight:
                formation = getFormationStackRight();
                break;
        }

        return formation;
    }

    private bool[,] getFormationX()
    {
        bool[,] formationX = new bool[5, 5];
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

    private bool[,] getFormationWedge()
    {
        bool[,] wedge = new bool[5, 5];
        wedge[4, 2] = true;

        wedge[3, 1] = true;
        wedge[3, 3] = true;

        wedge[2, 0] = true;
        wedge[2, 4] = true;

        return wedge;
    }

    private bool[,] getFormationVee()
    {
        bool[,] vee = new bool[5, 5];
        vee[0, 2] = true;

        vee[1, 1] = true;
        vee[1, 3] = true;

        vee[2, 0] = true;
        vee[2, 4] = true;

        return vee;
    }

    private bool[,] getFormationLine()
    {
        bool[,] line = new bool[5, 5];
        for (int j = 0; j < line.GetLength(1); j++)
        {
            line[0, j] = true;
        }

        return line;
    }
    private bool[,] getFormationColumn()
    {
        bool[,] column = new bool[5, 5];
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

    private bool[,] getFormationFileLeft()
    {
        bool[,] file = new bool[5, 5];
        for (int i = 0; i < file.GetLength(1); i++)
        {
            file[i, 0] = true;
        }

        return file;
    }
    
    private bool[,] getFormationFileRight()
    {
        bool[,] file = new bool[5, 5];
        for (int i = 0; i < file.GetLength(1); i++)
        {
            file[i, file.GetLength(0)-1] = true;
        }

        return file;
    }

    private bool[,] getFormationDiamond()
    {
        bool[,] diamond = new bool[5, 5];

        diamond[0, 2] = true;

        diamond[1, 1] = true;
        diamond[1, 3] = true;

        diamond[2, 0] = true;
        diamond[2, 2] = true;
        diamond[2, 4] = true;

        diamond[3, 1] = true;
        diamond[3, 3] = true;

        diamond[4, 2] = true;

        return diamond;
    }

    private bool[,] getFormationStackRight()
    {
        bool[,] stackRight = new bool[5, 5];
        for (int i = 0; i < stackRight.GetLength(0); i++)
        {
            stackRight[i, i] = true;
        }

        return stackRight;
    }

    private bool[,] getFormationStackLeft()
    {
        bool[,] stackLeft = new bool[5, 5];
        int j = 0;
        for (int i = stackLeft.GetLength(0) - 1; i >= 0; i--)
        {
            stackLeft[i, j] = true;
            j++;
        }

        return stackLeft;
    }
}