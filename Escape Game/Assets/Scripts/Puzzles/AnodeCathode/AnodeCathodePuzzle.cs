using UnityEngine;

public class AnodeCathodePuzzle : MonoBehaviour
{
    public MaterialData selectedAnode;
    public MaterialData selectedCathode;
    public SlotDetector anodeSlot; // Reference to the anode slot
    public SlotDetector cathodeSlot; // Reference to the cathode slot

    public GameObject unlockableDoor;

    private void Update()
    {
        // Check if both slots are occupied
        if (anodeSlot.isOccupied && cathodeSlot.isOccupied)
        {
            Debug.Log("Both materials are placed!");
            CheckCombination();
        }
    }

    public void CheckCombination()
    {
        // Get the materials placed in the slots
        MaterialData anode = anodeSlot.detectedMaterial;
        MaterialData cathode = cathodeSlot.detectedMaterial;

        if (anode != null && cathode != null)
        {
            int performance = anode.efficiency + cathode.efficiency;

            if (anode.materialName == "Lithium" && cathode.materialName == "Manganese")
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

    void UnlockDoor()
    {
        unlockableDoor.GetComponent<Animator>().SetTrigger("Unlock");
    }
}
