using System;
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
    private float elapsedTime = 0f;
    private float basicCd = 1f;
    private float abilityCd = 1f;
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

        float baseAbilityCd = abilityCd;

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

        //BasicAttack
        if (Input.GetButton("LeftMouse") && (basicCd <= 0))
        {
            print("hello");
            ChooseAttackDirection();
            basicCd = 1f;
        } else
        {
            basicCd -= Time.fixedDeltaTime; //dåligt system, kan bli -massa om man bara inte trycker in den knappen men får fixa det senare
            //tror nog inte ens att detta systemet kommer funka lol, TODO: fixa
        }

        //VariableAbilities
        if (Input.GetButton("RightMouse") && (abilityCd <= 0))
        {
            String ability = GetAbilityInSpot(0);
            useActiveAbiltiy(ability);
            abilityCd = baseAbilityCd;
        } else if(Input.GetButton("LShift") && (abilityCd <= 0))
        {
            String ability = GetAbilityInSpot(1);
            useActiveAbiltiy(ability);
            abilityCd = baseAbilityCd;
        } else if(Input.GetButton("Space") && (abilityCd <= 0))
        {
            String ability = GetAbilityInSpot(2);
            useActiveAbiltiy(ability);
            abilityCd = baseAbilityCd;
        } else
        {
            abilityCd -= Time.fixedDeltaTime;
        }
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

    void ChooseAttackDirection()
    {
        Vector3Int cellToAttack = origPos;

        string orientation = character.getOrientationAsString();
        switch (orientation)
        {
            case "north":
                cellToAttack.x += 1;
                break;
            case "south":
                cellToAttack.x -= 1;
                break;
            case "west":
                cellToAttack.y += 1;
                break;
            case "east":
                cellToAttack.y -= 1;
                break;
        }

        PerformAttack(cellToAttack);

    }

    private String GetAbilityInSpot(int spot)
    {
        
        string ability = character.GetAndDequeueAbility(spot);
        if (ability == null) // detta är bara temporärt
        {
            ability = "";
        }
        return ability;
    }

    void useActiveAbiltiy(String abilityName)
    {
        //cellToAttack =  //hämta detta på current mouse/cursor pos som ger en node/cellPos

        switch (abilityName)
        {
            case "C4":
                //cellToAttack.x + 1;
                //cellToAttack.x - 1;
                //cellToAttack.y + 1;
                //cellToAttack.y - 1;
                //cellToAttack;
                break;
            case "Shoot Laser":
                //cellToAttack;
                break;
            case "Forcefield":
                //become invulnerable for 2 seconds
                break;
            case "Shove":
                //push enemies that are in the _ spots in front of you
                break;
        }
    }

    private void PerformAttack(Vector3Int cellToAttack)
    {
        throw new NotImplementedException();
    }
}
