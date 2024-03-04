using System;
using System.Numerics;
using UnityEngine;

public class ClosestSpot
{
    int abilityRange;
    public int pXPos;
    public int pYPos;

    int targetX;
    int targetY;

    public ClosestSpot() 
    { 
    }

    public (int, int) FindClosestSpot(Vector3Int playerPos, Vector3Int mousePos, int range)
    {
        pXPos = playerPos.x;
        pYPos = playerPos.y;
        targetX = mousePos.x;
        targetY = mousePos.y;
        abilityRange = range;

        if (IsInRange(targetX, targetY))
        {
            return (targetX, targetY);
        }

        int closestX = targetX;
        int closestY = targetY;
        double minDistance = double.MaxValue;

        for (int i = Math.Max(pXPos - abilityRange, 0); i <= Math.Min(pXPos + abilityRange, targetX); i++)
        {
            for (int j = Math.Max(pYPos - abilityRange, 0); j <= Math.Min(pYPos + abilityRange, targetY); j++)
            {
                if (IsInRange(i, j))
                {
                    double distance = CalculateEuclideanDistance(i, j, targetX, targetY);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        closestX = i;
                        closestY = j;
                    }
                }
            }
        }
        Debug.Log(closestX + "closest" + closestY);
        return (closestX, closestY);
    }

    private static int CalculateManhattanDistance(int x1, int y1, int x2, int y2)
    {
        return Math.Abs(x1 - x2) + Math.Abs(y1 - y2);

    }

    private  double CalculateEuclideanDistance(int x1, int y1, int x2, int y2)
    {
        return Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));
    }


    public bool IsInRange (int targetX, int targetY)
    {
        double distance = CalculateEuclideanDistance(pXPos, pYPos, targetX, targetY);
        return distance <= abilityRange;
    }

}