using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BubbleOutro : MonoBehaviour
{
    private RectTransform rectTransform; // Référence au RectTransform de l'image

    // Référence à un gestionnaire (par exemple, TextManager) pour l'état actuel
    public TextOutro textOutro;

    // Liste des indices qui déclenchent l'inversion
    public List<int> flipIndices = new List<int> { 2, 4 };

    // Valeur d'échelle initiale
    private Vector3 originalScale;

    void Start()
    {
        // Récupère le RectTransform de l'objet
        rectTransform = GetComponent<RectTransform>();

        // Sauvegarde l'échelle d'origine
        originalScale = rectTransform.localScale;
    }

    void Update()
    {
        // Vérifie si le TextManager est défini
        if (textOutro != null)
        {
            // Obtenez l'indice actuel à partir du gestionnaire
            int currentIndex = textOutro.GetCurrentIndex();

            // Vérifie si l'indice correspond à un numéro dans la liste
            if (flipIndices.Contains(currentIndex))
            {
                FlipImage();
            }
            else
            {
                ResetImage();
            }
        }
    }

    private void FlipImage()
    {
        // Inverse l'échelle sur l'axe X
        Vector3 scale = rectTransform.localScale;
        scale.x = Mathf.Abs(scale.x) * -1; // Garantit l'inversion uniquement si nécessaire
        rectTransform.localScale = scale;
    }

    private void ResetImage()
    {
        // Réinitialise l'échelle à son état d'origine
        rectTransform.localScale = originalScale;
    }
}
