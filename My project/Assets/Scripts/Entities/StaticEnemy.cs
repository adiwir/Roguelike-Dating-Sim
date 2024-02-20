using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class StaticEnemy : MonoBehaviour, Enemy
{
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private Tilemap col;
    [SerializeField] private int HP = 3;
    private Vector3Int pos;

    public void Awake()
    {
        tilemap = GetComponent<Tilemap>();
        pos = tilemap.WorldToCell(transform.position);
        //col = GetComponent<Col>();
        
    }

    public Vector3Int getPos()
    {
        return this.pos;
    }

    public void takeDamage(int damage)
    {
        this.HP -= damage;
        if (this.HP < 0)
        {
            Debug.Log("Enemy died");
            //enemy dies here
        }
    }
}