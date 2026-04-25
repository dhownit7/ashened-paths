using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI healthText;
    private int score = 0;

    void Awake()
    {
        instance = this;
        UpdateHealth(3);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && gameOverText.gameObject.activeSelf)
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
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

    public TextMeshProUGUI gameOverText;

    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        Time.timeScale = 0f;
    }


}