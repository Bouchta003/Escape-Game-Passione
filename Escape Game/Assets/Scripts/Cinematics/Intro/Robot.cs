using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour
{
    private RectTransform rectTransform;

    // R�f�rence � un autre script ou syst�me pour obtenir l'indice affich� (par exemple, un script de texte)
    public TextAmpaire textAmpaire; // Script pour g�rer le texte (ou autre logique)

    // Tailles pour les deux �tats
    public Vector2 smallSize = new Vector2(100, 100); // Taille "petit"
    public Vector2 largeSize = new Vector2(120, 120); // Taille "grand"

    // Liste des indices qui d�clenchent une taille "grande"
    private HashSet<int> largeSizeMessages = new HashSet<int> { 6, 9, 14, 16 };

    void Start()
    {
        // R�cup�re le RectTransform de l'image
        rectTransform = GetComponent<RectTransform>();

        // D�finit la taille initiale
        rectTransform.sizeDelta = smallSize;
    }

    void Update()
    {
        // V�rifie si le TextManager est d�fini
        if (textAmpaire != null)
        {
            // Obtenez l'indice actuel du message affich� depuis TextManager
            int currentIndex = textAmpaire.GetCurrentIndex();

            // Ajuste la taille en fonction de l'indice actuel
            ToggleSize(currentIndex);
        }
    }

    void ToggleSize(int currentIndex)
    {
        // V�rifie si l'indice actuel est dans la liste des "grandes tailles"
        if (largeSizeMessages.Contains(currentIndex))
        {
            rectTransform.sizeDelta = largeSize; // Passe � grand
        }
        else
        {
            rectTransform.sizeDelta = smallSize; // Passe � petit
        }
    }
}