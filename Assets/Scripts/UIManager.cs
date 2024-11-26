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
		// Ensure these texts are hidden when the game starts
		if (gameOverText != null)
		{
			gameOverText.gameObject.SetActive(false);
		}

		if (winText != null)
		{
			winText.gameObject.SetActive(false);
		}
	}

	public void SetLives(int lives)
	{
		if (livesText != null)
		{
			livesText.text = $"Lives: {lives}";
		}
	}

	public void AddScore(int points)
	{
		score += points;
		if (scoreText != null)
		{
			scoreText.text = $"Score: {score}";
		}
	}

	public void ShowGameOverText()
	{
		if (gameOverText != null)
		{
			gameOverText.gameObject.SetActive(true); // Display the Game Over text
		}
	}

	public void ShowWinText()
	{
		if (winText != null)
		{
			winText.gameObject.SetActive(true); // Display the You Win text
		}
	}
}
