using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEngine.GraphicsBuffer;


public class EnemyController : MonoBehaviour
{

    [SerializeField] Tilemap tilemap, col;

    private float moveSpeed = 1;
    private Vector3Int cellPos, targetCell;
    private Vector3 currentPos, targetPos;
    private int elapsedTime = 0;
    private int timeBetweenPathfinding = 0;

    private Astar pathfinder;
    private Node currentNode, targetNode;
    private List<Node> path;
    public GameObject player;
    


    // Start is called before the first frame update
    void Start()
    {
        path = new List<Node>();
        
        cellPos = tilemap.WorldToCell(transform.position);
        currentPos = tilemap.GetCellCenterWorld(cellPos);
        Vector2Int nodePos = new Vector2Int(cellPos.x, cellPos.y);
        currentNode = GridManager.Instance.GetNodeAtPos(nodePos);

        targetCell = tilemap.WorldToCell(player.transform.position);
        targetPos = tilemap.GetCellCenterWorld(targetCell);
        Vector2Int tNodePos = new Vector2Int(targetCell.x, targetCell.y);
        targetNode = GridManager.Instance.GetNodeAtPos(tNodePos);

        path = FindPath(currentNode, targetNode);
    }

    void FixedUpdate()
    {
        
        cellPos = tilemap.WorldToCell(transform.position);
        Vector2Int nodePos = new Vector2Int(cellPos.x, cellPos.y);
        currentNode = GridManager.Instance.GetNodeAtPos(nodePos);
        GridManager.Instance.map.TryGetValue(nodePos, out Node node);

        targetCell = tilemap.WorldToCell(player.transform.position);
        targetPos = tilemap.GetCellCenterWorld(targetCell);
        Vector2Int tNodePos = new Vector2Int(targetCell.x, targetCell.y);
        targetNode = GridManager.Instance.GetNodeAtPos(tNodePos);

        Move();


        /*if (timeBetweenPathfinding > 60)
        {
            foreach (Node p in path)
            {
                p.HideTile();
            }
            path.Clear();
            path = FindPath(currentNode, targetNode);
            StopAllCoroutines();
            //StartCoroutine(Move());
            
            timeBetweenPathfinding = 0;
            foreach (Node p in path)
            {
                p.ShowTile();
            }
        }*/
        

        elapsedTime += 1;
        timeBetweenPathfinding += 1;
    }

    private void Move()
    {
        if (elapsedTime > 60)
        {
            /*targetPos = tilemap.GetCellCenterWorld(target[0].position);
            target.RemoveAt(0);
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed);
            */

            /*Vector3 target = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            Vector3Int tar = tilemap.WorldToCell(target);
            tar.x -= 1;
            target = tilemap.GetCellCenterWorld(tar);*/

            Vector3Int tar = path[1].position;
            Debug.Log(path[1].position);
            Debug.Log(cellPos);
            path.RemoveAt(0);
            Vector3 target = tilemap.GetCellCenterWorld(tar);

            if (CanMove(tar))
            {
                transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed);
            }
            elapsedTime = 0;
        }   
        
    }
    private bool CanMove(Vector3Int target)
    {
        if (!tilemap.HasTile(target) || col.HasTile(target))
        {
            return false;
        }
        return true;

    }

    public List<Node> FindPath(Node start, Node end)
    {
        List<Node> openList = new List<Node>();
        List<Node> closedList = new List<Node>();

        openList.Add(start);

        while (openList.Count > 0)
        {
            Node currentNode = openList.OrderBy(x => x.F).First();

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            if (currentNode == end)
            {
                return GetFinishedList(start, end);
            }

            foreach (var node in GetNeighbourNodes(currentNode))
            {
                if (closedList.Contains(node) || Mathf.Abs(currentNode.position.z - node.position.z) > 1)
                {
                    continue;
                }

                node.G = GetManhattenDistance(start, node);
                node.H = GetManhattenDistance(end, node);

                node.parent = currentNode;

                if (!openList.Contains(node))
                {
                    openList.Add(node);
                }
            }
        }

        return new List<Node>();
    } 

    private List<Node> GetFinishedList(Node start, Node end)
    {
        List<Node> finishedList = new List<Node>();
        Node currentNode = end;

        while (currentNode != null)
        {
            finishedList.Add(currentNode);
            currentNode = currentNode.parent;
        }

        finishedList.Reverse();
        return finishedList;
    }

    private int GetManhattenDistance(Node start, Node node)
    {
        return Mathf.Abs(start.position.x - node.position.x) + Mathf.Abs(start.position.y - node.position.y);
    }

    private List<Node> GetNeighbourNodes(Node currentNode)
    {
        var map = GridManager.Instance.map;
        List<Node> neighbours = new List<Node>();

        //right
        Vector2Int locationToCheck = new Vector2Int(
            currentNode.position.x + 1,
            currentNode.position.y
        );

        if (map.ContainsKey(locationToCheck))
        {
            neighbours.Add(map[locationToCheck]);
        }

        //left
        locationToCheck = new Vector2Int(
            currentNode.position.x - 1,
            currentNode.position.y
        );

        if (map.ContainsKey(locationToCheck))
        {
            neighbours.Add(map[locationToCheck]);
        }

        //up
        locationToCheck = new Vector2Int(
            currentNode.position.x,
            currentNode.position.y + 1
        );

        if (map.ContainsKey(locationToCheck))
        {
            neighbours.Add(map[locationToCheck]);
        }

        //down
        locationToCheck = new Vector2Int(
            currentNode.position.x,
            currentNode.position.y - 1
        );

        if (map.ContainsKey(locationToCheck))
        {
            neighbours.Add(map[locationToCheck]);
        }

        return neighbours;
    }
}
