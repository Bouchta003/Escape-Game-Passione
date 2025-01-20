using UnityEngine;
using System.Collections;

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

    [Header("Sound Effects")]
    AudioSource audioSource;

    [Tooltip("Sound playing when scanning the chosen elements.")]
    [SerializeField] private AudioClip scanningSound;

    [Tooltip("Sound for failing the experiment.")]
    [SerializeField] private AudioClip failSound;

    [Tooltip("Sound for choosing the right elements.")]
    [SerializeField] private AudioClip successSound;

    [Header("Unlockable Door")]
    [Tooltip("The door to unlock upon solving the puzzle.")]
    [SerializeField] GameObject unlockableDoor;

    static bool isInteracting = false;
    static public bool IsInteracting => isInteracting;

    void Start()
    {
        // Ensure keypad UI elements are hidden at the start
        slotsCanvas.SetActive(false);

        audioSource = GetComponent<AudioSource>();
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

        if (anode != null && cathode != null)
        {
            StartCoroutine(PlaySoundsAndCheckCombination(anode, cathode));
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

    public AudioClip GetPickUpSound() => null;

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

    /// <summary>
    /// Plays sounds in sequence and checks the combination of anode and cathode materials.
    /// </summary>
    /// <param name="anode">The selected anode material.</param>
    /// <param name="cathode">The selected cathode material.</param>
    /// <returns>An enumerator for coroutine control.</returns>
    IEnumerator PlaySoundsAndCheckCombination(MaterialData anode, MaterialData cathode)
    {
        // Play the scanning sound
        yield return PlaySoundAndContinue(scanningSound);

        // Get material names for comparison
        string anodeName = anode.materialName;
        string cathodeName = cathode.materialName;

        if (anodeName == "Graphite" && cathodeName == "Cobalt")
        {
            // Play success sound and unlock the door
            yield return PlaySoundAndContinue(successSound);
            Debug.Log("Correct Combination! Door Unlocks.");
            ExitInteraction();
            UnlockDoor();
        }
        else
        {
            // Play failure sound
            yield return PlaySoundAndContinue(failSound);
            Debug.Log("Combination is not optimal.");
        }
    }

    /// <summary>
    /// Plays a sound clip and waits for it to finish before continuing.
    /// </summary>
    /// <param name="soundClip">The sound clip to play.</param>
    /// <returns>An enumerator for coroutine control.</returns>
    IEnumerator PlaySoundAndContinue(AudioClip soundClip)
    {
        if (audioSource != null && soundClip != null)
        {
            // Assign and play the audio clip
            audioSource.clip = soundClip;
            audioSource.Play();

            // Wait for the duration of the audio clip
            yield return new WaitForSeconds(soundClip.length);

            Debug.Log("Sound finished! Continuing execution.");
        }
        else
        {
            Debug.LogWarning("AudioSource or AudioClip is missing.");
        }
    }
}
