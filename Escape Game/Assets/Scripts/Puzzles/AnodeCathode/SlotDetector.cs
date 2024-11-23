using UnityEngine;

public class SlotDetector : MonoBehaviour
{
    public string requiredTag = "Material"; // Tag for valid materials (set on material GameObjects)
    public MaterialData detectedMaterial;  // Reference to the detected material's data
    public bool isOccupied = false;        // Check if a material is already in the slot

    void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger has the required tag
        if (other.CompareTag(requiredTag) && !isOccupied)
        {
            // Get the MaterialData from the object
            MaterialInteractable materialInteractable = other.GetComponent<MaterialInteractable>();
            if (materialInteractable != null)
            {
                detectedMaterial = materialInteractable.materialData;
                isOccupied = true;

                Debug.Log($"Material {detectedMaterial.materialName} placed on the slot.");
                // Optionally, disable movement or interaction for the material
                other.GetComponent<Rigidbody>().isKinematic = true;
                other.transform.position = transform.position; // Snap material to the slot
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Reset if the material is removed
        if (other.CompareTag(requiredTag) && isOccupied)
        {
            detectedMaterial = null;
            isOccupied = false;

            Debug.Log("Material removed from the slot.");
        }
    }
}