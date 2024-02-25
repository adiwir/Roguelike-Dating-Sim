using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasicAbility : ScriptableObject, IAbility
{
    string Name;
    public string Description;
    public int range;
    //public float perEnemyBaseDamage;
    //public __ image

    //public abstract void UseAbility(Character character, Vector3Int targetTile);

    public abstract int GetDamage();

    public abstract int GetRange();

}