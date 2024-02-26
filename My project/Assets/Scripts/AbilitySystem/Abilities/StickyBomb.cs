using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyBomb : ActiveAbility
{
    //public __ image
    private int amountOfareaOfEffect = 5;

    public StickyBomb() 
    {
        this.areaOfEffect = new();
        this.name = "StickyBomb";
        this.range = 6;
        SetAreaOfEffect();
    }

    public override void UseAbility(Character character, Vector3Int targetTile)
    {

    }

    public override void SetAreaOfEffect() //sets tiles that the abilities can affect
    {
        areaOfEffect.Clear();
        areaOfEffect.Add(new Vector3Int(0, 0, 0));
        areaOfEffect.Add(new Vector3Int(1, 0, 0));
        areaOfEffect.Add(new Vector3Int(-1, 0, 0));
        areaOfEffect.Add(new Vector3Int(0, 1, 0));
        areaOfEffect.Add(new Vector3Int(0, -1, 0));
    }
}