using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class StaticEnemy : MonoBehaviour, Enemy
{
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private Tilemap col;
    private Vector3Int pos;
    GridManager gridManager;

    public void Awake()
    {
        gridManager = GridManager.Instance;
        tilemap = GetComponent<Tilemap>();
        pos = tilemap.WorldToCell(transform.position);
        //col = GetComponent<Col>();
    }

    public Vector3Int getPos()
    {
        return this.pos;
    }
}