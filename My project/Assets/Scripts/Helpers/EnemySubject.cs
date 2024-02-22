using System.Collections.Generic;
using UnityEngine;

public class EnemySubject : MonoBehaviour
{
    private List<IEnemyObserver> observers = new List<IEnemyObserver>();

    public void RegisterObserver(IEnemyObserver observer)
    {
        observers.Add(observer);
    }

    public void UnregisterObserver(IEnemyObserver observer)
    {
        observers.Remove(observer);
    }

    public void NotifyObservers(Vector3 newPosition)
    {
        foreach (var observer in observers)
        {
            observer.UpdateEnemyPosition(newPosition);
        }
    }
}