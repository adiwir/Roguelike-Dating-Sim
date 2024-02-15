using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActiveAbility : ScriptableObject, Ability
{
    string Name;
    public string Description;

    public int range;

    public Vector3Int targetSquare;

    //public abstract void useActive();

    public  abstract void UseAbility();

}