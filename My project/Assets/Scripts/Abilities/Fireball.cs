using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : ActiveAbility
{
    string Name = "Fireball";
    //public __ image
    private int amountOfTilesAffected = 5;


    private void Awake()
    {
        this.range = 3;
    }

    public override void UseAbility(Character character, Vector3Int targetTile)
    {

    }

}