using finished2;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Door : MonoBehaviour
{
    public Tilemap map;
    private bool hasOpened = false;

    void Update()
    {
        if (EnemyPosStorage.Instance.enemyList.Count <= 4 && !hasOpened)
        {
            openFirstDoor();
        }
    }

    private void openFirstDoor()
    {
        hasOpened = true;

        map.DeleteCells(new Vector3Int(-3, -29, 0), new Vector3Int(2, 1, 1));
        //map.SetTile(new Vector3Int(-2, -29, 0), null);
        //map.SetTile(new Vector3Int(-1, -29, 0), null);

        map.SetTile(new Vector3Int(-3, -36, 0), null);
        map.SetTile(new Vector3Int(-2, -36, 0), null);
        map.SetTile(new Vector3Int(-1, -36, 0), null);

        finished2.MapManager.Instance.instantiateDoorNodes(-3, -30);
        finished2.MapManager.Instance.instantiateDoorNodes(-2, -30);
        finished2.MapManager.Instance.instantiateDoorNodes(-1, -30);

        finished2.MapManager.Instance.instantiateDoorNodes(-3, -29);
        finished2.MapManager.Instance.instantiateDoorNodes(-2, -29);
        finished2.MapManager.Instance.instantiateDoorNodes(-1, -29);


        finished2.MapManager.Instance.instantiateDoorNodes(-3, -37);
        finished2.MapManager.Instance.instantiateDoorNodes(-2, -37);
        finished2.MapManager.Instance.instantiateDoorNodes(-1, -37);

        finished2.MapManager.Instance.instantiateDoorNodes(-3, -36);
        finished2.MapManager.Instance.instantiateDoorNodes(-2, -36);
        finished2.MapManager.Instance.instantiateDoorNodes(-1, -36);

        
    }
}
