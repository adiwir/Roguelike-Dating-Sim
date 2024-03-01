using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyBomb : ActiveAbility
{
    //public __ image
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
        //this.icon = Assets/Prefabs/sticky_bomb.png;
    }

    private static void CreateSprite(Sprite sprite)
    {

    }

    public override void UseAbility(List<Vector3Int> targetedTiles)
    {
        //Vector3Int staticEnemyPos = new Vector3Int(10, -1, 0);
        Enemy enemy;
        foreach (Vector3Int tile in targetedTiles)
        {
            //Debug.Log(tile);
            //if(tile == staticEnemyPos) { Debug.Log("targeted static enemy"); }

            enemy = EnemyPosStorage.Instance.GetEnemyOnCell(tile);
            if (enemy != null)
            {
                Debug.Log("hitting enemy");
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