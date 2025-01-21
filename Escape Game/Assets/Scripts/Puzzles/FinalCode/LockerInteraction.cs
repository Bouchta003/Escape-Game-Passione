using UnityEngine;
using System.Collections;

public class LockerInteraction : MonoBehaviour, IInteractable
{
    #region Attributes
    [Header("Cameras")]
    [Tooltip("Reference to the main camera.")]
    [SerializeField] Camera mainCamera;

    [Tooltip("Reference to the keypad camera.")]
    [SerializeField] Camera keypadCamera;

    [Header("UI Elements")]
    [Tooltip("Canvas for the keypad.")]
    [SerializeField] GameObject keypadCanvas;

    [Tooltip("Reference to the 'Return' button.")]
    [SerializeField] GameObject returnButton;

    [Header("Keypad Settings")]
    [Tooltip("Reference to the KeypadController script.")]
    [SerializeField] KeypadController keypadController;

    [Tooltip("Correct code for unlocking the locker.")]
    [SerializeField] string correctCode = "704";
    public string CorrectCode => correctCode;

    [Header("Locker Animation")]
    [Tooltip("Animator for the locker.")]
    [SerializeField] Animator lockerAnimator;

    [Header("Puzzle Elements")]
    [Tooltip("The USB Key to retrieve.")]
    [SerializeField] GameObject lootUSBKey;


    bool isInteracting = false;
    AudioSource audioSource;
    #endregion

    void Start()
    {
        // Ensure keypad UI elements are hidden at the start
        keypadCanvas.SetActive(false);
        returnButton.SetActive(false);

        // Initialize AudioSource
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
    /// Starts the interaction with the locker.
    /// </summary>
    public void Interact()
    {
        if (isInteracting) return; // Prevent multiple interactions

        isInteracting = true;

        // Switch to keypad camera and enable keypad UI
        mainCamera.gameObject.SetActive(false);
        keypadCamera.gameObject.SetActive(true);
        keypadCanvas.SetActive(true);
        returnButton.SetActive(true);

        EnableCursor();
    }

    public AudioClip GetPickUpSound() => null;

    /// <summary>
    /// Ends the interaction with the locker and resets the view.
    /// </summary>
    public void ExitInteraction()
    {
        if (!isInteracting) return; // Only exit if currently interacting

        isInteracting = false;

        // Switch back to the main camera and disable keypad UI
        keypadCamera.gameObject.SetActive(false);
        mainCamera.gameObject.SetActive(true);
        keypadCanvas.SetActive(false);
        returnButton.SetActive(false);

        DisableCursor();
    }

    /// <summary>
    /// Unlocks the locker by triggering an animation and exiting the interaction.
    /// </summary>
    public void UnlockLocker()
    {
        Debug.Log("Locker unlocked!");
        if (lockerAnimator != null)
        {
            StartCoroutine(PlaySoundAndContinue());
            lockerAnimator.SetTrigger("LevelFinished");
            lootUSBKey.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Locker animator is not assigned.");
        }

        ExitInteraction();
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
    /// Plays a sound clip and waits for it to finish before continuing.
    /// </summary>
    /// <param name="soundClip">The sound clip to play.</param>
    /// <returns>An enumerator for coroutine control.</returns>
    IEnumerator PlaySoundAndContinue()
    {
        if (audioSource != null)
        {
            // Assign and play the audio clip
            AudioClip soundClip = audioSource.clip;
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