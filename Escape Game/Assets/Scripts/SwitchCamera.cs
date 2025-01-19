using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCamera : MonoBehaviour
{
    public Camera mainCamera; // La caméra qui suit le joueur (de votre camarade)
    public Camera MemoryCamera; // La caméra spécifique à votre puzzle

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Vérifie que c'est bien le joueur
        {
            Debug.Log("Trigger activated!");
            mainCamera.enabled = false;

            // Activer la caméra du puzzle
            MemoryCamera.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // Quand le joueur quitte la salle
        {
            Debug.Log("Trigger deactivated!");
            // Réactiver la caméra principale
            mainCamera.enabled = true;

            // Désactiver la caméra du puzzle
            MemoryCamera.enabled = false;
        }
    }
}


