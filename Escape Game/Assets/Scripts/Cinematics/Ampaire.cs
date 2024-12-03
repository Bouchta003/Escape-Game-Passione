using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ampaire : MonoBehaviour
{
    private RectTransform rectTransform;

    // Référence au script TextAmpaire
    public TextAmpaire textAmpaire;

    // Tailles pour les deux états
    public Vector2 smallSize = new Vector2(100, 100); // Taille "petit"
    public Vector2 largeSize = new Vector2(120, 120); // Taille "grand"

    // Liste des indices des phrases qui déclenchent une taille "grande"
    private HashSet<int> largeSizeMessages = new HashSet<int> { 0, 1, 2, 3, 4, 6, 7, 9, 10, 11, 12, 14 };

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
        if (textAmpaire != null)
        {
            // Obtenez l'indice actuel du message affiché depuis TextAmpaire
            int currentMessageIndex = textAmpaire.GetCurrentIndex();

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
