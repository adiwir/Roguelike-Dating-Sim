using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEngine.GraphicsBuffer;


public class EnemyController : MonoBehaviour
{

    [SerializeField] Tilemap tilemap;

    public float moveSpeed = 1;


    private Vector3Int currentCell;
    public Vector2Int tilePos;

    private finished2.PathFinder pathFinder;
    private List<finished2.OverlayTile> path;
    public PlayerController target;
    public finished2.OverlayTile enemyTile;
    
    
    


    // Start is called before the first frame update
    void Start()
    {
        

        pathFinder = new finished2.PathFinder();
        path = new List<finished2.OverlayTile>();
        currentCell = tilemap.WorldToCell(transform.position);
        target = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        tilePos.x = currentCell.x;
        tilePos.y = currentCell.y;
        enemyTile = finished2.MapManager.Instance.map[tilePos];

    }

    

    private void Move(finished2.OverlayTile tile)
    {
        transform.position = Vector3.MoveTowards(transform.position, tilemap.GetCellCenterWorld(tile.gridLocation), moveSpeed);

        currentCell = tilemap.WorldToCell(transform.position);
        tilePos.x = currentCell.x;
        tilePos.y = currentCell.y;
        enemyTile = finished2.MapManager.Instance.map[tilePos];
        
    }

    public void MoveAlongPath()
    {
        path = pathFinder.FindPath(enemyTile, target.standingOnTile);

        if (path.Count > 0)
        {
            Move(path[0]);
            path.RemoveAt(0);

        }
    }

    public void ShowAttack(List<Vector2Int> attackTiles)
    {
        foreach (Vector2Int tilePos in attackTiles)
        {            
            finished2.MapManager.Instance.map[tilePos].ShowHurtTile();           
        }
    }

    public void HideAttack(List<Vector2Int> attackTiles)
    {
        foreach (Vector2Int tilePos in attackTiles)
        {           
            finished2.MapManager.Instance.map[tilePos].HideTile();           
        }
    }
}
