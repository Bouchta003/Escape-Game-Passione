using UnityEngine;
using TMPro;

public class DocumentInteractable : MonoBehaviour, IInteractable
{
    [TextArea] public string documentText; // Text of the document
    [SerializeField] TMP_Text reportContent;
    GameObject documentUI;
    [SerializeField] AudioClip pickUpSound;

    private void Start()
    {
        documentUI = gameObject;
        reportContent.text = documentText;
    }

    public void Interact()
    {
        // Display the document text in the UI
        // documentUI.SetActive(true);
    }

    /// <summary>
    /// Gets sound to play when picking this item up.
    /// </summary>
    /// <returns>AudioClip of the sound to play.</returns>
    public AudioClip GetPickUpSound() => pickUpSound;

    public void CloseDocument()
    {
        // documentUI.SetActive(false);
    }
}