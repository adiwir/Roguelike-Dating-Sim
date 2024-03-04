using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stomp : ActiveAbility
{
    private int amountOfareaOfEffect = 8;
    private int damage = 2;

    public Stomp() 
    {
        this.areaOfEffect = new();
        this.name = "Stomp";
        this.range = 1;
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

    public override void UseAbility(MonoBehaviour entity)
    {
        throw new System.NotImplementedException();
    }

    public override void SetAreaOfEffect() //sets tiles that the abilities can affect
    {
        areaOfEffect.Clear();
        areaOfEffect.Add(new Vector3Int(0, 0, 0));
        areaOfEffect.Add(new Vector3Int(1, 0, 0));
        areaOfEffect.Add(new Vector3Int(-1, 0, 0));
        areaOfEffect.Add(new Vector3Int(0, 1, 0));
        areaOfEffect.Add(new Vector3Int(0, -1, 0));
        areaOfEffect.Add(new Vector3Int(-1, -1, 0));
        areaOfEffect.Add(new Vector3Int(1, 1, 0));
        areaOfEffect.Add(new Vector3Int(1, -1, 0));
        areaOfEffect.Add(new Vector3Int(-1, 1, 0));
    }
}