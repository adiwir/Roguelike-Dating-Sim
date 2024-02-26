using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class StaticEnemy : Enemy
{
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private Tilemap col;
    //[SerializeField] private int hp = 3;
    EnemySubject enemySubject;

    public void Awake()
    {
        //tilemap = GetComponent<Tilemap>();
        //col = GetComponent<Col>();
        this.hp = 3;
        pos = tilemap.WorldToCell(transform.position);
        enemySubject = GetComponent<EnemySubject>();
        if (enemySubject != null)
        {
            // Register as an observer
            enemySubject.RegisterObserver(this);
        }
        else
        {
            Debug.LogError("EnemySubject not found.");
        }
        Debug.Log(this.pos);
        UpdateEnemyPosition(this.pos);
    }

    public override void OnDeath()
    {
        Debug.Log("Enemy died");
        Destroy(this.gameObject);
    }

    public override void TakeDamage(int damage)
    {
        Debug.Log("Ouch");
        this.hp -= damage;
        if (this.hp <= 0)
        {
            OnDeath();
        }
    }

    public override List<Vector3Int> GetCoveredArea()
    {
        throw new NotImplementedException();
    }
}