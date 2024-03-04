using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : ActiveAbility
{
    private int amountOfareaOfEffect = 1;
    private int damage = 0;

    public Teleport() 
    {
        this.areaOfEffect = new();
        this.name = "Teleport";
        this.range = 4;
        //SetAreaOfEffect();
        this.affectsAnArea = true;
        isAttackAbility = false;
    }

    //public override void UseAbility(List<Vector3Int> targetedTiles)
    //{
    //    HashSet<Enemy> hitEnemies = EnemyPosStorage.Instance.GetEnemyOnCell(targetedTiles);
    //    if (hitEnemies != null)
    //    {
    //        foreach (Enemy enemy in hitEnemies)
    //        {
    //            Debug.Log(enemy);
    //            enemy.TakeDamage(damage);
    //        }
    //    }
    //}

    public override void UseAbility(PlayerController player)
    {
        Vector3 mousePos = MousePos.Instance.GetHoveredNode();
        Vector3 playerPos = player.GetPos();

        Vector3Int mouseTargetCell = player.tilemap.WorldToCell(mousePos);
        Vector3Int currentPos = player.tilemap.WorldToCell(playerPos);
        Vector2Int twoD = new Vector2Int(currentPos.x, currentPos.y);

        HashSet <Vector2Int> areaInRange = AreaInRange.CalcAreaInRange(range, twoD);
        if(areaInRange.Contains(new Vector2Int(mouseTargetCell.x, mouseTargetCell.y)))
        {
            player.transform.position = mousePos;
            player.UpdatePlayerPos(mousePos);
        }

        
        
    }

    public override void SetAreaOfEffect() //sets tiles that the abilities can affect
    {
        throw new System.NotImplementedException();
    }

    public override void UseAbility(List<Vector3Int> targetedTiles)
    {
        throw new System.NotImplementedException();
    }
}