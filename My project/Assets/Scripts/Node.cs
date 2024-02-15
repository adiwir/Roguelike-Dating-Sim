using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{

    public int G;
    public int H;
    public int F { get { return G + H; } }
    public Node parent;
    public Vector3Int position;

    public Node (Vector3Int pos)
    {
        this.position = pos;
    }

    public void SetPosition(Vector3Int pos)
    {
        this.position = pos;
    }
    public void HideTile()
    {
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
    }

    public void ShowTile()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
    }

}
