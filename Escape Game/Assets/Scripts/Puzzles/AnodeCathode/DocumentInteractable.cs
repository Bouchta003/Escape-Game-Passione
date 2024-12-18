using UnityEngine;
using TMPro;

public class DocumentInteractable : MonoBehaviour, IInteractable
{
    [TextArea] public string documentText; // Text of the document
    [SerializeField] TMP_Text reportContent;
    GameObject documentUI;

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

    public void CloseDocument()
    {
        // documentUI.SetActive(false);
    }
}