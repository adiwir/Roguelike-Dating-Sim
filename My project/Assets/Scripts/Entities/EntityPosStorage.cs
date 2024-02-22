using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EntityPosStorage : MonoBehaviour
{

    //använd observer pattern
    //GridManager gridManager = GridManager.Instance;
    private static EntityPosStorage _instance;
    public static EntityPosStorage Instance { get { return _instance; } }
    public List<IEnemy> enemyList { get; set; }
    //public Dictionary<Vector3Int, IEnemy> enemyPositions { get; set; }

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
        //InitializeEnemyPositionMap();
    }

    private void Update()
    {
        //if (enemyList != null) 
        //{
        //    foreach (IEnemy enemy in enemyList)
        //    {
        //        AddEnemy(enemy);
        //    }
        //}
    }

    void InitializeEnemyList()
    {
        this.enemyList = new List<IEnemy>();
        //enemyList.Add(boss);
        //enemyList.Add(tempStaticEnemy);
    }

    public void AddEnemy(IEnemy enemy)
    {
        this.enemyList.Add(enemy);
    }
    //private void InitializeEnemyPositionMap()
    //{
    //    enemyPositions = new Dictionary<Vector3Int, IEnemy>();
    //}

    public IEnemy GetEnemyOnCell(Vector3Int targetCell)
    {
        //IEnemy enemyOnCell;
        //if (enemyPositions.TryGetValue(targetCell, out enemyOnCell))
        //{
        //    return enemyOnCell;
        //}
        //else
        //{
        //    return null;
        //}
        if (enemyList != null)
        {
            foreach (IEnemy enemy in enemyList)
            {
                if(enemy.getPos() == targetCell)//TODO: gör så att detta kommer funka för Bossen
                {
                    return enemy;
                }
                //implementera något här som kollar om de är en boss för att se hur stora 

            }
        }
        return null;
    }

}