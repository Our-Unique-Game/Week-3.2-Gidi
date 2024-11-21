using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Tooltip("Text field to display lives")]
    [SerializeField] private TextMeshProUGUI livesText;

    [Tooltip("Text field to display score")]
    [SerializeField] private TextMeshProUGUI scoreText;

    [Tooltip("Text field to display Game Over")]
    [SerializeField] private TextMeshProUGUI gameOverText;

    [Tooltip("Text field to display You Win")]
    [SerializeField] private TextMeshProUGUI winText;

    private int score = 0;

    private void Start()
    {
        gameOverText.gameObject.SetActive(false); // Hide Game Over text initially
        winText.gameObject.SetActive(false); // Hide You Win text initially
    }

    public void SetLives(int lives)
    {
        livesText.text = $"Lives: {lives}";
    }

    public void AddScore(int points)
    {
        score += points;
        scoreText.text = $"Score: {score}";
    }

    public void ShowGameOverText()
    {
        gameOverText.gameObject.SetActive(true); // Display the Game Over text
    }

    public void ShowWinText()
    {
        winText.gameObject.SetActive(true); // Display the You Win text
    }
}
