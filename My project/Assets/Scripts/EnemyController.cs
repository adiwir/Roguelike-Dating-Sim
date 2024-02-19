using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEngine.GraphicsBuffer;


public class EnemyController : MonoBehaviour
{

    [SerializeField] Tilemap tilemap, col;

    public float moveSpeed = 1;
    private Vector3Int currentCell, targetCell;
    private Vector3 currentPos, targetPos;
    private int elapsedTime = 0;
    private int timeBetweenPathfinding = 0;
    private Astar pathfinder;
    private Node currNode, targetNode;
    public List<Node> path;
    public GameObject player;
    


    // Start is called before the first frame update
    void Start()
    {
        path = new List<Node>();
        pathfinder = new Astar();

        currentCell = tilemap.WorldToCell(transform.position);
        currentPos = tilemap.GetCellCenterWorld(currentCell);
        currNode = GridManager.Instance.GetNodeAtPos(new Vector2Int(currentCell.x, currentCell.y));

        targetCell = tilemap.WorldToCell(player.transform.position);
        targetPos = tilemap.GetCellCenterWorld(targetCell);
        targetNode = GridManager.Instance.GetNodeAtPos(new Vector2Int(targetCell.x, targetCell.y));

        path = pathfinder.FindPath(currNode, targetNode);
        foreach (Node p in path)
        {
            p.ShowTile();
        }
    }

    void FixedUpdate()
    {
        
        
        
        targetCell = tilemap.WorldToCell(player.transform.position);
        targetPos = tilemap.GetCellCenterWorld(targetCell);
        targetNode = GridManager.Instance.GetNodeAtPos(new Vector2Int(targetCell.x, targetCell.y));
        
        

        /*
        //Move(path[1]);
        //path.RemoveAt(1);

        if (path.Count > 1)
        {
            foreach (Node p in path)
            {
                p.HideTile();
            }

            foreach (Node p in path)
            {
                p.ShowTile();
            }
            
            if (elapsedTime > 30)
            {
                Move(path[1]);
                path.RemoveAt(1);
            }
            
        }*/

        
        if (timeBetweenPathfinding > 60)
        {
            /*
            path.Clear();
            path = pathfinder.FindPath(currNode, targetNode);
            */
            if (path.Count > 1)
            {
                
                foreach (Node p in path)
                {
                    p.HideTile();
                }
                path.Clear();
                path = FindPath(currNode, targetNode);
                //path.ShowTile();
                
                Move(path[1]);
                path.RemoveAt(1);

                timeBetweenPathfinding = 0;
                    
                foreach (Node p in path)
                {
                    p.ShowTile();
                }
                    
            }

        }

        elapsedTime += 1;
        timeBetweenPathfinding += 1;
    }

    private void Move(Node node)
    {
        /*targetPos = tilemap.GetCellCenterWorld(target[0].position);
        target.RemoveAt(0);
        transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed);
        */

        /*Vector3 target = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Vector3Int tar = tilemap.WorldToCell(target);
        tar.x -= 1;
        target = tilemap.GetCellCenterWorld(tar);*/

        /*Vector3Int tar = path[1].position;
        Debug.Log(path[1].position);
        Debug.Log(currentCell);
        path.RemoveAt(0);*/

        

        if (CanMove(node.position))
        {
            
            transform.position = Vector3.MoveTowards(transform.position, tilemap.GetCellCenterWorld(node.position), moveSpeed);

            currentCell = tilemap.WorldToCell(transform.position);
            currentPos = tilemap.GetCellCenterWorld(currentCell);
            currNode = GridManager.Instance.GetNodeAtPos(new Vector2Int(currentCell.x, currentCell.y));
        }
        elapsedTime = 0;   
    }
    private bool CanMove(Vector3Int target)
    {
        if (!tilemap.HasTile(target) || col.HasTile(target))
        {
            return false;
        }
        return true;

    }

}
