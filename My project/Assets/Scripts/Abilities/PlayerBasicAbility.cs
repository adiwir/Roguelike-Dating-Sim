using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerBasicAbility", menuName = "BasicAbility/PlayerBasicAbility")]
public class PlayerBasicAbility : BasicAbility
{
    [SerializeField] int damage = 1;
    string name;
    private EnemyStorage enemyStorage = EnemyStorage.Instance;

    //public __ image

    public override void UseAbility(Character character, Vector3Int targetTile)
    {
        //start animation here
        Enemy enemy = enemyStorage.GetEnemyOnCell(targetTile);
        if (enemy != null)
        {
            enemy.takeDamage(damage);
        }
    }

    private void Awake()
    {
        name = "Whack";
        range = 1;
        //description = ""
    }

}