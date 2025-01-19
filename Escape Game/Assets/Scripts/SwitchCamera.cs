using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCamera : MonoBehaviour
{
    public Camera mainCamera; // La cam�ra qui suit le joueur (de votre camarade)
    public Camera MemoryCamera; // La cam�ra sp�cifique � votre puzzle

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // V�rifie que c'est bien le joueur
        {
            Debug.Log("Trigger activated!");
            mainCamera.enabled = false;

            // Activer la cam�ra du puzzle
            MemoryCamera.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // Quand le joueur quitte la salle
        {
            Debug.Log("Trigger deactivated!");
            // R�activer la cam�ra principale
            mainCamera.enabled = true;

            // D�sactiver la cam�ra du puzzle
            MemoryCamera.enabled = false;
        }
    }
}


