using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;




public class Astar 
{
    public List<Node> FindPath(Node start, Node end)
    {
        List<Node> openList = new List<Node>();
        List<Node> closedList = new List<Node>();

        if (openList.Count > 0)
        {
            openList.Clear();
            closedList.Clear();
        }

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

