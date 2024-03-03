using BarthaSzabolcs.Tutorial_SpriteFlash;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IEnemyObserver
{
    public int maxHp;
    public int hp;
    public Vector3Int pos;
    public FloatingHealthBar healthBar;
    public DamageFlash damageFlash;
    public Animator animator;
    public GameObject playerTarget;
    private bool isFlipped = false;

    public Vector3Int GetPos()
    {
        return this.pos;
    }

    public abstract List<Vector3Int> GetCoveredArea();

    public void TakeDamage(int damage)
    {
        damageFlash.Flash();
        this.hp -= damage;
        //healthBar.UpdateHealthBar(hp, maxHp);
        if (this.hp > 0) { healthBar.UpdateHealthBar(hp, maxHp); }
        if (this.hp <= 0)
        {
            OnDeath();
        }
        
        
        Debug.Log(this.hp);
    }

    public void updateTargetDirection()
    {
        Vector3 scale = transform.localScale;

        if (playerTarget.transform.position.y > transform.position.y)
        {
            isFlipped = true;

        }
        else
        {
            isFlipped = false;
        }

        if (playerTarget.transform.position.x > transform.position.x)
        {
            scale.x = Mathf.Abs(scale.x) * -1 * (isFlipped ? -1 : 1);
        }
        else
        {
            scale.x = Mathf.Abs(scale.x) * (isFlipped ? -1 : 1);
        }

        animator.SetBool("isFlipped", isFlipped);
        transform.localScale = scale;
    }

    public abstract void OnDeath();

    public void UpdateEnemyPosition(Vector3 newPosition)
    {
        EnemyPosStorage.Instance.AddEnemy(this);
    }
}


