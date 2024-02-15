using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Android.LowLevel;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private Tilemap tilemap;
    [SerializeField] private Tilemap col;
    
    Character character;
    public float moveSpeed;
    private Vector3Int origPos;
    private Vector3Int targetCell;
    private Vector3 targetPosition;
    //private int elapsedTime = 0;
    private float elapsedTime = 0f;
    private float basicCd = 1f;
    private float movementCd = 0.1f;


    private void Awake()
    {
        Debug.Log("will get character");
        character = GetComponent<Character>();
    }

    private void Start()
    {
        targetCell = tilemap.WorldToCell(transform.position);
        
        targetPosition = tilemap.GetCellCenterWorld(targetCell);
    }


    void FixedUpdate()
    {
        origPos = tilemap.WorldToCell(transform.position);
        targetCell = origPos;

        //elapsedTime += Time.fixedDeltaTime;
        //elapsedBasicAttackTime += Time.fixedDeltaTime;

        //PlayerMovement

        if (Input.GetKey(KeyCode.W))
        {   
            targetCell.x += 1;
            if (CanMove(targetCell))
            {
                targetPosition = tilemap.GetCellCenterWorld(targetCell);
                MovePlayer(targetPosition);
                character.SetOrientation("W");
            }   
        }

        else if (Input.GetKey(KeyCode.S))
        {
            targetCell.x -= 1;
            if (CanMove(targetCell))
            {
                targetPosition = tilemap.GetCellCenterWorld(targetCell);
                MovePlayer(targetPosition);
                character.SetOrientation("S");
            }
        }

        else if (Input.GetKey(KeyCode.A))
        {
            targetCell.y += 1;
            if (CanMove(targetCell))
            {
                targetPosition = tilemap.GetCellCenterWorld(targetCell);
                MovePlayer(targetPosition);
                character.SetOrientation("A");
            }
        }

        else if (Input.GetKey(KeyCode.D))
        {
            targetCell.y -= 1;
            if (CanMove(targetCell))
            {
                targetPosition = tilemap.GetCellCenterWorld(targetCell);
                MovePlayer(targetPosition);
                character.SetOrientation("D");
            }
        }

        if (Input.GetButton("Slash1") && (basicCd <= 0))
        {
            print("hello");
            attackTowardsOrientation();
            basicCd = 1f;
        } else
        {
            basicCd -= Time.fixedDeltaTime;
        }

        //elapsedTime += 6;
    }

    void attackTowardsOrientation()
    {
        string orientation = character.getOrientationAsString();
        //Debug.Log(orientation);
    }


    void MovePlayer(Vector3 target)
    {
        if(movementCd <= 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed);
            movementCd = 0.1f;
        } else
        {
            movementCd -= Time.fixedDeltaTime;
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
