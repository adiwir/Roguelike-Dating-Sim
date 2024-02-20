using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Boss : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private Tilemap col;

    private int elapsedTime = 0;
    private Vector3 targetPosition;
    private Vector3 currentPosition;
    private Vector3 outOfSightPosition = new Vector3(100f, 100f);
    [SerializeField] private bool inAttack = false;

    public GameObject dirtParticles;
    public GameObject playerTarget;

    public SpriteRenderer spriteRenderer;
    public Sprite facingUpSprite;
    public Sprite[] spriteArray;

    bool flip;

    void Start()
    {
        dirtParticles = GameObject.FindWithTag("Dirt");
        playerTarget = GameObject.FindWithTag("Player");
        targetPosition = playerTarget.transform.position;
    }

    private void Update()
    {
        Vector3 scale = transform.localScale;

        if(playerTarget.transform.position.y > transform.position.y)
        {
            ChangeSprite(1);
            flip = true;
        }
        else
        {
            ChangeSprite(0);
            flip = false;
        }

        if (playerTarget.transform.position.x > transform.position.x)
        {
            scale.x = Mathf.Abs(scale.x) * -1 * (flip ? -1 : 1);
        }
        else
        {
            scale.x = Mathf.Abs(scale.x) * (flip ? -1 : 1);
        }

        transform.localScale = scale;
    }

    void FixedUpdate()
    {
        targetPosition = playerTarget.transform.position;
        currentPosition = targetPosition;
        if (elapsedTime > 150)
        {
            float distance = Vector3.Distance(targetPosition, transform.position);
            if(distance < 50 && !inAttack)
            {
                inAttack = true;
                StartCoroutine(Burrow(targetPosition));
            } 
        }
        elapsedTime += 1;
    }

    public IEnumerator Burrow(Vector3 playerPos)
    {
        transform.position = Vector3.MoveTowards(transform.position, outOfSightPosition, 10000000000000);
        dirtParticles.transform.position = Vector3.MoveTowards(dirtParticles.transform.position, playerPos, 10000000000);
        yield return new WaitForSeconds(3);
        dirtParticles.transform.position = Vector3.MoveTowards(dirtParticles.transform.position, outOfSightPosition, 10000000000);
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(playerPos.x, playerPos.y, 5), 100000000000000000);
        yield return Charge(new Vector3(currentPosition.x, currentPosition.y, 5));
    }

    public IEnumerator Charge(Vector3 playerPos)
    {
        yield return new WaitForSeconds(0.5f);
        Vector3 pos = transform.position;
        while (pos != playerPos)
        {
            pos = Vector3.MoveTowards(transform.position, playerPos, 3 * Time.fixedDeltaTime);
            transform.position = pos;
            yield return null;
            
        }
        inAttack = false;
        elapsedTime = 0;
    }

    void ChangeSprite(int index)
    {
        spriteRenderer.sprite = spriteArray[index];
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Debug.Log("ouch");
        }
    }
}