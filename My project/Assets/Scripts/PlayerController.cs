
using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private Tilemap tilemap;
    [SerializeField] private Tilemap col;
    
    Character character;
    public Animator animator;

    public float moveSpeed;
    public finished2.OverlayTile standingOnTile;
    private Vector2Int tilePos;
    private Vector3Int origPos, targetCell;
    private Vector3 targetPosition;
    private float basicCd = 1f;
    private float abilityCd = 1f;
    private float baseAbilityCd;
    private float movementCd = 0.1f;
    private float fullRechargeTime = 2f; //how long the player can't do anything else while recharging
    private float currentRechargeTime;
    public int timeThresh;
    private int elapsedTime = 0;
    private bool activeAbilitySelected = false;
    private bool isDead = false;
    public bool isFlipped = false;
    public bool isRunning = false;
    public bool isRecharging = false;
    public bool allOutOfAbilities = false;
    

    public List<KeyCode> movIn;


    private void Start()
    {
        currentRechargeTime = 0;

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
        baseAbilityCd = 0;
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
        Vector3 scale = transform.localScale;

        //elapsedTime += Time.fixedDeltaTime;
        //elapsedBasicAttackTime += Time.fixedDeltaTime;

        //PlayerMovement
        CleanMovIn();

        if (currentRechargeTime > 0) { 
            currentRechargeTime -= Time.fixedDeltaTime;
        } else 
        {
            isRecharging = false;
        }
        if (elapsedTime > timeThresh)
        {
            if (movIn.Count > 0 && movIn[movIn.Count - 1] == KeyCode.W)
            {
                isFlipped = true;
                isRunning = true;
                scale.x = Mathf.Abs(scale.x);
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
                isFlipped = false;
                isRunning = true;
                scale.x = Mathf.Abs(scale.x);
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
                isFlipped = true;
                isRunning = true;
                scale.x = Mathf.Abs(scale.x) * -1;
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
                scale.x = Mathf.Abs(scale.x) * -1;
                isFlipped = false;
                isRunning = true;
                if (CanMove(targetCell))
                {
                    targetPosition = tilemap.GetCellCenterWorld(targetCell);
                    MovePlayer(targetPosition);
                    character.SetOrientation("D");
                }
            }
            else
            {
                isRunning = false;
            }
            elapsedTime = 0;
        }
        elapsedTime += 1;

        animator.SetBool("isRunning", isRunning);
        animator.SetBool("isFlipped", isFlipped);
        transform.localScale = scale;

        //BasicAttack
        if (Input.GetButton("LeftMouse") && (basicCd <= 0) && !(activeAbilitySelected))
        {
            character.UseBasicAbility(this.origPos);
            basicCd = 0.1f;
            animator.SetTrigger("Shoot");
        }
        else
        {
            basicCd -= Time.fixedDeltaTime; //d�ligt system, kan bli -massa om man bara inte trycker in den knappen men f�r fixa det senare
                                            //tror nog inte ens att detta systemet kommer funka lol, TODO: fixa
        }

        //VariableAbilities
        if (Input.GetButtonDown("RightMouse") && (abilityCd <= 0)) //frågan är om man ska använda nya ability systemet här
        {
            if(!isRecharging)
            {
                Debug.Log("activated RightMouse");
                character.ActivateAbilityInSpot(2);
            }
            
            if (character.UsedAbility()) { abilityCd = baseAbilityCd; }
        }
        else if (Input.GetButtonDown("Space") && (abilityCd <= 0))
        {
            if (!isRecharging)
            {
                Debug.Log("activated LShift");
                character.ActivateAbilityInSpot(1);
            }

            if (character.UsedAbility()) { abilityCd = baseAbilityCd; }
        }
        else if (Input.GetButtonDown("LShift") && (abilityCd <= 0))
        {
            if (!isRecharging)
            {
                Debug.Log("activated LShift");
                character.ActivateAbilityInSpot(0);
            }
            
            if (character.UsedAbility()) { abilityCd = baseAbilityCd; }
        }
    
        if(abilityCd > 0)
        {
            abilityCd -= Time.fixedDeltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("pressed esc");
            Debug.Log("before " +character.GetToggledAbility());
            character.UnToggleAbility();
            Debug.Log("after " +character.GetToggledAbility());
        }

        if (!(character.UsedAbility()))
        {
            character.DisplayAreaOfEffect();
        }

        allOutOfAbilities = character.CheckIfAllAbilitiesUsed();

        //Recharge
        if (Input.GetKeyDown(KeyCode.R) && allOutOfAbilities)
        {
            isRecharging = true;
            character.RechargeAbilities();
            currentRechargeTime = fullRechargeTime;
        }
    }

    //void ShowAreaOfEffect()

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
        if (!tilemap.HasTile(target) || col.HasTile(target) || isRecharging)
        {
            return false;
        }
        return true;
    }

    public void SetDead(bool value)
    {
        isDead = value;
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Teleport"))
        {
            Debug.Log("yeet");
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(70, 39), 1000000000);
            float newSize = Mathf.MoveTowards(Camera.main.orthographicSize, 5, 100 * Time.fixedDeltaTime);
            Camera.main.orthographicSize = newSize;
        }
    }

    private void CleanMovIn()
    {
        if (movIn.Count > 0)
        {
            if (movIn.Contains(KeyCode.W) && !Input.GetKey(KeyCode.W))
            {
                movIn.Remove(KeyCode.W);
            }

            if (movIn.Contains(KeyCode.A) && !Input.GetKey(KeyCode.A))
            {
                movIn.Remove(KeyCode.A);
            }

            if (movIn.Contains(KeyCode.D) && !Input.GetKey(KeyCode.D))
            {
                movIn.Remove(KeyCode.D);
            }

            if (movIn.Contains(KeyCode.S) && !Input.GetKey(KeyCode.S))
            {
                movIn.Remove(KeyCode.S);
            }
        }
    }
}
