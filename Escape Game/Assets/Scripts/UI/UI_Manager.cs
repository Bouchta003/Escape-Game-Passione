using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Manager : MonoBehaviour
{
    // Inventory Canvas
    [SerializeField] GameObject inventoryMenu;
    [SerializeField] GameObject inventorySlots; // Parent object containing the slots
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject itemPrefab;
    [SerializeField] GameObject inspectPanel;
    
    [SerializeField] Slot anodeSlot; // Reference to the Anode Slot UI
    [SerializeField] Slot cathodeSlot; // Reference to the Cathode Slot UI

    Slot activeSlot; // The slot that requested the inventory

    void Start()
    {
        MaterialDatabase.InitializeDatabase(); // Automatically load materials
        pauseMenu.SetActive(false);
        inventoryMenu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) ToggleInventoryMenu();
        if (Input.GetKeyDown(KeyCode.Escape)) TogglePauseMenu();
    }

    public void TogglePauseMenu()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);

        if (pauseMenu.activeSelf)
        {
            EnableCursor();
            Time.timeScale = 0;
        }
        else
        {
            DisableCursor();
            Time.timeScale = 1;
        }
    }


    public void ToggleInventoryMenu()
    {
        inventoryMenu.SetActive(!inventoryMenu.activeSelf);

        if (inventoryMenu.activeSelf)
        {
            PopulateInventorySlots();
            EnableCursor();
        }
        else DisableCursor();
    }

    public void OpenInventoryForSlot(Slot slot)
    {
        activeSlot = slot; // Set the active slot
        inventoryMenu.SetActive(true);
        PopulateInventorySlots();
        EnableCursor();
    }

    private void PopulateInventorySlots()
    {
        int slotIndex = 0;

        // Clear existing buttons
        foreach (Transform slot in inventorySlots.transform)
        {
            foreach (Transform child in slot.transform) Destroy(child.gameObject);
        }

        // Loop through the inventory list
        foreach (string item in Player_Inventory.inventory)
        {

            // Get the corresponding slot
            if (slotIndex < inventorySlots.transform.childCount)
            {
                Transform slot = inventorySlots.transform.GetChild(slotIndex);

                // Instantiate the prefab in the slot
                GameObject instantiatedItem = Instantiate(itemPrefab, slot.transform);
                instantiatedItem.name = item;
                instantiatedItem.tag = "Stored";
                // Set the text of the TextMeshPro component in the prefab
                TextMeshProUGUI textComponent = instantiatedItem.GetComponentInChildren<TextMeshProUGUI>();
                if (textComponent != null)
                {
                    textComponent.text = item; // Set the item's name as the text
                }

                Image itemImage = instantiatedItem.GetComponentInChildren<Image>();
                Sprite itemSprite = null; // Replace with logic to get the item's sprite
                if (itemImage != null)
                {
                    itemSprite = itemImage.sprite; // Use the prefab's sprite
                }

                InventoryItemHover hoverScript = instantiatedItem.AddComponent<InventoryItemHover>();
                hoverScript.inspectPanel = inspectPanel; // Reference to your inspect panel
                hoverScript.inspectImage = inspectPanel.GetComponent<Image>();
                hoverScript.itemSprite = itemSprite; // Pass the item's sprite

                // Add functionality to assign the item to a slot when clicked
                Button button = instantiatedItem.GetComponentInChildren<Button>();
                if (button != null)
                {
                    button.onClick.AddListener(() => OnInventoryItemClicked(item));
                }

                slotIndex++;
            }
            else
            {
                Debug.LogWarning("Not enough slots for all items in the inventory!");
                break;
            }
        }
    }

    private void OnInventoryItemClicked(string itemName)
    {
        if (activeSlot == null)
        {
            Debug.LogError("No active slot to assign the item to!");
            return;
        }

        // Get the MaterialData for the clicked item
        MaterialData material = MaterialDatabase.GetMaterialData(itemName.Split('_')[0]);

        // Assign the material to the active slot
        activeSlot.AssignMaterial(material);

        // Close the inventory
        inventoryMenu.SetActive(false);

        // Clear the active slot reference
        activeSlot = null;
    }

    /// <summary>
    /// Enables the cursor for user interaction.
    /// </summary>
    private void EnableCursor()
    {
        Cursor.lockState = CursorLockMode.None; // Unlock cursor
        Cursor.visible = true; // Make cursor visible
    }

    /// <summary>
    /// Disables the cursor to restore game control.
    /// </summary>
    private void DisableCursor()
    {
        Cursor.lockState = CursorLockMode.Locked; // Lock cursor
        Cursor.visible = false; // Hide cursor
    }
}
