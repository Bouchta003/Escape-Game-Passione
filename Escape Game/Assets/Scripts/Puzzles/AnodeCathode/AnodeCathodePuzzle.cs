using UnityEngine;

public class AnodeCathodePuzzle : MonoBehaviour
{
    public MaterialData selectedAnode;
    public MaterialData selectedCathode;
    public GameObject unlockableDoor;

    public void CheckCombination()
    {
        if (selectedAnode != null && selectedCathode != null)
        {
            int performance = selectedAnode.efficiency + selectedCathode.efficiency;

            if (performance > 100) // Example threshold
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
