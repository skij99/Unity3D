using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Sprite[] lives;
    public Image livesImageDisplay;
    public Text scoreText;
    public GameObject titleScreen;
    public int score = 0;

    public void UpdateLives(int currentLives)
    {
        Debug.Log("Current Lives: " + currentLives);
        livesImageDisplay.sprite = lives[currentLives];
    }

    public void UpdateScore()
    {
        score += 10;
        scoreText.text = "Score: " + score;
    }

    public void ShowTitle()
    {
        titleScreen.SetActive(true);
    }

    public void HideTitle()
    {
        titleScreen.SetActive(false);
        scoreText.text = "Score: 0";
        score = 0;
    }
}
