using UnityEngine;

public class DebugLockerInteraction : MonoBehaviour
{
    [Header("Locker Interaction Reference")]
    [Tooltip("Reference to the LockerInteraction script to simulate interaction.")]
    [SerializeField] private LockerInteraction lockerInteraction;

    private void Update()
    {
        // Simulate player interaction with the locker
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (lockerInteraction != null)
            {
                lockerInteraction.Interact();
                Debug.Log("Simulated locker interaction (Interact).");
            }
            else
            {
                Debug.LogWarning("LockerInteraction reference is missing.");
            }
        }

        // Simulate exiting the interaction and returning to the normal view
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (lockerInteraction != null)
            {
                lockerInteraction.ExitInteraction();
                Debug.Log("Simulated exiting locker interaction (ExitInteraction).");
            }
            else
            {
                Debug.LogWarning("LockerInteraction reference is missing.");
            }
        }
    }
}
