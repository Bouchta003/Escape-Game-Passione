using UnityEngine;

public class TranslationCredits : MonoBehaviour
{
    // Vitesse de translation en unités par seconde
    private float vitesse = 100f;

    // Update est appelé une fois par frame
    void Update()
    {
        // Déplacement selon l'axe Y
        transform.Translate(Vector3.up * vitesse * Time.deltaTime);
    }
}
