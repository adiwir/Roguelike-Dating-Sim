using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

[CreateAssetMenu(fileName = "PlayerBasicAbility", menuName = "BasicAbility/PlayerBasicAbility")]
public class PlayerBasicAbility : BasicAbility
{
    [SerializeField] int damage = 1;
    string name;

    private void Awake()
    {
        //enemyStorage = EnemyStorage.Instance;
        //enemyStorage = GetComponment<EnemyStorage>();//TODO: Fixa detta imorgon
        name = "Whack";
        range = 2;
        //description = ""
    }

    public override int GetDamage()
    {
        return this.damage;
    }

    public override int GetRange()
    {
        return this.range;
    }
}