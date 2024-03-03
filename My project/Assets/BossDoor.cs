using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BossDoor : MonoBehaviour
{
    public Tilemap map;

    // Update is called once per frame

    void Start()
    {
        map = GetComponent<Tilemap>();
    }
    void Update()
    {
        if (EnemyPosStorage.Instance.enemyList.Count <= 4)
        {
            openFirstDoor();
        }

        if (EnemyPosStorage.Instance.enemyList.Count <= 1 )
        {
            openBossDoor();
        }
    }

    private void openFirstDoor()
    {
        map.SetTile(new Vector3Int(-3, -30, 2), null);
        map.SetTile(new Vector3Int(-2, -30, 2), null);
        map.SetTile(new Vector3Int(-1, -30, 2), null);

        map.SetTile(new Vector3Int(-3, -30, 4), null);
        map.SetTile(new Vector3Int(-2, -30, 4), null);
        map.SetTile(new Vector3Int(-1, -30, 4), null);

        map.SetTile(new Vector3Int(-3, -30, 6), null);
        map.SetTile(new Vector3Int(-2, -30, 6), null);
        map.SetTile(new Vector3Int(-1, -30, 6), null);

        map.SetTile(new Vector3Int(-3, -30, 8), null);
        map.SetTile(new Vector3Int(-2, -30, 8), null);
        map.SetTile(new Vector3Int(-1, -30, 8), null);

        map.SetTile(new Vector3Int(-3, -37, 2), null);
        map.SetTile(new Vector3Int(-2, -37, 2), null);
        map.SetTile(new Vector3Int(-1, -37, 2), null);

        map.SetTile(new Vector3Int(-3, -37, 4), null);
        map.SetTile(new Vector3Int(-2, -37, 4), null);
        map.SetTile(new Vector3Int(-1, -37, 4), null);

        map.SetTile(new Vector3Int(-3, -37, 6), null);
        map.SetTile(new Vector3Int(-2, -37, 6), null);
        map.SetTile(new Vector3Int(-1, -37, 6), null);

        map.SetTile(new Vector3Int(-3, -37, 8), null);
        map.SetTile(new Vector3Int(-2, -37, 8), null);
        map.SetTile(new Vector3Int(-1, -37, 8), null);
    }

    private void openBossDoor()
    {
        map.SetTile(new Vector3Int(24, -57, 2), null);
        map.SetTile(new Vector3Int(24, -58, 2), null);
        map.SetTile(new Vector3Int(24, -59, 2), null);

        map.SetTile(new Vector3Int(24, -57, 4), null);
        map.SetTile(new Vector3Int(24, -58, 4), null);
        map.SetTile(new Vector3Int(24, -59, 4), null);

        map.SetTile(new Vector3Int(24, -57, 6), null);
        map.SetTile(new Vector3Int(24, -58, 6), null);
        map.SetTile(new Vector3Int(24, -59, 6), null);

        map.SetTile(new Vector3Int(24, -57, 8), null);
        map.SetTile(new Vector3Int(24, -58, 8), null);
        map.SetTile(new Vector3Int(24, -59, 8), null);

        map.SetTile(new Vector3Int(24, -57, 10), null);
        map.SetTile(new Vector3Int(24, -58, 10), null);
        map.SetTile(new Vector3Int(24, -59, 10), null);

        map.SetTile(new Vector3Int(24, -57, 12), null);
        map.SetTile(new Vector3Int(24, -58, 12), null);
        map.SetTile(new Vector3Int(24, -59, 12), null);

        map.SetTile(new Vector3Int(24, -57, 14), null);
        map.SetTile(new Vector3Int(24, -58, 14), null);
        map.SetTile(new Vector3Int(24, -59, 14), null);

        map.SetTile(new Vector3Int(24, -57, 16), null);
        map.SetTile(new Vector3Int(24, -58, 16), null);
        map.SetTile(new Vector3Int(24, -59, 16), null);

        map.SetTile(new Vector3Int(24, -57, 18), null);
        map.SetTile(new Vector3Int(24, -58, 18), null);
        map.SetTile(new Vector3Int(24, -59, 18), null);
    }


}
