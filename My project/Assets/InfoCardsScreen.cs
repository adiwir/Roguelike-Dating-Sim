using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class InfoCardsScreen : MonoBehaviour
{
    // Start is called before the first frame update
    private bool isActive = false;
    public void ShowInfoScreen()
    {
        if (!isActive)
        {
            gameObject.SetActive(true);
            isActive = true;
            PauseGame();
        }
        else
        {
            gameObject.SetActive(false);
            isActive = false;
            ResumeGame();
        }
    }

    void PauseGame()
    {
        Time.timeScale = 0;
        Health.Instance.setInvincible(true);
    }

    void ResumeGame()
    {
        Time.timeScale = 1;
        Health.Instance.setInvincible(false);
    }

}
