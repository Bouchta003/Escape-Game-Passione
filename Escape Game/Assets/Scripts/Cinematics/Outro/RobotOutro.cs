/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotOutro : MonoBehaviour
{
    private RectTransform rectTransform;

    // Référence à un autre script ou système pour obtenir l'indice affiché (par exemple, un script de texte)
    public TextOutro textOutro; // Script pour gérer le texte (ou autre logique)

    // Tailles pour les deux états
    public Vector2 smallSize = new Vector2(100, 100); // Taille "petit"
    public Vector2 largeSize = new Vector2(120, 120); // Taille "grand"

    // Liste des indices qui déclenchent une taille "grande"
    private HashSet<int> largeSizeMessages = new HashSet<int> { 2, 5, 6, 7, 9 };

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
        if (textOutro != null)
        {
            // Obtenez l'indice actuel du message affiché depuis TextManager
            int currentIndex = textOutro.GetCurrentIndex();

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

public class RobotOutro : MonoBehaviour
{
    // Référence au script TextOutro
    public TextOutro textOutro;

    // Référence au composant Image
    private Image image;

    // Liste des indices des phrases qui déclenchent l'apparition de l'image
    private HashSet<int> visibleMessages = new HashSet<int> { 2, 5, 6, 7, 9 };

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
        // Vérifie si le script TextOutro est défini
        if (textOutro != null)
        {
            // Obtenez l'indice actuel du message affiché depuis TextOutro
            int currentIndex = textOutro.GetCurrentIndex();

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
