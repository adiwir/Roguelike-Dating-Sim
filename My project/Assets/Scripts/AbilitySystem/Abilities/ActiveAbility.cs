using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActiveAbility : IAbility
{
    public string name;
    public string Description;
    public Sprite icon;
    public bool toggled = false;
    public bool affectsAnArea;
    public bool isAttackAbility;

    public int range;

    public Vector3Int targetSquare;
    public List<Vector3Int> areaOfEffect;

    public abstract void UseAbility(List<Vector3Int> targetedTiles);

    public abstract void UseAbility(PlayerController player);

    public void CanIActivate()
    {
        Debug.Log(name);
    }

    public List<Vector3Int> GetAreaOfEffect() { return this.areaOfEffect; }

    public abstract void SetAreaOfEffect();

    public Sprite GetIcon() { return this.icon; }

    internal string GetName()
    {
        return this.name;
    }
}