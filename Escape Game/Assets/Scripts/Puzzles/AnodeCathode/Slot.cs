using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Slot : MonoBehaviour
{
    [Header("UI Elements")]
    [Tooltip("Type of slot (e.g., Anode or Cathode).")]
    public string slotType;

    [Tooltip("UI Image to display the material icon.")]
    public Image materialImage;

    [Tooltip("Reference to the assigned material.")]
    public MaterialData AssignedMaterial { get; private set; }

    [Tooltip("Reference to the GameObject of the '+' character.")]
    public GameObject plusButtonText;

    [Tooltip("Reference to the TMP_Text element to display the material's name.")]
    public TMP_Text materialText;

    [Tooltip("Reference to the UI Manager.")]
    public UI_Manager uiManager;

    [Header("Sound Effects")]
    [Tooltip("Source to play sounds from")]
    [SerializeField] AudioSource audioSource;

    [Tooltip("Sound for picking an object of the wrong type")]
    [SerializeField] AudioClip badObjectSound;

    [Tooltip("Sound for picking an object of the right type")]
    [SerializeField] AudioClip goodObjectSound;

    [Tooltip("Sound for putting the object back to the inventory")]
    [SerializeField] AudioClip clearSound;


    public void AssignMaterial(MaterialData material)
    {
        if (material != null && material.materialType == slotType)
        {
            // Remove Material from inventory
            if (Player_Inventory.inventory.Contains($"{material.materialName}_Mat"))
                Player_Inventory.inventory.Remove($"{material.materialName}_Mat");

            audioSource.PlayOneShot(goodObjectSound);
            AssignedMaterial = material;
            materialImage.sprite = material.icon; // Display the material icon
            plusButtonText.SetActive(false); // Hide '+'
            materialImage.enabled = true; // Show the image
            materialText.text = material.materialName; // Display the material name
            Debug.Log($"{material.materialName} assigned to {slotType} slot.");
        }
        else
        {
            audioSource.PlayOneShot(badObjectSound);
            Debug.Log($"Cannot assign material {material?.materialName ?? "None"} to {slotType} slot!");
        }
    }

    public void ClearSlot()
    {
        if (AssignedMaterial != null)
        {
            // Add the material back to the inventory only if it was assigned
            if (!Player_Inventory.inventory.Contains($"{AssignedMaterial.materialName}_Mat"))
            {
                Player_Inventory.inventory.Add($"{AssignedMaterial.materialName}_Mat");
            }

            // Clear the assigned material
            audioSource.PlayOneShot(clearSound);
            AssignedMaterial = null;
            materialImage.sprite = null;
            plusButtonText.SetActive(true); // Show '+'
            materialImage.enabled = false; // Hide the image
            materialText.text = ""; // Clear the material name

            Debug.Log("Slot cleared and material returned to inventory.");
        }
        else
        {
            Debug.LogWarning("ClearSlot called, but no material was assigned!");
        }
    }

    public void OnAddButtonClicked()
    {
        if (AssignedMaterial == null)
        {
            // Notify the UI Manager to open the inventory for this slot
            uiManager.OpenInventoryForSlot(this);
        }
        else ClearSlot();

    }
}