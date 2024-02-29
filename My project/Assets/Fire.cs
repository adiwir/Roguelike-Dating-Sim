using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class Fire : MonoBehaviour
{
    // Start is called before the first frame update

    // Update is called once per frame

    public GameObject spawnPoint;
    public GameObject player;
    private float movementSpeed = 1f;
    private bool isMoving = false;
    void Start()
    {
        spawnPoint = GameObject.FindWithTag("Boss");
        player = GameObject.FindWithTag("Player");
    }
    void FixedUpdate()
    {
        if(spawnPoint == null)
        {
            Destroy(this.gameObject);
            return;
        }
        Vector3 targetPos = new Vector3(spawnPoint.transform.position.x + Random.Range(-25, 25), spawnPoint.transform.position.y + Random.Range(-25, 25), 5);
        if (!isMoving)
        {
            isMoving = true;
            StartCoroutine(fireTowardsRandom(targetPos));
        }
    }

    private IEnumerator fireTowardsRandom(Vector3 target)
    {
        Vector3 pos = transform.position;
        while (pos != target)
        {
            pos = Vector3.MoveTowards(pos, target, movementSpeed * Time.fixedDeltaTime);
            transform.position = pos;
            yield return null;
        }
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player.GetComponentInParent<Health>().decreaseHealthPoints(1);
            Destroy(this.gameObject);
        }
    }
}
