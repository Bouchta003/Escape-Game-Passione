using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmpaireOutro : MonoBehaviour
{
    private RectTransform rectTransform;

    // Référence au script TextAmpaire
    public TextOutro textOutro;

    // Tailles pour les deux états
    public Vector2 smallSize = new Vector2(100, 100); // Taille "petit"
    public Vector2 largeSize = new Vector2(120, 120); // Taille "grand"

    // Liste des indices des phrases qui déclenchent une taille "grande"
    private HashSet<int> largeSizeMessages = new HashSet<int> { 1, 3, 5 };

    void Start()
    {
        // Récupère le RectTransform de l'image
        rectTransform = GetComponent<RectTransform>();

        // Définit la taille initiale (petite)
        rectTransform.sizeDelta = smallSize;
    }

    void Update()
    {
        // Vérifie si le script TextAmpaire est défini
        if (textOutro != null)
        {
            // Obtenez l'indice actuel du message affiché depuis TextAmpaire
            int currentMessageIndex = textOutro.GetCurrentIndex();

            // Ajuste la taille en fonction de l'indice du message
            ToggleSize(currentMessageIndex);
        }
    }

    void ToggleSize(int messageIndex)
    {
        // Vérifie si l'indice du message est dans la liste des "grandes tailles"
        if (largeSizeMessages.Contains(messageIndex))
        {
            rectTransform.sizeDelta = largeSize; // Passe à grand
        }
        else
        {
            rectTransform.sizeDelta = smallSize; // Passe à petit
        }
    }
}
