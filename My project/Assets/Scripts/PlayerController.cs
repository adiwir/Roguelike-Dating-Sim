
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private Tilemap tilemap;
    [SerializeField] private Tilemap col;
    
    Character character;
    
    public float moveSpeed;
    public finished2.OverlayTile standingOnTile;
    private Vector2Int tilePos;
    private Vector3Int origPos, targetCell;
    private Vector3 targetPosition;
    private float elapsedTime = 0f;
    private float basicCd = 1f;
    private float abilityCd = 1f;
    private float movementCd = 0.1f;
    private bool activeAbilitySelected = false;
    private bool isDead = false;

    public List<KeyCode> movIn;


    private void Start()
    {
        tilePos = new Vector2Int();
        targetCell = tilemap.WorldToCell(transform.position);
        targetPosition = tilemap.GetCellCenterWorld(targetCell);
        tilePos.x = targetCell.x;
        tilePos.y = targetCell.y;
        standingOnTile = finished2.MapManager.Instance.map[tilePos];
        //currentNode = GridManager.Instance.map[new Vector2Int(targetCell.x, targetCell.y)];
        character = GetComponent<Character>();
        character.SetPos(transform.position);

        moveSpeed = character.GetMoveSpeed();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            movIn.Add(KeyCode.W);
        }

        else if (Input.GetKeyDown(KeyCode.A))
        {
            movIn.Add(KeyCode.A);
        }

        else if (Input.GetKeyDown(KeyCode.S))
        {
            movIn.Add(KeyCode.S);
        }

        else if (Input.GetKeyDown(KeyCode.D))
        {
            movIn.Add(KeyCode.D);
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            movIn.Remove(KeyCode.W);
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            movIn.Remove(KeyCode.A);
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            movIn.Remove(KeyCode.S);
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            movIn.Remove(KeyCode.D);
        }
    }



    void FixedUpdate()
    {
        if (isDead) return;
        origPos = tilemap.WorldToCell(transform.position);
        targetCell = origPos;

        float baseAbilityCd = abilityCd;

        //elapsedTime += Time.fixedDeltaTime;
        //elapsedBasicAttackTime += Time.fixedDeltaTime;

        //PlayerMovement

        if (movIn.Count > 0 && movIn[movIn.Count - 1] == KeyCode.W)
        {
            targetCell.x += 1;
            if (CanMove(targetCell))
            {
                targetPosition = tilemap.GetCellCenterWorld(targetCell);
                MovePlayer(targetPosition);
                character.SetOrientation("W");
            }   
        }

        else if (movIn.Count > 0 && movIn[movIn.Count - 1] == KeyCode.S)
        {
            targetCell.x -= 1;
            if (CanMove(targetCell))
            {
                targetPosition = tilemap.GetCellCenterWorld(targetCell);
                MovePlayer(targetPosition);
                character.SetOrientation("S");
            }
        }

        else if (movIn.Count > 0 && movIn[movIn.Count - 1] == KeyCode.A)
        {
            targetCell.y += 1;
            if (CanMove(targetCell))
            {
                targetPosition = tilemap.GetCellCenterWorld(targetCell);
                MovePlayer(targetPosition);
                character.SetOrientation("A");
            }
        }

        else if (movIn.Count > 0 && movIn[movIn.Count - 1] == KeyCode.D)
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
        if (Input.GetButton("LeftMouse") && (basicCd <= 0) && !(activeAbilitySelected))
        {
            ChooseAttackDirection();
            basicCd = 1f;
        } else
        {
            basicCd -= Time.fixedDeltaTime; //d�ligt system, kan bli -massa om man bara inte trycker in den knappen men f�r fixa det senare
            //tror nog inte ens att detta systemet kommer funka lol, TODO: fixa
        }

        //VariableAbilities
        if (Input.GetButton("RightMouse") && (abilityCd <= 0))
        {
            activeAbilitySelected = true;
            String ability = GetAbilityInSpot(0);
            UseActiveAbiltiy(ability);
            abilityCd = baseAbilityCd;
        } else if(Input.GetButton("LShift") && (abilityCd <= 0))
        {
            activeAbilitySelected = true;
            String ability = GetAbilityInSpot(1);
            UseActiveAbiltiy(ability);
            abilityCd = baseAbilityCd;
        } else if(Input.GetButton("Space") && (abilityCd <= 0))
        {
            activeAbilitySelected = true;
            String ability = GetAbilityInSpot(2);
            UseActiveAbiltiy(ability);
            abilityCd = baseAbilityCd;
        } else
        {
            abilityCd -= Time.fixedDeltaTime;
        }
    }

    private void LateUpdate()
    {
        
    }

    void MovePlayer(Vector3 target)
    {
        if(movementCd <= 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed);
            character.SetPos(transform.position);
            targetCell = tilemap.WorldToCell(transform.position);
            targetPosition = tilemap.GetCellCenterWorld(targetCell);
            tilePos.x = targetCell.x;
            tilePos.y = targetCell.y;
            standingOnTile = finished2.MapManager.Instance.map[tilePos];
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

        string orientation = character.GetOrientationAsString();
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

        
        if (ability == null) // detta �r bara tempor�rt
        {
            ability = "";
        }
        return ability;
    }

    void UseActiveAbiltiy(String abilityName)
    {
        
        //cellToAttack =  //h�mta detta p� current mouse/cursor pos som ger en node/cellPos(använd MousePos.cs)

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

    public void setDead(bool value)
    {
        isDead = value;
    }

    private void PerformAttack(Vector3Int cellToAttack)
    {
        character.UseBasicAbility(cellToAttack);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Teleport")
        {
            Debug.Log("yeet");
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(70, 39), 1000000000);
            float newSize = Mathf.MoveTowards(Camera.main.orthographicSize, 5, 100 * Time.fixedDeltaTime);
            Camera.main.orthographicSize = newSize;
        }
    }

}
