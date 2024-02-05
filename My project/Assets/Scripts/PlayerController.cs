using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Android.LowLevel;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private Grid grid;
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private Tilemap col;
        

    public float moveSpeed;
    private Vector3Int origPos;
    private Vector3Int targetCell;
    private Vector3 targetPosition;


    private void Start()
    {
        
        targetCell = grid.WorldToCell(transform.position);
        
        targetPosition = grid.CellToWorld(targetCell);
        
    }


    void Update()
    {
        origPos = grid.WorldToCell(transform.position);
        targetCell = origPos;

        if (Input.GetKeyDown(KeyCode.W))
        {   
            
            targetCell.x += 1;
            if (CanMove(targetCell))
            {
                targetPosition = grid.CellToWorld(targetCell);
                MovePlayer(targetPosition);
            }   
        }

        else if (Input.GetKeyDown(KeyCode.S))
        {
            targetCell.x -= 1;
            if (CanMove(targetCell))
            {
                targetPosition = grid.CellToWorld(targetCell);
                MovePlayer(targetPosition);
            }
        }

        else if (Input.GetKeyDown(KeyCode.A))
        {
            targetCell.y += 1;
            if (CanMove(targetCell))
            {
                targetPosition = grid.CellToWorld(targetCell);
                MovePlayer(targetPosition);
            }
        }

        else if (Input.GetKeyDown(KeyCode.D))
        {
            targetCell.y -= 1;
            if (CanMove(targetCell))
            {
                targetPosition = grid.CellToWorld(targetCell);
                MovePlayer(targetPosition);
            }
        }
    }

    void MovePlayer(Vector3 target)
    {
        transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed);

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
