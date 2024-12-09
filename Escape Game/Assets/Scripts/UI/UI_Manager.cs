using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Import TextMeshPro namespace

public class UI_Manager : MonoBehaviour
{
    // Inventory Canvas
    [SerializeField] GameObject inventoryMenu;
    [SerializeField] GameObject inventorySlots; // Parent object containing the slots
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject itemPrefab;

    void Start()
    {
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
        if (pauseMenu.activeSelf == false)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;

        pauseMenu.SetActive(!pauseMenu.activeSelf);
    }

    public void ToggleInventoryMenu()
    {
        inventoryMenu.SetActive(!inventoryMenu.activeSelf);

        if (inventoryMenu.activeSelf)
        {
            PopulateInventorySlots();
        }
    }

    private void PopulateInventorySlots()
    {
        int slotIndex = 0;

        // Loop through the inventory list
        foreach (string item in Player_Inventory.inventory)
        {
            // Get the corresponding slot
            if (slotIndex < inventorySlots.transform.childCount)
            {
                Transform slot = inventorySlots.transform.GetChild(slotIndex);

                // Instantiate the prefab in the slot
                GameObject instantiatedItem = Instantiate(itemPrefab, slot.position, Quaternion.identity);
                instantiatedItem.transform.SetParent(slot);

                // Optionally reset local position/rotation/scale
                instantiatedItem.transform.localPosition = Vector3.zero;
                instantiatedItem.transform.localRotation = Quaternion.identity;
                instantiatedItem.transform.localScale = Vector3.one;

                // Assign the item's name or other details to the prefab
                instantiatedItem.name = item;

                // Set the text of the TextMeshPro component in the prefab
                TextMeshProUGUI textComponent = instantiatedItem.GetComponentInChildren<TextMeshProUGUI>();
                if (textComponent != null)
                {
                    textComponent.text = item; // Set the item's name as the text
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
}
