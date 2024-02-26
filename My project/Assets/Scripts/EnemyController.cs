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

    private void Update()
    {
        if (target.standingOnTile == enemyTile && finished2.MapManager.Instance.map.ContainsKey(new Vector2Int(enemyTile.gridLocation.x + 1, enemyTile.gridLocation.y)))
        {
            Move(finished2.MapManager.Instance.map[new Vector2Int(enemyTile.gridLocation.x + 1, enemyTile.gridLocation.y)]);
        }

        else if (target.standingOnTile == enemyTile && finished2.MapManager.Instance.map.ContainsKey(new Vector2Int(enemyTile.gridLocation.x, enemyTile.gridLocation.y + 1)))
        {
            Move(finished2.MapManager.Instance.map[new Vector2Int(enemyTile.gridLocation.x, enemyTile.gridLocation.y + 1)]);
        }

        else if (target.standingOnTile == enemyTile && finished2.MapManager.Instance.map.ContainsKey(new Vector2Int(enemyTile.gridLocation.x, enemyTile.gridLocation.y - 1)))
        {
            Move(finished2.MapManager.Instance.map[new Vector2Int(enemyTile.gridLocation.x, enemyTile.gridLocation.y - 1)]);
        }

        else if (target.standingOnTile == enemyTile && finished2.MapManager.Instance.map.ContainsKey(new Vector2Int(enemyTile.gridLocation.x - 1, enemyTile.gridLocation.y)))
        {
            Move(finished2.MapManager.Instance.map[new Vector2Int(enemyTile.gridLocation.x - 1, enemyTile.gridLocation.y)]);
        }
    }

    public void SetPos()
    {
        
        currentCell = tilemap.WorldToCell(transform.position);
        tilePos.x = currentCell.x;
        tilePos.y = currentCell.y;
        enemyTile = finished2.MapManager.Instance.map[tilePos];
    }
    private void Move(finished2.OverlayTile tile)
    {
        transform.position = Vector3.MoveTowards(transform.position, tilemap.GetCellCenterWorld(tile.gridLocation), moveSpeed);
        SetPos();    
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
            if (finished2.MapManager.Instance.map.ContainsKey(tilePos))
            {
                finished2.MapManager.Instance.map[tilePos].ShowHurtTile();
            }                       
        }
    }

    public void HideAttack(List<Vector2Int> attackTiles)
    {
        foreach (Vector2Int tilePos in attackTiles)
        {
            if (finished2.MapManager.Instance.map.ContainsKey(tilePos))
            {
                finished2.MapManager.Instance.map[tilePos].HideTile();
            }
        }
    }
}
