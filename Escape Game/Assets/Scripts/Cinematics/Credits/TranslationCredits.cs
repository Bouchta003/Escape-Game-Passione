using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranslationCredits : MonoBehaviour
{
    // Vitesse de translation en unités par seconde
    private float vitesse = 60f;

    // Update est appelé une fois par frame
    void Update()
    {
        // Déplacement selon l'axe Y
        transform.Translate(Vector3.up * vitesse * Time.deltaTime);
    }
}
