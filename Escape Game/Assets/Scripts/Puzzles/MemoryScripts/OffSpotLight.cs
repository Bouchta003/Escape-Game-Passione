using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffSpotLight: MonoBehaviour
{
    public Light spotLight; // Le Spot Light à désactiver
    public GameObject puzzleUI; // L'interface du puzzle à activer
    public string playerTag = "Player"; // Tag pour détecter le joueur

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag)) // Vérifie que c'est le joueur qui entre
        {
            // Éteindre la lumière
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

