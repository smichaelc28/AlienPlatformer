using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public float health = 2;
    public float score = 0;
    public bool gameover = false;
    public Text healthUI;
    public Text scoreUI;
    public GameObject gameOverNotification;
    public GameObject youWinNotification;

    public void StopGame()
    {
        gameover = true;
        Invoke("LoadMainMenu", 3f);
        gameObject.SetActive(false);
    }
    
    public void AddScore()
    {
        score += 10;
        scoreUI.text = score.ToString();
    }

    public void SubtractHealth()
    {
        health -= 1;
        healthUI.text = health.ToString();
        if (health <= 0)
        {
            gameOverNotification.SetActive(true);
            StopGame();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Coin")
        {
            AddScore();
            Destroy(collision.gameObject);
        }

        if(collision.tag == "Water")
        {
            health = 0;
            healthUI.text = health.ToString();
            gameOverNotification.SetActive(true) ;
            StopGame();
        }

        if (collision.tag == "Exit")
        {
            youWinNotification.SetActive(true);
            StopGame();
            Invoke("LoadMainMenu", 3f);
        }
    }
    void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}

