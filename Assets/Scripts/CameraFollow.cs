using UnityEngine;

public class SmartCameraFollow : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public Vector3 offset; // Offset between the camera and the player
    public float smoothSpeed = 0.125f; // Speed of the smooth transition
    public float moveThreshold = 0.5f; // Minimum distance the player needs to move for the camera to update
    public float campingTime = 2.0f; // Time after which the player is considered camping

    private Vector3 lastPlayerPosition;
    private float campingTimer = 0f;

    void Start()
    {
        lastPlayerPosition = player.position;
    }

    void FixedUpdate()
    {
        float distance = Vector3.Distance(player.position, lastPlayerPosition);

        if (distance > moveThreshold)
        {
            campingTimer = 0f; // Reset camping timer
            Vector3 desiredPosition = player.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
            lastPlayerPosition = player.position;
        }
        else
        {
            campingTimer += Time.deltaTime;
            if (campingTimer > campingTime)
            {
                // If player is camping, you can implement any special behavior here
                // For now, we'll just slowly move the camera towards the player
                Vector3 desiredPosition = player.position + offset;
                Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * 0.5f);
                transform.position = smoothedPosition;
            }
        }
    }
}
