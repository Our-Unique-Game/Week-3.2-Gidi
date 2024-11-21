using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f; // Forward speed
    [SerializeField] private float laneHeight = 3f; // Height difference between lanes
    [SerializeField] private float explosionRadius = 2f; // Radius of explosion
    [SerializeField] private int lives = 3; // Player lives
    [SerializeField] private int currentLane = 1; // Current lane (0 = bottom, 1 = middle, 2 = top)
    [SerializeField] private int maxLanes = 3; // Total number of lanes

    private Rigidbody2D rb;
    private UIManager uiManager;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        uiManager = FindObjectOfType<UIManager>();

        if (uiManager != null)
        {
            uiManager.SetLives(lives);
        }

        // Initialize the current lane based on the player's initial position
        float startY = transform.position.y;
        currentLane = Mathf.RoundToInt(startY / laneHeight);
        currentLane = Mathf.Clamp(currentLane, 0, maxLanes - 1); // Ensure within bounds
        SetPlayerPositionToLane();
        Debug.Log($"Player starts at lane {currentLane}");
    }

    private void Update()
    {
        // Automatic movement to the right
        rb.linearVelocity = new Vector2(speed, rb.linearVelocity.y);

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
        if (targetLane >= 0 && targetLane < maxLanes)
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
        // Align the player's Y-position with the current lane
        Vector3 newPosition = new Vector3(transform.position.x, currentLane * laneHeight, transform.position.z);
        transform.position = newPosition;
    }

    private void Explode()
    {
        // Detect objects in explosion radius
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
                    GameOver();
                }
            }
        }

        Debug.Log("Exploded!");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bomb"))
        {
            lives--; // Reduce lives for collision
            uiManager.SetLives(lives);

            Debug.Log($"Player hit a bomb! Lives remaining: {lives}");

            if (lives <= 0)
            {
                GameOver();
            }

            Destroy(collision.gameObject); // Destroy the bomb
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            lives--; // Reduce lives for obstacle collision
            uiManager.SetLives(lives);

            Debug.Log($"Player hit an obstacle! Lives remaining: {lives}");

            if (lives <= 0)
            {
                GameOver();
            }
        }
    }

    private void GameOver()
    {
        Debug.Log("Game Over!");
        // Add your game-over logic here (e.g., load a Game Over scene)
    }

    // Debugging the explosion radius in the editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
