using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Tooltip("The player to follow")]
    [SerializeField] private Transform player;

    [Tooltip("Offset from the player")]
    [SerializeField] private Vector3 offset;

    [Tooltip("Speed of the camera's movement when following the player")]
    [SerializeField] private float smoothSpeed = 0.125f;

    private bool isMoving = true;

    private void LateUpdate()
    {
        if (isMoving && player != null)
        {
            // Smoothly follow the player
            Vector3 desiredPosition = new Vector3(player.position.x + offset.x, offset.y, offset.z);
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }

    public void StopCamera()
    {
        isMoving = false;
    }
}
