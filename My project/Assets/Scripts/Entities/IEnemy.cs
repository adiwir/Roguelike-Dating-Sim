using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy
{
    //Observer stuff
    // private List<IObserver> _observers = new List<IObserver>();
    // private Vector3Int position;

    // public void AddObserver(IObserver observer){
    //     _observers.Add(observer);
    // }

    // public void RemoveObserver(IObserver observer){
    //     _observers.Remove(observer);
    // }

    // protected void NotifyObservers(Vector3Int currentPos){
    //     _observers.ForEach((_observer) => {
    //         _observer.OnNotify();
    //     })
    // }

    public abstract Vector3Int getPos();

    public abstract void takeDamage(int damage);
}


