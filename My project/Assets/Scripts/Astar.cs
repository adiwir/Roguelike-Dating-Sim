using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Astar : MonoBehaviour
{
    [SerializeField] Tilemap tilemap;
    [SerializeField] GameObject player;
    private Vector3Int goal;

    private void Start()
    {
        Vector3 playerPos = player.transform.position;
        goal = tilemap.WorldToCell(playerPos);
    }


    private void Update()
    {
               
    }
    public void Pathfinding(Vector3Int start, Vector3Int goal)
    {

    }
}
