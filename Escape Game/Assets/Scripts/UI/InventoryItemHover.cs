using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItemHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject inspectPanel; // Drag your inspect panel here in the inspector
    public Image inspectImage; // The Image component on the inspect panel
    public Sprite itemSprite; // The sprite of the item

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (inspectPanel != null && inspectImage != null)
        {
            inspectPanel.SetActive(true);
            inspectImage.sprite = itemSprite; // Set the sprite to display
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (inspectPanel != null)
        {
            inspectPanel.SetActive(false);
        }
    }
}