using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBasicAbility : BasicAbility
{
    string name;

    //public __ image
    public override void UseAbility()
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