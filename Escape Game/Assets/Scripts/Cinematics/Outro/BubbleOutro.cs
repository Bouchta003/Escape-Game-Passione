using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BubbleOutro : MonoBehaviour
{
    private RectTransform rectTransform; // R�f�rence au RectTransform de l'image

    // R�f�rence � un gestionnaire (par exemple, TextManager) pour l'�tat actuel
    public TextOutro textOutro;

    // Liste des indices qui d�clenchent l'inversion
    public List<int> flipIndices = new List<int> { 2, 4 };

    // Valeur d'�chelle initiale
    private Vector3 originalScale;

    void Start()
    {
        // R�cup�re le RectTransform de l'objet
        rectTransform = GetComponent<RectTransform>();

        // Sauvegarde l'�chelle d'origine
        originalScale = rectTransform.localScale;
    }

    void Update()
    {
        // V�rifie si le TextManager est d�fini
        if (textOutro != null)
        {
            // Obtenez l'indice actuel � partir du gestionnaire
            int currentIndex = textOutro.GetCurrentIndex();

            // V�rifie si l'indice correspond � un num�ro dans la liste
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
        // Inverse l'�chelle sur l'axe X
        Vector3 scale = rectTransform.localScale;
        scale.x = Mathf.Abs(scale.x) * -1; // Garantit l'inversion uniquement si n�cessaire
        rectTransform.localScale = scale;
    }

    private void ResetImage()
    {
        // R�initialise l'�chelle � son �tat d'origine
        rectTransform.localScale = originalScale;
    }
}
