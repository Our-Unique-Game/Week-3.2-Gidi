using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f; // Forward speed
    [SerializeField] private float explosionRadius = 2f; // Radius of explosion
    [SerializeField] private int lives = 3; // Player lives
    [SerializeField] private float winPositionX = 1000f; // X position to trigger "You Win"
    [SerializeField] private Sprite bombSprite; // Sprite to display during explosion
    [SerializeField] private float explosionDuration = 0.5f; // Time to display the bomb sprite
    [SerializeField] private Sprite normalSprite; // Normal player sprite

    private Rigidbody2D rb;
    private UIManager uiManager;
    private bool isGameOver = false; // Flag to check if the game is over

    private readonly float[] lanePositions = { -3.1f, 0f, 3.2f }; // Predefined lane positions
    private int currentLane = 1; // Default starting lane is the middle lane

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        uiManager = FindObjectOfType<UIManager>();

        if (uiManager != null)
        {
            uiManager.SetLives(lives);
        }

        SetPlayerPositionToLane();
    }

    private void Update()
    {
        if (isGameOver) return; // Prevent further actions if the game is over

        // Automatic movement to the right
        rb.linearVelocity = new Vector2(speed, rb.linearVelocity.y);

        // Check for win condition
        if (transform.position.x >= winPositionX)
        {
            TriggerWin();
            return;
        }

        // Lane-based movement
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            MoveLane(1); // Move up
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            MoveLane(-1); // Move down
        }

        // Explode with Space Bar
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Explode();
        }
    }

    private void MoveLane(int direction)
    {
        int targetLane = currentLane + direction;

        // Ensure the lane remains within bounds
        if (targetLane >= 0 && targetLane < lanePositions.Length)
        {
            currentLane = targetLane;
            SetPlayerPositionToLane();
            Debug.Log($"Moved to lane {currentLane}");
        }
        else
        {
            Debug.Log("Attempted to move out of bounds");
        }
    }

    private void SetPlayerPositionToLane()
    {
        Vector3 newPosition = new Vector3(transform.position.x, lanePositions[currentLane], transform.position.z);
        transform.position = newPosition;
    }

    private void Explode()
    {
        StartCoroutine(ChangeToBombSprite());

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        foreach (var hit in hits)
        {
            if (hit.CompareTag("Fruit"))
            {
                Destroy(hit.gameObject);
                if (uiManager != null)
                {
                    uiManager.AddScore(1);
                }
            }
            else if (hit.CompareTag("Bomb"))
            {
                Destroy(hit.gameObject);
                lives--;

                if (uiManager != null)
                {
                    uiManager.SetLives(lives);
                }

                Debug.Log($"Bomb exploded! Lives remaining: {lives}");

                if (lives <= 0)
                {
                    TriggerGameOver();
                }
            }
            else if (hit.CompareTag("Obstacle"))
            {
                Destroy(hit.gameObject); // Destroy cactus without affecting lives
                Debug.Log("Cactus exploded!");
            }
        }

        Debug.Log("Exploded!");
    }

    private System.Collections.IEnumerator ChangeToBombSprite()
    {
        spriteRenderer.sprite = bombSprite;
        yield return new WaitForSeconds(explosionDuration);
        spriteRenderer.sprite = normalSprite;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bomb"))
        {
            uiManager.SetLives(--lives);

            Debug.Log($"Player hit a bomb! Lives remaining: {lives}");

            if (lives <= 0)
            {
                TriggerGameOver();
            }

            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            uiManager.SetLives(--lives);

            Debug.Log($"Player hit a cactus! Lives remaining: {lives}");

            if (lives <= 0)
            {
                TriggerGameOver();
            }

            Destroy(collision.gameObject); // Destroy the cactus after collision
        }
    }

    private void TriggerGameOver()
    {
        isGameOver = true;

        // Stop player movement
        rb.linearVelocity = Vector2.zero;
        rb.isKinematic = true; // Prevent further physics interactions

        // Notify UIManager to display "Game Over" text
        if (uiManager != null)
        {
            uiManager.ShowGameOverText();
        }

        Debug.Log("Game Over!");
    }

    private void TriggerWin()
    {
        isGameOver = true;

        // Stop player movement
        rb.linearVelocity = Vector2.zero;
        rb.isKinematic = true;

        // Notify UIManager to display "You Win" text
        if (uiManager != null)
        {
            uiManager.ShowWinText();
        }

        Debug.Log("You Win!");
    }

    // Debugging the explosion radius in the editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
