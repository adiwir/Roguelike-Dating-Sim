using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Android.LowLevel;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private Tilemap tilemap;
    [SerializeField] private Tilemap col;
        

    public float moveSpeed;
    private Vector3Int origPos;
    private Vector3Int targetCell;
    private Vector3 targetPosition;
    private int elapsedTime = 0;


    private void Start()
    {
        
        targetCell = tilemap.WorldToCell(transform.position);
        
        targetPosition = tilemap.GetCellCenterWorld(targetCell);
        
    }


    void FixedUpdate()
    {
        origPos = tilemap.WorldToCell(transform.position);
        targetCell = origPos;

        if (Input.GetKey(KeyCode.W))
        {   
            targetCell.x += 1;
            if (CanMove(targetCell))
            {
                targetPosition = tilemap.GetCellCenterWorld(targetCell);
                MovePlayer(targetPosition);
            }   
        }

        else if (Input.GetKey(KeyCode.S))
        {
            targetCell.x -= 1;
            if (CanMove(targetCell))
            {
                targetPosition = tilemap.GetCellCenterWorld(targetCell);
                MovePlayer(targetPosition);
            }
        }

        else if (Input.GetKey(KeyCode.A))
        {
            targetCell.y += 1;
            if (CanMove(targetCell))
            {
                targetPosition = tilemap.GetCellCenterWorld(targetCell);
                MovePlayer(targetPosition);
            }
        }

        else if (Input.GetKey(KeyCode.D))
        {
            targetCell.y -= 1;
            if (CanMove(targetCell))
            {
                targetPosition = tilemap.GetCellCenterWorld(targetCell);
                MovePlayer(targetPosition);
            }
        }

        elapsedTime += 6;
    }

    void MovePlayer(Vector3 target)
    {
        if(elapsedTime > 60)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed);
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
    
}
