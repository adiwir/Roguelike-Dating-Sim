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
    }

    void FixedUpdate()
    {
        if(elapsedTime == 150)
        {
            targetPosition = playerTarget.transform.position;
            float distance = Vector3.Distance(targetPosition, transform.position);
            if(distance < 20)
            {
                StartCoroutine(Attack(targetPosition));
            }
        }
        elapsedTime += 1;
    }

    public IEnumerator Attack(Vector3 targetPosition)
    {
        transform.position = Vector3.MoveTowards(transform.position, outOfSightPosition, 10000000000000);
        dirtParticles.transform.position = Vector3.MoveTowards(dirtParticles.transform.position, targetPosition, 10000000000);
        yield return new WaitForSeconds(3);
        elapsedTime = 0;
        dirtParticles.transform.position = Vector3.MoveTowards(dirtParticles.transform.position, outOfSightPosition, 10000000000);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, 100000000000000000);

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Debug.Log("ouch");
        }
    }
}