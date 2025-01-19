using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public GameObject puzzleMemoCanvas; // Reference to the Canvas

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Something entered the trigger.");

        if (other.CompareTag("Player")) // Check if it's the Player
        {
            Debug.Log("Player entered the trigger.");

            // Activate the Canvas PuzzleMemo
            if (puzzleMemoCanvas != null)
            {
                puzzleMemoCanvas.SetActive(true);
                Debug.Log("PuzzleMemo Canvas activated.");
            }
            else
            {
                Debug.LogError("PuzzleMemo Canvas reference is missing in the script.");
            }
        }
    }
}

