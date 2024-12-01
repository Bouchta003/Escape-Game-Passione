using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ampaire : MonoBehaviour
{
    private bool isLarge = false; // État initial : petit
    private RectTransform rectTransform;

    // Tailles pour les deux états
    public Vector2 smallSize = new Vector2(100, 100); // Taille "petit"
    public Vector2 largeSize = new Vector2(150, 150); // Taille "grand"

    // Start is called before the first frame update
    void Start()
    {
        // Récupère le RectTransform de l'image
        rectTransform = GetComponent<RectTransform>();

        // Définit la taille initiale (petite)
        rectTransform.sizeDelta = smallSize;
    }

    // Update is called once per frame
    void Update()
    {
        // Vérifie si l'utilisateur clique sur l'écran
        if (Input.GetMouseButtonDown(0))
        {
            ToggleSize();
        }
    }

    void ToggleSize()
    {
        // Change la taille en fonction de l'état actuel
        if (isLarge)
        {
            rectTransform.sizeDelta = smallSize; // Passe à petit
        }
        else
        {
            rectTransform.sizeDelta = largeSize; // Passe à grand
        }

        // Bascule l'état
        isLarge = !isLarge;
    }
}
