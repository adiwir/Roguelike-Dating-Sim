using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

[CreateAssetMenu(fileName = "PlayerBasicAbility", menuName = "BasicAbility/PlayerBasicAbility")]
public class PlayerBasicAbility : BasicAbility
{
    [SerializeField] int damage = 1;
    string name;
    private EnemyStorage enemyStorage;

    //public __ image

    public override void UseAbility(Character character, Vector3Int targetTile)
    {
        //start animation here
        Debug.Log("enemyStorage " + (enemyStorage == null));
        Debug.Log("enemyStorage.GetEnemyOnCell(targetTile) " + (enemyStorage.GetEnemyOnCell(targetTile) == null));
        Enemy enemy = enemyStorage.GetEnemyOnCell(targetTile);
        if (enemy != null)
        {
            enemy.takeDamage(damage);
        }
    }

    private void Awake()
    {
        //enemyStorage = EnemyStorage.Instance;
        enemyStorage = GetComponment<EnemyStorage>();//TODO: Fixa detta imorgon
        name = "Whack";
        range = 1;
        //description = ""
    }

}