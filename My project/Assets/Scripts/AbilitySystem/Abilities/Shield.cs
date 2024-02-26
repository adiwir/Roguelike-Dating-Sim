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
        this.affectsAnArea = false;
    }

    public override void UseAbility(List<Vector3Int> targetedTiles)
    {

    }

    public override void SetAreaOfEffect()
    {
        throw new System.NotImplementedException();
    }
}