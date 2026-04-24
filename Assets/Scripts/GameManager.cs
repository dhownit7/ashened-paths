using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI healthText;
    private int score = 0;

    void Awake()
    {
        instance = this;
    }

    public void AddScore(int amount)
    {
        score += amount;
        scoreText.text = "Embers: " + score;
    }

    public void UpdateHealth(int health)
    {
        healthText.text = "HP: " + health;
    }

    public void GameOver()
    {
        Debug.Log("Game Over!");
    }
}