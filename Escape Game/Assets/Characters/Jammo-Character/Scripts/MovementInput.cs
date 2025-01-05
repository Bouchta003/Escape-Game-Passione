using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class MovementInput : MonoBehaviour
{
    public float Velocity;
    public float InputX;
    public float InputZ;
    public Vector3 desiredMoveDirection;
    public float desiredRotationSpeed = 0.1f;
    public Animator anim;
    public float Speed;
    public float allowPlayerRotation = 0.1f;
    public Camera cam;
    public CharacterController controller;
    public bool isGrounded;

    [Header("Camera Settings")]
    public float mouseSensitivity = 100f;
    public float cameraDistance = 5f;
    public float cameraHeight = 2f;
    public Transform cameraTarget;

    private float verticalLookRotation;

    [Header("Animation Smoothing")]
    [Range(0, 1f)]
    public float HorizontalAnimSmoothTime = 0.2f;
    [Range(0, 1f)]
    public float VerticalAnimTime = 0.2f;
    [Range(0, 1f)]
    public float StartAnimTime = 0.3f;
    [Range(0, 1f)]
    public float StopAnimTime = 0.15f;

    public float verticalVel;
    private Vector3 moveVector;

    void Start()
    {
        anim = this.GetComponent<Animator>();
        cam = Camera.main;
        controller = this.GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        HandleCamera();
        InputMagnitude();

        isGrounded = controller.isGrounded;
        if (isGrounded)
        {
            verticalVel -= 0;
        }
        else
        {
            verticalVel -= 1;
        }
        moveVector = new Vector3(0, verticalVel * .2f * Time.deltaTime, 0);
        controller.Move(moveVector);
    }

    void HandleCamera()
    {
        // Get mouse input for rotation
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Rotate camera target (horizontal rotation)
        transform.Rotate(Vector3.up * mouseX);

        // Adjust vertical rotation (clamped)
        verticalLookRotation -= mouseY;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -30f, 60f); // Adjust these limits as needed

        // Adjust camera distance based on mouse scroll
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        cameraDistance -= scrollInput * 2f; // Adjust zoom speed as needed
        cameraDistance = Mathf.Clamp(cameraDistance, 2f, 10f); // Min and max zoom levels

        // Set camera rotation
        cam.transform.rotation = Quaternion.Euler(verticalLookRotation, transform.eulerAngles.y, 0);

        // Calculate desired camera position
        Vector3 cameraPosition = cameraTarget.position - cam.transform.forward * cameraDistance + Vector3.up * cameraHeight;

        // Smoothly move camera to the desired position
        cam.transform.position = Vector3.Lerp(cam.transform.position, cameraPosition, Time.deltaTime * 10f); // Smooth follow
    }



    void PlayerMoveAndRotation()
    {
        InputX = Input.GetAxis("Horizontal");
        InputZ = Input.GetAxis("Vertical");

        var forward = cam.transform.forward;
        var right = cam.transform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        desiredMoveDirection = forward * InputZ + right * InputX;

        if (desiredMoveDirection.magnitude > 0)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredMoveDirection), desiredRotationSpeed);
            controller.Move(desiredMoveDirection * Time.deltaTime * Velocity);
        }
    }

    void InputMagnitude()
    {
        InputX = Input.GetAxis("Horizontal");
        InputZ = Input.GetAxis("Vertical");

        Speed = new Vector2(InputX, InputZ).sqrMagnitude;

        if (Speed > allowPlayerRotation)
        {
            anim.SetFloat("Blend", Speed, StartAnimTime, Time.deltaTime);
            PlayerMoveAndRotation();
        }
        else if (Speed < allowPlayerRotation)
        {
            anim.SetFloat("Blend", Speed, StopAnimTime, Time.deltaTime);
        }
    }
}
