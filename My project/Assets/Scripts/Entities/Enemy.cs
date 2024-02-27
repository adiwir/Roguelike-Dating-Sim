using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IEnemyObserver
{
    public int maxHp;
    public int hp;
    public Vector3Int pos;
    public FloatingHealthBar healthBar;
    public Vector3Int GetPos()
    {
        return this.pos;
    }
    public abstract List<Vector3Int> GetCoveredArea();

    public  void TakeDamage(int damage)
    {
        this.hp -= damage;
        //healthBar.UpdateHealthBar(hp, maxHp);
        if (this.hp > 0) { healthBar.UpdateHealthBar(hp, maxHp); }
        if (this.hp <= 0)
        {
            OnDeath();
        }
        
        
        Debug.Log(this.hp);
    }

    public abstract void OnDeath();

    public void UpdateEnemyPosition(Vector3 newPosition)
    {
        EnemyPosStorage.Instance.AddEnemy(this);
    }
}


