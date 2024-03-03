using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyPosStorage : MonoBehaviour
{
    //använd observer pattern
    //GridManager gridManager = GridManager.Instance;
    private static EnemyPosStorage _instance;
    public static EnemyPosStorage Instance { get { return _instance; } }
    public List<Enemy> enemyList { get; set; }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        InitializeEnemyList();
    }

    void InitializeEnemyList()
    {
        this.enemyList = new List<Enemy>();
    }

    public void AddEnemy(Enemy enemy)
    {
        this.enemyList.Add(enemy);
    }

    public HashSet<Enemy> GetEnemyOnCell(List<Vector3Int> targetCells)
    {
        //I apologise for this function
        HashSet<Enemy> enemySet = new HashSet<Enemy>();
        if (enemyList != null)
        {
            foreach (Vector3Int targetCell in targetCells)
            {
                foreach (Enemy enemy in enemyList)
                {
                    Vector3Int enemyPos = enemy.GetPos();
                    if (enemy.GetType() == typeof(Boss))
                    {
                        List<Vector3Int> coveredArea = enemy.GetCoveredArea();
                        foreach (Vector3Int enemyCell in coveredArea)
                        {
                            if ((enemyCell.x == targetCell.x) && (enemyCell.y == targetCell.y))
                            {
                                enemySet.Add(enemy);
                            }
                        }
                    }
                    else
                    {
                        if ((enemyPos.x == targetCell.x) && (enemyPos.y == targetCell.y))
                        {
                            enemySet.Add(enemy);
                        }
                    }
                }
            }
            return enemySet;
        }
        return null;
    }

}