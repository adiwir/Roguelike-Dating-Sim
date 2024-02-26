using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActiveAbility : IAbility
{
    public string name;
    public string Description;
    public bool toggled = false;

    public int range;

    public Vector3Int targetSquare;
    public List<Vector3Int> areaOfEffect;

    public abstract void UseAbility(Character character, Vector3Int targetTile);

    public void CanIActivate()
    {
        Debug.Log(name);
    }

    public List<Vector3Int> GetAreaOfEffect() { return this.areaOfEffect; }

    public abstract void SetAreaOfEffect();

}