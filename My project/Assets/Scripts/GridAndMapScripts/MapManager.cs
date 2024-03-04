using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace finished2 
{
    public class MapManager : MonoBehaviour
    {
        private static MapManager _instance;
        public static MapManager Instance { get { return _instance; } }
        //public Dictionary<Vector3Int, IEnemy> enemyPositions { get; set; }
        //private Boss boss;
        //private StaticEnemy tempStaticEnemy;

        public Tilemap walkable, col;
        public GameObject overlayPrefab;
        public GameObject overlayContainer;

        public Dictionary<Vector2Int, OverlayTile> map;

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            } else
            {
                _instance = this;
            }
        }

        void Start()
        {
            //var tileMaps = gameObject.transform.ge.OrderByDescending(x => x.GetComponent<TilemapRenderer>().sortingOrder);
            map = new Dictionary<Vector2Int, OverlayTile>();

            //foreach (var tm in tileMaps)
            //{
                BoundsInt bounds = walkable.cellBounds;

                //for (int z = bounds.max.z; z > bounds.min.z; z--)
                //{
                    for (int y = bounds.min.y; y < bounds.max.y; y++)
                    {
                        for (int x = bounds.min.x; x < bounds.max.x; x++)
                        {
                            if (walkable.HasTile(new Vector3Int(x, y, 0)) && !col.HasTile(new Vector3Int(x, y, 0)))
                            {
                                if (!map.ContainsKey(new Vector2Int(x, y)))
                                {
                                    var overlayTile = Instantiate(overlayPrefab, overlayContainer.transform);
                                    var cellWorldPosition = walkable.GetCellCenterWorld(new Vector3Int(x, y, 0));
                                    overlayTile.transform.position = new Vector3(cellWorldPosition.x, cellWorldPosition.y, cellWorldPosition.z + 1);
                                    overlayTile.GetComponent<SpriteRenderer>().sortingOrder = walkable.GetComponent<TilemapRenderer>().sortingOrder;
                                    overlayTile.gameObject.GetComponent<OverlayTile>().gridLocation = new Vector3Int(x, y, 0);
                                    //overlayTile.gameObject.GetComponent<OverlayTile>().HideTile();
    
                                    map.Add(new Vector2Int(x, y), overlayTile.gameObject.GetComponent<OverlayTile>());
                                }
                            }
                        }
                    }
                //}
            //}
        }

        public void instantiateDoorNodes(int x, int y)
        {
            var overlayTile = Instantiate(overlayPrefab, overlayContainer.transform);
            var cellWorldPosition = walkable.GetCellCenterWorld(new Vector3Int(x, y, 0));
            overlayTile.transform.position = new Vector3(cellWorldPosition.x, cellWorldPosition.y, cellWorldPosition.z + 1);
            overlayTile.GetComponent<SpriteRenderer>().sortingOrder = walkable.GetComponent<TilemapRenderer>().sortingOrder;
            overlayTile.gameObject.GetComponent<OverlayTile>().gridLocation = new Vector3Int(x, y, 0);
            overlayTile.gameObject.GetComponent<OverlayTile>().HideTile();

            map.Add(new Vector2Int(x, y), overlayTile.gameObject.GetComponent<OverlayTile>());
        }
    }
}
