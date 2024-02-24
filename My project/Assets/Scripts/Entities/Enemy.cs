using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IEnemyObserver
{
    public Vector3Int pos;
    public Vector3Int GetPos()
    {
        return this.pos;
    }
    public abstract List<Vector3Int> GetCoveredArea();

    public abstract void TakeDamage(int damage);

    public abstract void OnDeath();

    public void UpdateEnemyPosition(Vector3 newPosition)
    {
        EnemyPosStorage.Instance.AddEnemy(this);
    }
}


