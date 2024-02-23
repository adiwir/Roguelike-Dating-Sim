using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Sheep : MonoBehaviour
{
    private EnemyController controller;
    public int timeThresh;
    private int elapsedTime = 0;
    private bool isAttacking;
    public List<Vector2Int> attackTiles;

    private void Start()
    {
        controller = GetComponent<EnemyController>();
        isAttacking = false;
        timeThresh = 60;
        
    }
    void LateUpdate()
    {

        if (CheckRange() && !isAttacking)
        {
            attackTiles = GetAttackRange();
            controller.ShowAttack(attackTiles);
            isAttacking = true;
            elapsedTime = 0;
        }
        else if (isAttacking)
        {
            controller.ShowAttack(attackTiles);
            if (elapsedTime > timeThresh)
            {
                FireBall();
                elapsedTime = 0;
                controller.HideAttack(attackTiles);
                attackTiles.Clear();
                isAttacking = false;
            }

        }
        /*else if (path.Count > 0 && path[0] == player.standingOnTile && elapsedTime > timeThresh)
        {
            BasicAttack();
        }*/
        else if (!isAttacking && elapsedTime > timeThresh)
        {
            controller.MoveAlongPath();
            elapsedTime = 0;
        }

        /*
        if (elapsedTime > 90)
        {
            Debug.Log(CheckRange());
            elapsedTime = 0;
        }*/


        elapsedTime += 1;
    }

    private void BasicAttack()
    {
        //TODO: Deal damage to player
        Debug.Log("You have activated my trap card");
        elapsedTime = 0;
    }

    private void FireBall()
    {
        //TODO: Deal damage to player
        foreach (Vector2Int tilePos in attackTiles)
        {
            if (finished2.MapManager.Instance.map[tilePos] == controller.target.standingOnTile)
            {
                controller.target.GetComponentInParent<Health>().decreaseHealthPoints(1);
                return;
            }
        }
    }

    private bool CheckRange()
    {
        for (int y = -5; y <= 5; y++)
        {
            for (int x = -5; x <= 5; x++)
            {
                if (finished2.MapManager.Instance.map.ContainsKey(new Vector2Int(controller.enemyTile.gridLocation.x + x, controller.enemyTile.gridLocation.y + y))
                    && Mathf.Abs(x) + Mathf.Abs(y) <= 5
                    && finished2.MapManager.Instance.map[new Vector2Int(controller.enemyTile.gridLocation.x + x, controller.enemyTile.gridLocation.y + y)] == controller.target.standingOnTile)
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
        for (int y = -1; y <= 1; y++)
        {
            for (int x = -1; x <= 1; x++)
            {
                if (finished2.MapManager.Instance.map.ContainsKey(new Vector2Int(controller.target.standingOnTile.gridLocation.x + x, controller.target.standingOnTile.gridLocation.y + y))
                && Mathf.Abs(x) + Mathf.Abs(y) <= 1)
                {
                    aRange.Add(new Vector2Int(controller.target.standingOnTile.gridLocation.x + x, controller.target.standingOnTile.gridLocation.y + y));
                }
            }
        }
        return aRange;
    }
}
