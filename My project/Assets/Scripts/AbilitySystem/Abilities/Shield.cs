using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Shield : ActiveAbility
{
    string Name = "Shield";
    private float invincibilityDurationSeconds = 3;
    //private Health health;
   
   public Shield() 
   {
        this.name = "Shield";
        isAttackAbility = false;
   }
    
    private void BecomeTemporarilyInvincible()
    {
        Health.Instance.BecomeInvincible(invincibilityDurationSeconds);
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