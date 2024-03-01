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

    //public abstract void UseAbility(Character character, List<Vector3Int> targetedTiles);
    public abstract void UseAbility(List<Vector3Int> targetedTiles);

    public abstract void UseAbility(MonoBehaviour entity);

    public void CanIActivate()
    {
        Debug.Log(name);
    }

    public List<Vector3Int> GetAreaOfEffect() { return this.areaOfEffect; }

    public abstract void SetAreaOfEffect();

}