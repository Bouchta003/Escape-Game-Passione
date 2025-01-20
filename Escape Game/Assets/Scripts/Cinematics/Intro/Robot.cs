/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour
{
    private RectTransform rectTransform;

    // Référence à un autre script ou système pour obtenir l'indice affiché (par exemple, un script de texte)
    public TextAmpaire textAmpaire; // Script pour gérer le texte (ou autre logique)

    // Tailles pour les deux états
    public Vector2 smallSize = new Vector2(100, 100); // Taille "petit"
    public Vector2 largeSize = new Vector2(120, 120); // Taille "grand"

    // Liste des indices qui déclenchent une taille "grande"
    private HashSet<int> largeSizeMessages = new HashSet<int> { 3, 4, 5, 9, 10, 11, 13, 14, 18, 19, 22 };

    void Start()
    {
        // Récupère le RectTransform de l'image
        rectTransform = GetComponent<RectTransform>();

        // Définit la taille initiale
        rectTransform.sizeDelta = smallSize;
    }

    void Update()
    {
        // Vérifie si le TextManager est défini
        if (textAmpaire != null)
        {
            // Obtenez l'indice actuel du message affiché depuis TextManager
            int currentIndex = textAmpaire.GetCurrentIndex();

            // Ajuste la taille en fonction de l'indice actuel
            ToggleSize(currentIndex);
        }
    }

    void ToggleSize(int currentIndex)
    {
        // Vérifie si l'indice actuel est dans la liste des "grandes tailles"
        if (largeSizeMessages.Contains(currentIndex))
        {
            rectTransform.sizeDelta = largeSize; // Passe à grand
        }
        else
        {
            rectTransform.sizeDelta = smallSize; // Passe à petit
        }
    }
}
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Robot : MonoBehaviour
{
    // Référence au script qui gère l'affichage du texte
    public TextAmpaire textAmpaire;

    // Référence au composant Image (ou SpriteRenderer pour des objets 2D)
    private Image image;

    // Liste des indices qui déclenchent l'apparition de l'image
    private HashSet<int> visibleMessages = new HashSet<int> { 3, 4, 5, 9, 10, 11, 13, 14, 18, 19, 22 };

    void Start()
    {
        // Récupère le composant Image
        image = GetComponent<Image>();

        // Cache l'image au démarrage
        if (image != null)
        {
            image.enabled = false;
        }
    }

    void Update()
    {
        // Vérifie si TextAmpaire est défini
        if (textAmpaire != null)
        {
            // Obtenez l'indice actuel du message affiché depuis TextAmpaire
            int currentIndex = textAmpaire.GetCurrentIndex();

            // Ajuste la visibilité en fonction de l'indice actuel
            ToggleVisibility(currentIndex);
        }
    }

    void ToggleVisibility(int currentIndex)
    {
        // Affiche ou cache l'image sans désactiver l'objet complet
        if (image != null)
        {
            image.enabled = visibleMessages.Contains(currentIndex);
        }
    }
}
