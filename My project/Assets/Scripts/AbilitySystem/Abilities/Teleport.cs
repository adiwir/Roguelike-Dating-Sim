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

    public override void UseAbility(PlayerController player)
    {
        //Vector3 mousePos = MousePos.Instance.GetHoveredNode();
        Vector3 playerPos = player.GetPos();
        Vector3 addVec = new Vector3(0, 0, 0);
        //Vector3Int mouseplayerPos = player.tilemap.WorldToCell(mousePos);
        string orientation = player.GetOrientation();

        switch (orientation)
        {
            case "W":
                addVec.x += 1;
                break;

            case "S":
                addVec.x -= 1;
                break;

            case "A":
                addVec.y += 1;
                break;

            case "D":
                addVec.y -= 1;
                break;
        }

        for(int i=0; i < range; i++)
        {
            playerPos += addVec;
            Vector3.MoveTowards(player.transform.position, playerPos, 10);
            player.UpdatePlayerPos(playerPos);
        }
        //Vector3 target = Vector3.MoveTowards(player.transform.position, playerPos + , 10);
        //player.transform.position = target;
        //player.transform.position = target;
        //player.UpdatePlayerPos(target);
        

        //HashSet<Vector2Int> areaInRange = AreaInRange.CalcAreaInRange(range, twoD);

        //Vector3Int currentPos = player.tilemap.WorldToCell(playerPos);
        //Vector2Int twoD = new Vector2Int(currentPos.x, currentPos.y);

        //HashSet <Vector2Int> areaInRange = AreaInRange.CalcAreaInRange(range, twoD);
        //if(areaInRange.Contains(new Vector2Int(mouseT.x, mouseplayerPos.y)))
        //{
        //    player.transform.position = mousePos;
        //    player.UpdatePlayerPos(mousePos);
        //}

        
        
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