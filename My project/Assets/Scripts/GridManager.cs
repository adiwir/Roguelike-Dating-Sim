using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;



public class GridManager : MonoBehaviour
{
    private static GridManager _instance;
    public static GridManager Instance { get { return _instance; } }


    public GameObject nodePrefab;
    public GameObject nodeContainer;
    public Tilemap col;

    public Dictionary<Vector2Int, Node> map;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        var tileMaps = gameObject.transform.GetComponentsInChildren<Tilemap>().OrderByDescending(x => x.GetComponent<TilemapRenderer>().sortingOrder);
        map = new Dictionary<Vector2Int, Node>();

        foreach (var tm in tileMaps)
        {
            BoundsInt bounds = tm.cellBounds;

            for (int y = bounds.min.y; y < bounds.max.y; y++)
            {
                for (int x = bounds.min.x; x < bounds.max.x; x++)
                {
                    if (tm.HasTile(new Vector3Int(x, y, 0)) && !col.HasTile(new Vector3Int(x, y, 0)))
                    {
                        if (!map.ContainsKey(new Vector2Int(x, y)))
                        {
                            var nodeTile = Instantiate(nodePrefab, nodeContainer.transform);
                            var cellWorldPosition = tm.GetCellCenterWorld(new Vector3Int(x, y, 0));
                            nodeTile.GetComponent<Node>().SetPosition(new Vector3Int(x, y, 0));
                            nodeTile.transform.position = new Vector3(cellWorldPosition.x, cellWorldPosition.y, cellWorldPosition.z + 1);
                            nodeTile.GetComponent<SpriteRenderer>().sortingOrder = tm.GetComponent<TilemapRenderer>().sortingOrder;


                            map.Add(new Vector2Int(x, y), nodeTile.gameObject.GetComponent<Node>());
                            //map.Add(new Vector2Int(x, y), nodeTile);
                        }
                    }
                }
            }
        }
    }

    void Start()
    {
        
    }

    public Node GetNodeAtPos(Vector2Int pos)
    {
        return map[pos];
    }
}

