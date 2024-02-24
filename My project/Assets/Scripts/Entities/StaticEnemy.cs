using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class StaticEnemy : MonoBehaviour, IEnemy
{
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private Tilemap col;
    [SerializeField] private int HP = 3;
    private Vector3Int pos;
    EnemySubject enemySubject;

    public void Awake()
    {
        //tilemap = GetComponent<Tilemap>();
        //col = GetComponent<Col>();
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
        UpdateEnemyPosition(this.pos);
    }

    public Vector3Int GetPos()
    {
        return this.pos;
    }

    public void OnDeath()
    {
        Debug.Log("IEnemy died");
        Destroy(this.gameObject);
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("Ouch");
        this.HP -= damage;
        if (this.HP < 0)
        {
            OnDeath();
        }
    }

    public void UpdateEnemyPosition(Vector3 newPosition)
    {
        EntityPosStorage.Instance.AddEnemy(this);
    }

    public List<Vector3Int> GetCoveredArea()
    {
        throw new NotImplementedException();
    }
}