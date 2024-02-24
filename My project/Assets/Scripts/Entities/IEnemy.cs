using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy : IEnemyObserver
{
    public Vector3Int GetPos();
    public List<Vector3Int> GetCoveredArea();

    public void TakeDamage(int damage);

    public void OnDeath();
}


