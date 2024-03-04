using System;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public static class AreaInRange
{
    public static HashSet<Vector2Int> areaInRange = new();
    
    public static HashSet<Vector2Int> CalcAreaInRange(int range, Vector2Int currentPos)
    {
        areaInRange.Clear();

        for(int i = -range; i < range; i++)
        {
            for(int j = -range; j < range; j++)
            {
                areaInRange.Add(new Vector2Int(i, j) + currentPos);
            }
        }
        return areaInRange;
    }
}