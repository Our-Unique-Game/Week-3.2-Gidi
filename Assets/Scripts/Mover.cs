using UnityEngine;

public class Mover : MonoBehaviour
{
	[Tooltip("Speed of movement to the right")]
	[SerializeField] private float moveSpeed = 5f;

	private Rigidbody2D rb;

	public float MoveSpeed
	{
		get => moveSpeed;
		set
		{
			moveSpeed = value;
			Debug.Log($"Mover speed updated to: {moveSpeed}");
		}
	}

	private void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		if (rb == null)
		{
			Debug.LogWarning("Rigidbody2D not found! Adding Rigidbody2D component dynamically.");
			rb = gameObject.AddComponent<Rigidbody2D>();
			rb.gravityScale = 0; // Ensure no gravity for the player
		}
	}

	private void FixedUpdate()
	{
		// Move to the right at the specified speed
		rb.linearVelocity = new Vector2(moveSpeed, rb.linearVelocity.y);
	}

	/// <summary>
	/// Public method to change the movement speed.
	/// </summary>
	/// <param name="newSpeed">The new speed value.</param>
	public void SetSpeed(float newSpeed)
	{
		MoveSpeed = newSpeed;
	}
}
