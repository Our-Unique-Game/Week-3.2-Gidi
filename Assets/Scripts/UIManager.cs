using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Tooltip("Text field to display lives")]
    [SerializeField] private TextMeshProUGUI livesText;

    [Tooltip("Text field to display score")]
    [SerializeField] private TextMeshProUGUI scoreText;

    private int score = 0;

    public void SetLives(int lives)
    {
        livesText.text = $"Lives: {lives}";

        if (lives <= 0)
        {
            Debug.Log("No more lives left!");
            // Implement game-over display logic here if needed
        }
    }

    public void AddScore(int points)
    {
        score += points;
        scoreText.text = $"Score: {score}";
    }
}
