using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ampaire : MonoBehaviour
{
    private bool isLarge = false; // �tat initial : petit
    private RectTransform rectTransform;

    // Tailles pour les deux �tats
    public Vector2 smallSize = new Vector2(100, 100); // Taille "petit"
    public Vector2 largeSize = new Vector2(150, 150); // Taille "grand"

    // Start is called before the first frame update
    void Start()
    {
        // R�cup�re le RectTransform de l'image
        rectTransform = GetComponent<RectTransform>();

        // D�finit la taille initiale (petite)
        rectTransform.sizeDelta = smallSize;
    }

    // Update is called once per frame
    void Update()
    {
        // V�rifie si l'utilisateur clique sur l'�cran
        if (Input.GetMouseButtonDown(0))
        {
            ToggleSize();
        }
    }

    void ToggleSize()
    {
        // Change la taille en fonction de l'�tat actuel
        if (isLarge)
        {
            rectTransform.sizeDelta = smallSize; // Passe � petit
        }
        else
        {
            rectTransform.sizeDelta = largeSize; // Passe � grand
        }

        // Bascule l'�tat
        isLarge = !isLarge;
    }
}
