using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Shield : ActiveAbility
{
    string Name = "Shield";
    //public __ image
    private float invincibilityDurationSeconds = 3;
   
   public Shield() 
   {
        isAttackAbility = false;
   }
 
    private void Awake()
    {
        this.range = 0;
        this.affectsAnArea = false;
    }
    
    private IEnumerator BecomeTemporarilyInvincible()
    {
        health.setInvincible(true);
        yield return new WaitForSeconds(invincibilityDurationSeconds);
        health.setInvincible(false);
    }

    public override void UseAbility(MonoBehaviour entity)
    {
        BecomeTemporarilyInvincible();
    }

    public override void SetAreaOfEffect()
    {
        throw new NotImplementedException();
    }

    public override void UseAbility(List<Vector3Int> targetedTiles)
    {
        throw new NotImplementedException();
    }
}