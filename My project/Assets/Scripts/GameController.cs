using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public InfoScreen infoScreen;
    public GameOverScreen gameOverScreen;
    public GameWonScreen gameWonScreen;
    public InfoCardsScreen infoCardsScreen;
    public Health healthController;
    public Boss bossController;

    void Update()
    {
        int health = healthController.getHealth();

        if (health <= 0)
        {
            GameOver();
        }

        if (bossController.hp <= 0) 
        {
            GameWon();
        }

        if (Input.GetKeyDown(KeyCode.Tab)) {
            infoCardsScreen.ShowInfoScreen();
        }
    }

    public void GameWon()
    {
        gameWonScreen.Setup();
    }

    public void GameOver()
    {
        gameOverScreen.Setup();
    }
}
