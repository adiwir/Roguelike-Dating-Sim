using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Turtle : MonoBehaviour
{
    private EnemyController controller;
    
    public int timeThresh;
    public int spinCD;
    public int range;

    private int elapsedTime = 0;
    private int spinTime;

    private bool isAttacking;
    private bool isSpinning;
    private bool isBackSpinning;
    private bool hasSpun;

    public List<Vector2Int> attackTiles;
    private Vector2Int targetCell;

    private void Start()
    {
        controller = GetComponent<EnemyController>();
        isAttacking = false;
        isSpinning = false;
        isBackSpinning = false;
        hasSpun = false;

    }
    void LateUpdate()
    {
        if (isSpinning || isBackSpinning)
        {
            RapidSpin();
            elapsedTime = 0;
        }
        
        else if (CheckAdjacency() && !isAttacking && elapsedTime > timeThresh)
        {
            BasicAttack();
            isAttacking = false;
            controller.HideAttack(attackTiles);
            attackTiles.Clear();
            elapsedTime = 0;
        }
        
        else if (CheckRange() && !isAttacking && spinTime > spinCD)
        {
            attackTiles = GetAttackRange();
            controller.ShowAttack(attackTiles);
            isAttacking = true;
            elapsedTime = 0;
            spinTime = 0;
        }
        
        else if (isAttacking)
        {
            controller.ShowAttack(attackTiles);
            if (elapsedTime > timeThresh)
            {
                isSpinning = true;
                elapsedTime = 0;
                controller.HideAttack(attackTiles);
                attackTiles.Clear();
                isAttacking = false;
            }
        }

        else if (!isAttacking && elapsedTime > timeThresh)
        {
            controller.MoveAlongPath();
            elapsedTime = 0;
        }

        elapsedTime += 1;
        spinTime += 1;
    }

    private void BasicAttack()
    {
        controller.target.GetComponentInParent<Health>().decreaseHealthPoints(1);
        elapsedTime = 0;
    }

    private void RapidSpin()
    {
        if (isSpinning)
        {
            MoveFast();
        }

        else if (isBackSpinning)
        {
            MoveBackFast();
        }
    }

    private void MoveFast()
    {
        Vector3 target = GameObject.FindAnyObjectByType<Grid>().CellToWorld(new Vector3Int(targetCell.x, targetCell.y, 0));
        transform.position = Vector3.MoveTowards(transform.position, target, 15 * Time.deltaTime);
        if (GameObject.FindAnyObjectByType<Grid>().WorldToCell(transform.position) == new Vector3Int(controller.target.standingOnTile.gridLocation.x, controller.target.standingOnTile.gridLocation.y, 0)
            && !hasSpun)
        {
            controller.target.GetComponentInParent<Health>().decreaseHealthPoints(1);
            hasSpun = true;
        }
        if (transform.position == target && finished2.MapManager.Instance.map.ContainsKey(targetCell))
        {
            isSpinning = false;
            controller.SetPos();
            hasSpun = false;
        }
        else if (transform.position == target && (!finished2.MapManager.Instance.map.ContainsKey(targetCell) || target == controller.target.transform.position))
        {
            isSpinning = false;
            isBackSpinning = true;
            hasSpun = false;
        }
    }

    private void MoveBackFast()
    {
        Vector3 target = GameObject.FindAnyObjectByType<Grid>().CellToWorld(new Vector3Int(controller.enemyTile.gridLocation.x, controller.enemyTile.gridLocation.y, 0));
        transform.position = Vector3.MoveTowards(transform.position, target, 15 * Time.deltaTime);
        if (transform.position == target)
        {
            isBackSpinning = false;
            controller.SetPos();
        }
    }

    private bool CheckRange()
    {
        for (int y = range * (-1); y <= range; y++)
        {
            for (int x = range * (-1); x <= range; x++)
            {
                if (finished2.MapManager.Instance.map.ContainsKey(new Vector2Int(controller.enemyTile.gridLocation.x + x, controller.enemyTile.gridLocation.y + y))
                    && finished2.MapManager.Instance.map[new Vector2Int(controller.enemyTile.gridLocation.x + x, controller.enemyTile.gridLocation.y + y)] == controller.target.standingOnTile
                    && (x == 0 || y == 0) && (Mathf.Abs(x) != 1 || Mathf.Abs(y) != 1))
                {
                    return true;
                }
            }
        }
        return false;
    }

    private List<Vector2Int> GetAttackRange()
    {
        List<Vector2Int> aRange = new List<Vector2Int>();
        
        if (finished2.MapManager.Instance.map.ContainsKey(new Vector2Int(controller.target.standingOnTile.gridLocation.x, controller.target.standingOnTile.gridLocation.y)))
        {
            if (controller.target.standingOnTile.gridLocation.x > controller.enemyTile.gridLocation.x)
            {
                for (int x = 1; x <= range; ++x)
                {
                    aRange.Add(new Vector2Int(controller.enemyTile.gridLocation.x + x, controller.enemyTile.gridLocation.y));
                }
                targetCell = new Vector2Int(controller.enemyTile.gridLocation.x + 6, controller.enemyTile.gridLocation.y);
            }

            else if (controller.target.standingOnTile.gridLocation.x < controller.enemyTile.gridLocation.x)
            {
                for (int x = 1; x <= range; ++x)
                {
                    aRange.Add(new Vector2Int(controller.enemyTile.gridLocation.x - x, controller.enemyTile.gridLocation.y));
                }
                targetCell = new Vector2Int(controller.enemyTile.gridLocation.x - 6, controller.enemyTile.gridLocation.y);
            }

            else if (controller.target.standingOnTile.gridLocation.y > controller.enemyTile.gridLocation.y)
            {
                for (int y = 1; y <= range; ++y)
                {
                    aRange.Add(new Vector2Int(controller.enemyTile.gridLocation.x, controller.enemyTile.gridLocation.y + y));
                }
                targetCell = new Vector2Int(controller.enemyTile.gridLocation.x, controller.enemyTile.gridLocation.y + 6);
            }

            else if (controller.target.standingOnTile.gridLocation.y < controller.enemyTile.gridLocation.y)
            {
                for (int y = 1; y <= range; ++y)
                {
                    aRange.Add(new Vector2Int(controller.enemyTile.gridLocation.x, controller.enemyTile.gridLocation.y - y));
                }
                targetCell = new Vector2Int(controller.enemyTile.gridLocation.x, controller.enemyTile.gridLocation.y - 6);
            }
        }   
        return aRange;
    }

    private bool CheckAdjacency()
    {
        for (int y = -1; y <= 1; y++)
        {
            for (int x = -1; x <= 1; x++)
            {
                if (finished2.MapManager.Instance.map.ContainsKey(new Vector2Int(controller.enemyTile.gridLocation.x + x, controller.enemyTile.gridLocation.y + y)) 
                    && finished2.MapManager.Instance.map[new Vector2Int(controller.enemyTile.gridLocation.x + x, controller.enemyTile.gridLocation.y + y)] == controller.target.standingOnTile
                    && Mathf.Abs(x) + Mathf.Abs(y) == 1)
                {
                    return true;
                }
            }
        }
        return false;
    }
    
}
