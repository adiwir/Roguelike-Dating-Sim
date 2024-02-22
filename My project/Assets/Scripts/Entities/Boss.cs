using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEditor.PlayerSettings;

public class Boss : MonoBehaviour, IEnemy, IEnemyObserver
{
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private Tilemap col;
    private List<Vector3Int> coveredArea = new();

    private int elapsedTime = 0;
    private Vector3 targetPosition;
    private Vector3 outOfSightPosition = new Vector3(100f, 100f);
    private Vector3Int position;
    private int tilesCovered = 3;

    public GameObject dirtParticles;
    public GameObject playerTarget;

    void Start()
    {
        dirtParticles = GameObject.FindWithTag("Dirt");
        playerTarget = GameObject.FindWithTag("Player");
        targetPosition = playerTarget.transform.position;
        coveredArea = new List<Vector3Int>();
        UpdateEnemyPosition(this.position);
        UpdateCoveredArea();
    }

    void FixedUpdate()
    {
        targetPosition = playerTarget.transform.position;
        //For harder boss, change to if (elapsedTime > 300)
        if (elapsedTime == 300)
        {
            float distance = Vector3.Distance(targetPosition, transform.position);
            if(distance < 50)
            {
                Debug.Log("In range");
                StartCoroutine(Attack(targetPosition));
            }
            elapsedTime = 0;
        }
        elapsedTime += 1;
    }

    public IEnumerator Attack(Vector3 targetPosition)
    {
        transform.position = Vector3.MoveTowards(transform.position, outOfSightPosition, 10000000000000);
        position = tilemap.WorldToCell(transform.position);
        dirtParticles.transform.position = Vector3.MoveTowards(dirtParticles.transform.position, targetPosition, 10000000000);
        yield return new WaitForSeconds(3);
        
        dirtParticles.transform.position = Vector3.MoveTowards(dirtParticles.transform.position, outOfSightPosition, 10000000000);
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetPosition.x, targetPosition.y, 5), 100000000000000000);
        position = tilemap.WorldToCell(transform.position);
        //position = Vector(targetPosition.x, targetPosition.y, 5);

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Debug.Log("ouch");
        }
    }

    public Vector3Int getPos()
    {
        return this.position;
    }

    public List<Vector3Int> GetCoveredArea()
    {
         return this.coveredArea;
    }

    private void UpdateCoveredArea()//kalla på denna när bossen rör sig
    {
        coveredArea.Clear();
        coveredArea.Add(this.position);

        for (int x = position.x - 1; x <= position.x + 1; x++)
        {
            for (int y = position.y - 1; y <= position.y + 1; y++)
            {
                for (int z = position.z - 1; z <= position.z + 1; z++)
                {
                    coveredArea.Add(new Vector3Int(x, y, z));
                }
            }
        }
    }

    public void TakeDamage(int damage)
    {
        throw new NotImplementedException();
    }

    public void OnDeath()
    {
        //you get loot, game ends/ new room is unlocked
        throw new NotImplementedException();
    }

    public void UpdateEnemyPosition(Vector3 newPosition)
    {
        EntityPosStorage.Instance.AddEnemy(this);
    }
}