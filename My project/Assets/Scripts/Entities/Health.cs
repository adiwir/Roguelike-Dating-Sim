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

    [SerializeField] private Material shieldMaterial;
    private Material originalMaterial;

    private SpriteRenderer spriteRenderer;

    private PlayerController playerController;
    
    //Simons tillägg under
    private static Health _instance;
    public static Health Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    private void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        originalMaterial = spriteRenderer.material;

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
            playerController.SetDead(true);
        }

        StartCoroutine(BecomeTemporarilyInvincible(invincibilityDurationSeconds, false));
    }

    private void ScaleModelTo(Vector3 scale)
    {
        transform.localScale = scale;
    }

    private IEnumerator BecomeTemporarilyInvincible(float duration, bool usedShield) //detta är skitfult
    {
        isInvincible = true;
        for (float i = 0; i < duration; i += invincibilityDeltaTime)
        {
            if (!usedShield)
            {
                startFlashingOnInjury();
            }
            else
            {
                startShielding();
            }
            yield return new WaitForSeconds(invincibilityDeltaTime);
        }
        ScaleModelTo(Vector3.one);
        spriteRenderer.material = originalMaterial;
        isInvincible = false;
    }

    private void startShielding()
    {
        if (spriteRenderer.material == shieldMaterial)
        {
            spriteRenderer.material = originalMaterial;
        }
        else
        {
            spriteRenderer.material = shieldMaterial;
        }
    }

    private void startFlashingOnInjury()
    {
        if (transform.localScale == Vector3.one)
        {
            ScaleModelTo(Vector3.zero);
        }
        else
        {
            ScaleModelTo(Vector3.one);
        }
    }

    public void setInvincible(bool invincibilityValue)
    {
        isInvincible = invincibilityValue;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            decreaseHealthPoints(1);
        }
    }

    public void BecomeInvincible(float seconds)
    {
        StartCoroutine(BecomeTemporarilyInvincible(seconds, true));
    }

}