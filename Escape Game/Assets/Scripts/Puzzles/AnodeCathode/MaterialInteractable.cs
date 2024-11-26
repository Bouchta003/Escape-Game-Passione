using UnityEngine;

public class MaterialInteractable : MonoBehaviour, IInteractable
{
    public MaterialData materialData; // Reference to material properties

    public void Interact()
    {
        Debug.Log($"Interacted with: {materialData.materialName}");
        // Add logic to pick up or place the material
    }
}