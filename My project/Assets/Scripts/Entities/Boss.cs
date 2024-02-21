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
    public Animator animator;

    [SerializeField] public bool isInSecondPhase = false;
    public bool isFlipped = false;

    void Start()
    {
        dirtParticles = GameObject.FindWithTag("Dirt");
        playerTarget = GameObject.FindWithTag("Player");
        targetPosition = playerTarget.transform.position;
    }

    private void Update()
    {

        if (!(animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1))
        {
            return;
        }

        Vector3 scale = transform.localScale;

        if(playerTarget.transform.position.y > transform.position.y)
        {
            isFlipped = true;

        }
        else
        {
            isFlipped = false;
        }
        animator.SetBool("isFlipped", isFlipped);

        if (playerTarget.transform.position.x > transform.position.x)
        {
            
            scale.x = Mathf.Abs(scale.x) * -1 * (isFlipped ? -1 : 1);

        }
        else
        {
            scale.x = Mathf.Abs(scale.x) * (isFlipped ? -1 : 1);
        }
        transform.localScale = scale;

        if(isInSecondPhase)
        {
            StartCoroutine(AnimatorSetFire("isTransforming", 2.0f));
            isInSecondPhase = false;
        }
    }

    private IEnumerator AnimatorSetFire(string animation, float animationLength)
    {
        animator.SetBool(animation, true);
        yield return new WaitForSeconds(animationLength);
        animator.SetBool(animation, false);
        
    }

    void FixedUpdate()
    {
        if (!(animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1))
        {
            return;
        }

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
        animator.SetBool("isBurrowing", true);
        yield return new WaitForSeconds(1.1f);
        transform.position = Vector3.MoveTowards(transform.position, outOfSightPosition, 10000000000000);
        dirtParticles.transform.position = Vector3.MoveTowards(dirtParticles.transform.position, playerPos, 10000000000);
        animator.SetBool("isBurrowing", false);
        yield return new WaitForSeconds(2);
        animator.SetBool("isUnburrowing", true);
        dirtParticles.transform.position = Vector3.MoveTowards(dirtParticles.transform.position, outOfSightPosition, 10000000000);
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(playerPos.x, playerPos.y, 5), 100000000000000000);
        yield return new WaitForSeconds(1.1f);
        animator.SetBool("isUnburrowing", false);
        yield return Charge(new Vector3(currentPosition.x, currentPosition.y, 5));
    }

    public IEnumerator Charge(Vector3 playerPos)
    {
        animator.SetBool("isCharging", true);
        Vector3 pos = transform.position;
        while (pos != playerPos)
        {
            pos = Vector3.MoveTowards(transform.position, playerPos, 3 * Time.fixedDeltaTime);
            transform.position = pos;
            yield return null;
            
        }
        animator.SetBool("isCharging", false);
        inAttack = false;
        elapsedTime = 0;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Debug.Log("ouch");
        }
    }
}