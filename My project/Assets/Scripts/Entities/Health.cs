using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int health;
    public int numOfHearts;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    [SerializeField] private bool isInvincible = false;
    [SerializeField] private float invincibilityDurationSeconds;
    [SerializeField] private float invincibilityDeltaTime;

    private PlayerController playerController;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        health = 4;
        numOfHearts = 4;
    }

    void Update()
    {
        if(health > numOfHearts)
        {
            health = numOfHearts;
        }

        for(int i = 0; i < hearts.Length; i++)
        {
            if(i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
            if(i < numOfHearts)
            {
                hearts[i].enabled = true;
            }  
            else
            {
                hearts[i].enabled = false;
            }

        }
    }

    public int getHealth()
    {
        return health; 
    }


    public void decreaseHealthPoints(int hpReduction)
    {
        if (isInvincible) return;

        health -= hpReduction;

        if(health <= 0)
        {
            playerController.setDead(true);
        }

        StartCoroutine(BecomeTemporarilyInvincible());
    }

    private void ScaleModelTo(Vector3 scale)
    {
        transform.localScale = scale;
    }

    private IEnumerator BecomeTemporarilyInvincible()
    {
        isInvincible = true;
        for (float i = 0; i < invincibilityDurationSeconds; i += invincibilityDeltaTime)
        {
            // Alternate between 0 and 1 scale to simulate flashing
            if (transform.localScale == Vector3.one) 
            {
                ScaleModelTo(Vector3.zero);
            }
            else
            {
                ScaleModelTo(Vector3.one);
            }
            yield return new WaitForSeconds(invincibilityDeltaTime);
        }
        ScaleModelTo(Vector3.one);
        isInvincible = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            decreaseHealthPoints(1);
        }
    }

}