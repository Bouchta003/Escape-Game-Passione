using UnityEngine;

public class AnodeCathodePuzzle : MonoBehaviour, IInteractable
{
    MaterialData selectedAnode;
    MaterialData selectedCathode;

    [Header("Slot Detectors")]
    [Tooltip("Reference to the anode slot detector.")]
    [SerializeField] Slot anodeSlot;
    public Slot AnodeSlot => anodeSlot;

    [Tooltip("Reference to the cathode slot detector.")]
    [SerializeField] Slot cathodeSlot;
    public Slot CathodeSlot => cathodeSlot;

    [Header("Cameras")]
    [Tooltip("Reference to the main camera.")]
    [SerializeField] private Camera mainCamera;

    [Tooltip("Reference to the UI camera.")]
    [SerializeField] private Camera slotsCamera;

    [Header("UI Elements")]
    [Tooltip("Canvas for the UI.")]
    [SerializeField] private GameObject slotsCanvas;

    [Header("Unlockable Door")]
    [Tooltip("The door to unlock upon solving the puzzle.")]
    [SerializeField] GameObject unlockableDoor;

    static bool isInteracting = false;
    static public bool IsInteracting => isInteracting;

    void Start()
    {
        // Ensure keypad UI elements are hidden at the start
        slotsCanvas.SetActive(false);
    }

    void Update()
    {
        // Exit interaction with the locker when 'Q' is pressed
        if (isInteracting && Input.GetKeyDown(KeyCode.Q))
        {
            ExitInteraction();
        }
    }

    /// <summary>
    /// Checks the combination of materials in the slots and performs the appropriate action.
    /// </summary>
    public void CheckCombination()
    {
        // Get the materials placed in the slots
        MaterialData anode = anodeSlot.AssignedMaterial;
        MaterialData cathode = cathodeSlot.AssignedMaterial;


        string anodeName = anode.materialName;
        string cathodeName = cathode.materialName;

        if (anode != null && cathode != null)
        {
            if (anodeName == "Graphite" && cathodeName == "Cobalt")
            {
                Debug.Log("Correct Combination! Door Unlocks.");
                ExitInteraction();
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

    public void Interact()
    {
        if (isInteracting) return; // Prevent multiple interactions

        isInteracting = true;

        // Switch to keypad camera and enable keypad UI
        mainCamera.gameObject.SetActive(false);
        slotsCamera.gameObject.SetActive(true);
        slotsCanvas.SetActive(true);

        EnableCursor();
    }

    public void ExitInteraction()
    {
        if (!isInteracting) return; // Only exit if currently interacting

        isInteracting = false;

        // Switch back to the main camera and disable keypad UI
        slotsCamera.gameObject.SetActive(false);
        mainCamera.gameObject.SetActive(true);
        slotsCanvas.SetActive(false);

        DisableCursor();
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

    /// <summary>
    /// Enables the cursor for user interaction.
    /// </summary>
    void EnableCursor()
    {
        Cursor.lockState = CursorLockMode.None; // Unlock cursor
        Cursor.visible = true; // Make cursor visible
    }

    /// <summary>
    /// Disables the cursor to restore game control.
    /// </summary>
    void DisableCursor()
    {
        Cursor.lockState = CursorLockMode.Locked; // Lock cursor
        Cursor.visible = false; // Hide cursor
    }
}
