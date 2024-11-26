using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float interactionRange = 3f; // Range within which the player can interact
    public LayerMask interactableLayer; // Layer for interactable objects

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) // Press E to interact
        {
            InteractWithObject();
        }
    }

    void InteractWithObject()
    {

        RaycastHit hit;

        // Cast the ray forward from the camera's position
        Vector3 rayOrigin = Camera.main.transform.position;
        Vector3 rayDirection = Camera.main.transform.forward;
        ;

        // Visualize the ray in the Scene view for debuggin
        Debug.DrawRay(rayOrigin, rayDirection * interactionRange, Color.red, 1f);

        if (Physics.Raycast(rayOrigin, rayDirection, out hit, interactionRange, interactableLayer))
        {
            // Trigger the interaction logic
            hit.collider.GetComponent<IInteractable>()?.Interact();
        }
    }
}