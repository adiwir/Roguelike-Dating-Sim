using System;
using System.Collections;
using UnityEngine;
public interface IEnemyObserver
{
    void UpdateEnemyPosition(Vector3 newPosition);
}