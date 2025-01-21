using UnityEngine;

public class StorableDetector : MonoBehaviour
{
    [Header("Prefab Settings")]
    [Tooltip("Prefab to instantiate above detected objects.")]
    [SerializeField] private GameObject prefabToInstantiate;

    [Tooltip("Height offset for the instantiated prefab.")]
    [SerializeField] private float prefabHeight = 2f;

    private GameObject currentPrefabInstance; // Tracks the currently instantiated prefab
    private GameObject currentStorableObject; // Tracks the current "Storable" object
    private IInteractable currentInteractable; // Tracks the current interactable object
    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object is storable or interactable
        bool isStorable = other.CompareTag("Storable");
        bool isInteractable = other.TryGetComponent(out currentInteractable);

        if (isStorable || isInteractable)
        {
            // Destroy the existing prefab if one is already instantiated
            if (currentPrefabInstance != null)
            {
                Destroy(currentPrefabInstance);
            }

            // Calculate the spawn position for the prefab
            Renderer objectRenderer = other.GetComponent<Renderer>();
            float objectHeight = objectRenderer != null ? objectRenderer.bounds.size.y : 0f;
            Vector3 spawnPosition = other.transform.position + Vector3.up * (objectHeight / 2 + prefabHeight);

            // Instantiate the prefab and track it
            currentPrefabInstance = Instantiate(prefabToInstantiate, spawnPosition, Quaternion.identity);

            // Track the storable object if applicable
            if (isStorable)
            {
                currentStorableObject = other.gameObject;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Clear references when the object exits the trigger
        currentInteractable = null;
        currentStorableObject = null;

        if (currentPrefabInstance != null)
        {
            Destroy(currentPrefabInstance);
            currentPrefabInstance = null;
        }
    }

    private void Update()
    {
        // Handle interaction logic when the E key is pressed
        if (Input.GetKeyDown(KeyCode.I))
        {
            // Add the storable object to the player's inventory and destroy it
            if (currentStorableObject != null)
            {
                AudioClip pickUpClip = currentInteractable.GetPickUpSound();
                if (pickUpClip != null) audioSource.PlayOneShot(pickUpClip);
                Player_Inventory.inventory.Add(currentStorableObject.name);
                Destroy(currentStorableObject);
                currentStorableObject = null;
            }

            // Trigger interaction logic for interactable objects
            currentInteractable?.Interact();

            // Destroy the prefab if it exists
            if (currentPrefabInstance != null)
            {
                Destroy(currentPrefabInstance);
                currentPrefabInstance = null;
            }
        }
    }
}
