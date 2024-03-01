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
    //private Health health;
   
   public Shield() 
   {
        isAttackAbility = false;
   }
    
    private void BecomeTemporarilyInvincible()
    {
        //Debug.Log("shouldBecomeInvincible");
        //Health.Instance.setInvincible(true);
        //yield return new WaitForSeconds(invincibilityDurationSeconds);
        //Health.Instance.setInvincible(false);
        Health.Instance.BecomeInvincible(invincibilityDurationSeconds);
    }

    public override void UseAbility(MonoBehaviour entity)
    {
        Debug.Log("trying to use shield");
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