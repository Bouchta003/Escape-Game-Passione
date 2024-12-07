using UnityEngine;

public class AnodeCathodePuzzle : MonoBehaviour
{
    MaterialData selectedAnode;
    MaterialData selectedCathode;

    [Header("Slot Detectors")]
    [Tooltip("Reference to the anode slot detector.")]
    [SerializeField] SlotDetector anodeSlot;

    [Tooltip("Reference to the cathode slot detector.")]
    [SerializeField] SlotDetector cathodeSlot;

    [Header("Unlockable Door")]
    [Tooltip("The door to unlock upon solving the puzzle.")]
    [SerializeField] GameObject unlockableDoor;

    void Update()
    {
        // Check if both slots are occupied
        if (anodeSlot.IsOccupied && cathodeSlot.IsOccupied)
        {
            Debug.Log("Both materials are placed!");
            CheckCombination();
        }
    }

    /// <summary>
    /// Checks the combination of materials in the slots and performs the appropriate action.
    /// </summary>
    public void CheckCombination()
    {
        // Get the materials placed in the slots
        MaterialData anode = anodeSlot.DetectedMaterial;
        MaterialData cathode = cathodeSlot.DetectedMaterial;


        string anodeName = anode.materialName;
        string cathodeName = cathode.materialName;

        if (anode != null && cathode != null)
        {
            int performance = anode.efficiency + cathode.efficiency;

            if (anodeName == "Graphite" && cathodeName == "Cobalt")
            {
                Debug.Log("Correct Combination! Door Unlocks.");
                UnlockDoor();
            }
            else
            {
                Debug.Log("Combination is not optimal.");
            }
        }
        else
        {
            Debug.Log("Select both Anode and Cathode materials!");
        }
    }

    /// <summary>
    /// Unlocks the door using the assigned animation trigger.
    /// </summary>
    void UnlockDoor()
    {
        if (unlockableDoor != null)
        {
            if (unlockableDoor.TryGetComponent<Animator>(out var doorAnimator)) doorAnimator.SetTrigger("Unlock");
        }
    }
}
