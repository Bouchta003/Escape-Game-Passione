using UnityEngine;

public class DebugAnodeCathode : MonoBehaviour
{
    [Header("Locker Interaction Reference")]
    [Tooltip("Reference to the LockerInteraction script to simulate interaction.")]
    [SerializeField] private AnodeCathodePuzzle anodeCathodePuzzle;

    private void Update()
    {
        // Simulate player interaction with the locker
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (anodeCathodePuzzle != null)
            {
                anodeCathodePuzzle.Interact();
                Debug.Log("Simulated Anode & Cathode Puzzle (Interact).");
            }
            else
            {
                Debug.LogWarning("AnodeCathodePuzzle reference is missing.");
            }
        }

        // Simulate exiting the interaction and returning to the normal view
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (anodeCathodePuzzle != null)
            {
                anodeCathodePuzzle.ExitInteraction();
                Debug.Log("Simulated exiting Anode & Cathode Puzzle (ExitInteraction).");
            }
            else
            {
                Debug.LogWarning("AnodeCathodePuzzle reference is missing.");
            }
        }
    }
}
