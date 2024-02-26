using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : ActiveAbility
{
    string Name = "Shield";
    //public __ image
    private float duration = 2;

    private void Awake()
    {
        this.range = 0;
    }

    public override void UseAbility(Character character, Vector3Int targetTile)
    {

    }
    
    public override void SetAreaOfEffect()
    {
        throw new System.NotImplementedException();
    }
}