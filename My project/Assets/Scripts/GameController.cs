using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public GameOverScreen gameOverScreen;
    public Health healthController;

    void Update()
    {
        int health = healthController.getHealth();

        if (health <= 0)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        gameOverScreen.Setup();
    }
}
