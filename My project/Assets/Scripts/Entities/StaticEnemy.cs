using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class StaticEnemy : MonoBehaviour, IEnemy, IEnemyObserver
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

    public Vector3Int getPos()
    {
        return this.pos;
    }

    public void takeDamage(int damage)
    {
        Debug.Log("Ouch");
        this.HP -= damage;
        if (this.HP < 0)
        {
            Debug.Log("IEnemy died");
            //enemy dies here
        }
    }

    public void UpdateEnemyPosition(Vector3 newPosition)
    {
        EntityPosStorage.Instance.AddEnemy(this);
    }
}