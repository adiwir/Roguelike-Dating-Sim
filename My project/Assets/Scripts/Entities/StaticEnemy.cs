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
        this.maxHp = 30;
        this.hp = maxHp;
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

    private void Start()
    {
        healthBar.UpdateHealthBar(hp, maxHp);
    }

    public override void OnDeath()
    {
        Debug.Log("Enemy died");
        Destroy(this.gameObject);
    }

    public override List<Vector3Int> GetCoveredArea()
    {
        throw new NotImplementedException();
    }
}