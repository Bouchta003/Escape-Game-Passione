using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffSpotLight: MonoBehaviour
{
    public Light spotLight; // Le Spot Light � d�sactiver
    public GameObject puzzleUI; // L'interface du puzzle � activer
    public string playerTag = "Player"; // Tag pour d�tecter le joueur

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag)) // V�rifie que c'est le joueur qui entre
        {
            // �teindre la lumi�re
            if (spotLight != null)
            {
                spotLight.enabled = false;
            }

            // Activer l'interface utilisateur du puzzle
            if (puzzleUI != null)
            {
                puzzleUI.SetActive(true);
            }
        }
    }
}

