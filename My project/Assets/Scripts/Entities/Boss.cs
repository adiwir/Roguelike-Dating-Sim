using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Boss : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private Tilemap col;

    private int elapsedTime = 0;
    private Vector3 targetPosition;
    private Vector3 outOfSightPosition = new Vector3(100f, 100f);

    public GameObject dirtParticles;
    public GameObject playerTarget;

    void Start()
    {
        dirtParticles = GameObject.FindWithTag("Dirt");
        playerTarget = GameObject.FindWithTag("Player");
        targetPosition = playerTarget.transform.position;
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
        dirtParticles.transform.position = Vector3.MoveTowards(dirtParticles.transform.position, targetPosition, 10000000000);
        yield return new WaitForSeconds(3);
        
        dirtParticles.transform.position = Vector3.MoveTowards(dirtParticles.transform.position, outOfSightPosition, 10000000000);
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetPosition.x, targetPosition.y, 5), 100000000000000000);

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Debug.Log("ouch");
        }
    }
}