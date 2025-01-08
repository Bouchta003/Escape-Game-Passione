/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotOutro : MonoBehaviour
{
    private RectTransform rectTransform;

    // R�f�rence � un autre script ou syst�me pour obtenir l'indice affich� (par exemple, un script de texte)
    public TextOutro textOutro; // Script pour g�rer le texte (ou autre logique)

    // Tailles pour les deux �tats
    public Vector2 smallSize = new Vector2(100, 100); // Taille "petit"
    public Vector2 largeSize = new Vector2(120, 120); // Taille "grand"

    // Liste des indices qui d�clenchent une taille "grande"
    private HashSet<int> largeSizeMessages = new HashSet<int> { 2, 5, 6, 7, 9 };

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
        if (textOutro != null)
        {
            // Obtenez l'indice actuel du message affich� depuis TextManager
            int currentIndex = textOutro.GetCurrentIndex();

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
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RobotOutro : MonoBehaviour
{
    // R�f�rence au script TextOutro
    public TextOutro textOutro;

    // R�f�rence au composant Image
    private Image image;

    // Liste des indices des phrases qui d�clenchent l'apparition de l'image
    private HashSet<int> visibleMessages = new HashSet<int> { 2, 5, 6, 7, 9 };

    void Start()
    {
        // R�cup�re le composant Image
        image = GetComponent<Image>();

        // Cache l'image au d�marrage
        if (image != null)
        {
            image.enabled = false;
        }
    }

    void Update()
    {
        // V�rifie si le script TextOutro est d�fini
        if (textOutro != null)
        {
            // Obtenez l'indice actuel du message affich� depuis TextOutro
            int currentIndex = textOutro.GetCurrentIndex();

            // Ajuste la visibilit� en fonction de l'indice actuel
            ToggleVisibility(currentIndex);
        }
    }

    void ToggleVisibility(int currentIndex)
    {
        // Affiche ou cache l'image sans d�sactiver l'objet complet
        if (image != null)
        {
            image.enabled = visibleMessages.Contains(currentIndex);
        }
    }
}
