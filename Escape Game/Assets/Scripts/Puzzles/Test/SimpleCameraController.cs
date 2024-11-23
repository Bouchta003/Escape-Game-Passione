using UnityEngine;

public class SimpleCameraController : MonoBehaviour
{
    public Transform player; // Reference to the player's Transform
    public Vector3 offset;   // Offset position relative to the player
    public float sensitivity = 5f; // Mouse sensitivity

    private float pitch = 0f; // Vertical rotation
    private float yaw = 0f;   // Horizontal rotation

    void Start()
    {
        // Initialize the offset
        offset = transform.position - player.position;
    }

    void LateUpdate()
    {
        // Get mouse input
        yaw += Input.GetAxis("Mouse X") * sensitivity;
        pitch -= Input.GetAxis("Mouse Y") * sensitivity;
        pitch = Mathf.Clamp(pitch, -60f, 60f); // Limit vertical rotation

        // Rotate the camera around the player
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
        Vector3 targetPosition = player.position + rotation * offset;
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 10f);

        // Make the camera look at the player
        transform.LookAt(player.position);
    }
}
