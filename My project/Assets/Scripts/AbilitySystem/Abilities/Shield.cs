using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Shield : ActiveAbility
{
    private float invincibilityDurationSeconds = 3;
   
   public Shield() 
   {
        this.name = "Shield";
        isAttackAbility = false;
   }
    
    private void BecomeTemporarilyInvincible()
    {
        Health.Instance.BecomeInvincible(invincibilityDurationSeconds);
    }

    public override void UseAbility(PlayerController player)
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