using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActiveAbility : Ability
{
    //public string name;
    public string Description;

    public int range;

    public Vector3Int targetSquare;

    public  abstract void UseAbility(Character character, Vector3Int targetTile);

}