using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerBasicAbility", menuName = "BasicAbility/PlayerBasicAbility")]
public class PlayerBasicAbility : BasicAbility
{
    [SerializeField] int damage = 1;
    string name;

    //public __ image

    public override void UseAbility(Character character, Vector3Int targetTile)
    {

    }

    private void Awake()
    {
        name = "Whack";
        range = 1;
        perEnemyBaseDamage = 2;
        //description = ""
    }

}