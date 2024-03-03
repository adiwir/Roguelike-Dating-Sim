using System;
using System.Numerics;
using UnityEngine;

public class ClosestSpot
{
    int range;
    public int pXPos;
    public int pYPos;

    int targetX;
    int targetY;
    Vector3Int mPos;

    public ClosestSpot() 
    { 
    }

    public (int, int) FindClosestSpot(Vector3Int playerPos, Vector3Int mousePos, int range)
    {
        pXPos = playerPos.x;
        pYPos = playerPos.y;
        targetX = mousePos.x;
        targetY = mousePos.y;

        if (IsInRange(targetX, targetY))
        {
            return (targetX, targetY);
        }

        int closestX = targetX;
        int closestY = targetY;
        double minDistance = double.MaxValue;

        // raden nedanför är skit

        int differenceX = Math.Abs(targetX - pXPos);
        int differenceY = Math.Abs(targetY - pYPos);

        int i = 0;
        while (!IsInRange(closestX, closestY))
        {
            targetX += (targetX < pXPos) ? 1 : -1;

            // Update cY coordinate
            targetY += (targetY < pYPos) ? 1 : -1;

        }
        return (closestX, closestY);

        //    for (int i = Math.Max(pXPos - range, 0); i <= Math.Min(pXPos + range, targetX); i++)
        //    {
        //        for (int j = Math.Max(pYPos - range, 0); j <= Math.Min(pYPos + range, targetY); j++)
        //        {
        //            if (IsInRange(i, j))
        //            {
        //                double distance = CalculateEuclideanDistance(i, j, targetX, targetY);
        //                if (distance < minDistance)
        //                {
        //                    minDistance = distance;
        //                    closestX = i;
        //                    closestY = j;
        //                }
        //            }
        //        }
        //    }

        //    return (closestX, closestY);
        //}

        //private static int CalculateManhattanDistance(int x1, int y1, int x2, int y2)
        //{
        //    return Math.Abs(x1 - x2) + Math.Abs(y1 - y2);
    
    }

    private  double CalculateEuclideanDistance(int x2, int y2)
    {
        return Math.Sqrt(Math.Pow(x2 - pXPos, 2) + Math.Pow(y2 - pYPos, 2));
    }


    public bool IsInRange (int targetX, int targetY)
    {
        double distance = CalculateEuclideanDistance(targetX, targetY);
        return distance <= range;
    }

}