using UnityEngine;

public class TranslationCredits : MonoBehaviour
{
    // Vitesse de translation en unit�s par seconde
    private float vitesse = 100f;

    // Update est appel� une fois par frame
    void Update()
    {
        // D�placement selon l'axe Y
        transform.Translate(Vector3.up * vitesse * Time.deltaTime);
    }
}
