
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
    //public Node currentNode;
    public finished2.OverlayTile standingOnTile;
    private Vector2Int tilePos;
    private Vector3Int origPos, targetCell;
    private Vector3 targetPosition;

    private float basicCd = 1f;
    private float abilityCd = 1f;
    private float movementCd = 0.1f;
    private bool activeAbilitySelected = false;
    private bool isDead = false;



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

    void FixedUpdate()
    {
        if (isDead) return;
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
        if (Input.GetButton("LeftMouse") && (basicCd <= 0) && !(activeAbilitySelected))
        {
            character.UseBasicAbility(origPos);
            basicCd = 1f;
        } else
        {
            basicCd -= Time.fixedDeltaTime; //d�ligt system, kan bli -massa om man bara inte trycker in den knappen men f�r fixa det senare
            //tror nog inte ens att detta systemet kommer funka lol, TODO: fixa
        }

        //VariableAbilities
        if (Input.GetButtonDown("RightMouse") && (abilityCd <= 0)) //frågan är om man ska använda nya ability systemet här
        {
            Debug.Log("activated RightMouse");
            //activeAbilitySelected = true;
            character.ActivateAbilityInSpot(0); //nyare
            //character.ToggleAbilityInSpot(0);// nytt
            if (character.UsedAbility()) { abilityCd = baseAbilityCd; }
        } else if(Input.GetButtonDown("LShift") && (abilityCd <= 0))
        {
            //activeAbilitySelected = true;
            character.ActivateAbilityInSpot(1); //nyare
            //character.ToggleAbilityInSpot(1);// nytt
            if (character.UsedAbility()) { abilityCd = baseAbilityCd; }
        } else if(Input.GetButtonDown("Space") && (abilityCd <= 0))
        {
            //activeAbilitySelected = true;
            character.ActivateAbilityInSpot(2); //nyare
            //character.ToggleAbilityInSpot(2);// nytt
            if (character.UsedAbility()) { abilityCd = baseAbilityCd; }
        } else
        {
            abilityCd -= Time.fixedDeltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            character.UnToggleAbility();
        }

        if (!(character.UsedAbility()))
        {
            character.DisplayAreaOfEffect();
        }

    }

    private void LateUpdate()//TODO: Ta bort denna? Den gör väl inget?
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

    public void setDead(bool value)
    {
        isDead = value;
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
