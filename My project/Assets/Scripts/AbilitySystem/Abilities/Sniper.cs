using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : ActiveAbility
{
    private int amountOfareaOfEffect = 1;
    private int damage = 3;

    public Sniper() 
    {
        this.areaOfEffect = new();
        this.name = "Sniper";
        this.range = 14;
        SetAreaOfEffect();
        this.affectsAnArea = true;
        isAttackAbility = true;
    }

    public override void UseAbility(List<Vector3Int> targetedTiles)
    {
        HashSet<Enemy> hitEnemies = EnemyPosStorage.Instance.GetEnemyOnCell(targetedTiles);
        if (hitEnemies != null)
        {
            foreach (Enemy enemy in hitEnemies)
            {
                Debug.Log(enemy);
                enemy.TakeDamage(damage);
            }
        }
    }

    public override void UseAbility(PlayerController player)
    {
        throw new System.NotImplementedException();
    }

    public override void SetAreaOfEffect() //sets tiles that the abilities can affect
    {
        areaOfEffect.Clear();
        areaOfEffect.Add(new Vector3Int(0, 0, 0));
    }
}