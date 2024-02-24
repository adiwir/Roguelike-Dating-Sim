using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyBomb : ActiveAbility
{
    string Name = "StickyBomb";
    //public __ image
    private int amountOfTilesAffected = 5;
    private List<Vector3Int> TilesAffected = new();


    private void Awake()
    {
        this.range = 3;
    }

    private void OnEnable()
    {
        TilesAffected.Clear();
        TilesAffected.Add(new Vector3Int(0, 0, 0));
        TilesAffected.Add(new Vector3Int(1, 0, 0));
        TilesAffected.Add(new Vector3Int(-1, 0, 0));
        TilesAffected.Add(new Vector3Int(0, 1, 0));
        TilesAffected.Add(new Vector3Int(0, -1, 0));
    }

    public override void UseAbility(Character character, Vector3Int targetTile)
    {

    }

    public List<Vector3Int> GetAttackedTiles() 
    {
        return this.TilesAffected;

    }

}