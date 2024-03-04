using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyBomb : ActiveAbility
{
    private int amountOfareaOfEffect = 5;
    private int damage = 3;

    public StickyBomb() 
    {
        //this.icon = 
        this.areaOfEffect = new();
        this.name = "StickyBomb";
        this.range = 6;
        SetAreaOfEffect();
        this.affectsAnArea = true;
        isAttackAbility = true;
    }

    public override void UseAbility(List<Vector3Int> targetedTiles)
    {
        //Debug.Log("Nr of tiles: " + targetedTiles.Count);
        HashSet<Enemy> hitEnemies = EnemyPosStorage.Instance.GetEnemyOnCell(targetedTiles);
        //Debug.Log("Nr. of enemies: " + hitEnemies.Count);
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
    }
}