using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyPosStorage : MonoBehaviour
{
    //anv�nd observer pattern
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

    public Enemy GetEnemyOnCell(Vector3Int targetCell)
    {
        if (enemyList != null)
        {
            
            foreach (Enemy enemy in enemyList)
            {
                Vector3Int enemyPos = enemy.GetPos();
                if((enemyPos.x == targetCell.x) && (enemyPos.y == targetCell.y))
                {
                    return enemy;
                }


                //if(enemy.GetPos() == targetCell)//TODO: g�r s� att detta kommer funka f�r Bossen
                //{
                //    return enemy;
                //}
                //implementera n�got h�r som kollar om de �r en boss f�r att se hur stora 

            }
        }
        return null;
    }

}