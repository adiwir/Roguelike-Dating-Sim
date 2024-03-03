using finished2;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class MousePos : MonoBehaviour
{
    //[SerializeField] private Camera mainCamera;
    //public Vector3 pos;
    [SerializeField] private Grid grid;
    private static MousePos _instance;
    public static MousePos Instance { get { return _instance; } }

    public Vector3 nodeSelected;
    public Vector3Int nodeCell;
    //Vector3 nodeSelected = new Vector3(10000,0,0);
    public bool hasSelectedNode = false;

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
    }

    void FixedUpdate()
    {
        var focusedTileHit = GetFocusedOnTile();

        if (focusedTileHit.HasValue)
        {
            GameObject overlayTile = focusedTileHit.Value.collider.gameObject;
            //nodeSelected = GameObject.FindAnyObjectByType<Grid>().CellToWorld(overlayTile.GetComponentInChildren<OverlayTile>().gridLocation);
            
            transform.position = overlayTile.transform.position;
            nodeCell = grid.WorldToCell(transform.position);
            nodeCell.x += 1;
            nodeCell.y += 2;
            nodeSelected = grid.CellToWorld(nodeCell);

            hasSelectedNode = true;
            //gameObject.GetComponent<SpriteRenderer>().sortingOrder = overlayTile.GetComponent<SpriteRenderer>().sortingOrder;
            //raden ovan är för om vi skaffar en cursor
        }
        //pos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
    }

    //public Vector3 GetMousePos() {return this.pos;}

    public RaycastHit2D? GetFocusedOnTile()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 pos2d = new Vector2(pos.x, pos.y);

        RaycastHit2D[] hits = Physics2D.RaycastAll(pos2d, Vector2.zero);

        if(hits.Length > 0 )
        {
            return hits.OrderByDescending(i => i.collider.transform.position.z).First();
        }

        return null;
    }

    public Vector3 GetHoveredNode()
    {
        return this.nodeSelected;
    }

}