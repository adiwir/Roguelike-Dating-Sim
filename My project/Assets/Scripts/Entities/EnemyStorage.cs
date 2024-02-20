using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyStorage : MonoBehaviour
{
    //GridManager gridManager = GridManager.Instance;
    private static EnemyStorage _instance;
    public static EnemyStorage Instance {  get { return _instance; } }
    public List<Enemy> enemyList { get; set; }
    public Dictionary<Vector3Int, Enemy> enemyPositions { get; set; }

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
        InitializeEnemyPositionMap();
    }

    void InitializeEnemyList()
    {
        this.enemyList = new List<Enemy>();
        //enemyList.Add(boss);
        //enemyList.Add(tempStaticEnemy);
    }
    private void InitializeEnemyPositionMap()
    {
        foreach (Enemy enemy in enemyList)
        {
            AddEnemy(enemy);
        }
    }
    public void AddEnemy(Enemy enemy) //behöver ändra detta sen så att vi uppdaterar vad vi har här, Integer?
    {
        enemyPositions.Add(enemy.getPos(), enemy);
    }

    public Enemy GetEnemyOnCell(Vector3Int targetCell)
    {
        Enemy enemyOnCell;
        if (enemyPositions.TryGetValue(targetCell, out enemyOnCell))
        {
            return enemyOnCell;
        }
        else
        {
            return null;
        }
    }

}