using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ampaire : MonoBehaviour
{
    private RectTransform rectTransform;

    // R�f�rence au script TextAmpaire
    public TextAmpaire textAmpaire;

    // Tailles pour les deux �tats
    public Vector2 smallSize = new Vector2(100, 100); // Taille "petit"
    public Vector2 largeSize = new Vector2(120, 120); // Taille "grand"

    // Liste des indices des phrases qui d�clenchent une taille "grande"
    private HashSet<int> largeSizeMessages = new HashSet<int> { 1, 2, 3, 4, 5, 7, 8, 10, 11, 12, 13, 15 };

    void Start()
    {
        // R�cup�re le RectTransform de l'image
        rectTransform = GetComponent<RectTransform>();

        // D�finit la taille initiale (petite)
        rectTransform.sizeDelta = smallSize;
    }

    void Update()
    {
        // V�rifie si le script TextAmpaire est d�fini
        if (textAmpaire != null)
        {
            // Obtenez l'indice actuel du message affich� depuis TextAmpaire
            int currentMessageIndex = textAmpaire.GetCurrentIndex();

            // Ajuste la taille en fonction de l'indice du message
            ToggleSize(currentMessageIndex);
        }
    }

    void ToggleSize(int messageIndex)
    {
        // V�rifie si l'indice du message est dans la liste des "grandes tailles"
        if (largeSizeMessages.Contains(messageIndex))
        {
            rectTransform.sizeDelta = largeSize; // Passe � grand
        }
        else
        {
            rectTransform.sizeDelta = smallSize; // Passe � petit
        }
    }
}