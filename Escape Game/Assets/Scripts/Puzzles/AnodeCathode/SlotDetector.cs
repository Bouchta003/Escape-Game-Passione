using UnityEngine;

public class SlotDetector : MonoBehaviour
{
    [Header("Configuration")]
    [Tooltip("Tag used to identify valid materials.")]
    [SerializeField] string requiredTag = "Material"; // Tag for valid materials

    [Header("Slot State")]
    [Tooltip("Indicates whether the slot is currently occupied.")]
    bool isOccupied = false;
    public bool IsOccupied => isOccupied;

    [Tooltip("Reference to the detected material's data.")]
    MaterialData detectedMaterial;
    public MaterialData DetectedMaterial => detectedMaterial;

    void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger has the required tag
        if (other.CompareTag(requiredTag) && !isOccupied)
        {
            // Get the MaterialData from the object
            if (other.TryGetComponent<MaterialInteractable>(out var materialInteractable))
            {
                // Set the detected material and mark the slot as occupied
                detectedMaterial = materialInteractable.materialData;
                isOccupied = true;

                Debug.Log($"Material {detectedMaterial.materialName} placed on the slot.");

                // Disable the material's movement and snap it to the slot
                if (other.TryGetComponent<Rigidbody>(out var rb)) rb.isKinematic = true;
                other.transform.position = transform.position; // Snap material to the slot
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Reset if the material is removed
        if (other.CompareTag(requiredTag) && isOccupied)
        {
            // Clear the detected material and mark the slot as unoccupied
            detectedMaterial = null;
            isOccupied = false;

            Debug.Log("Material removed from the slot.");
        }
    }
}