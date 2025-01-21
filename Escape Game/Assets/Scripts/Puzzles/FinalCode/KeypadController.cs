using UnityEngine;
using System.Collections;
using TMPro;

public class KeypadController : MonoBehaviour
{
    #region Attributes
    [Header("UI Components")]
    [Tooltip("Text field to display the entered code.")]
    [SerializeField] TMP_Text codeDisplay;

    [Header("Locker Interaction")]
    [Tooltip("Reference to the locker interaction script.")]
    [SerializeField] LockerInteraction lockerInteraction;

    [Header("Sound Effects")]
    [Tooltip("Source to output the sound.")]
    [SerializeField] AudioSource audioSource;

    [Tooltip("Sound for incorrect code input")]
    [SerializeField] AudioClip failSound;

    [Tooltip("Sound for successful code input")]
    [SerializeField] AudioClip successSound;

    [Header("Code Settings")]
    [Tooltip("Maximum allowed code length.")]
    [SerializeField] int maxCodeLength = 3;

    string enteredCode = ""; // Stores the currently entered code
    #endregion

    /// <summary>
    /// Adds a digit to the entered code and updates the display.
    /// </summary>
    /// <param name="digit">The digit to add.</param>
    public void AddDigit(string digit)
    {
        if (enteredCode.Length < maxCodeLength)
        {
            enteredCode += digit;
            UpdateCodeDisplay();
        }
    }

    /// <summary>
    /// Checks the entered code against the correct code.
    /// </summary>
    public void ConfirmCode()
    {
        if (enteredCode == lockerInteraction.CorrectCode)
        {
            StartCoroutine(UnlockLocker());
        }
        else
        {
            StartCoroutine(ProcessWrongInput());
        }
    }

    /// <summary>
    /// Resets the entered code and clears the display.
    /// </summary>
    public void ResetCode()
    {
        enteredCode = "";
        UpdateCodeDisplay();
    }

    /// <summary>
    /// Updates the displayed code in the UI.
    /// </summary>
    void UpdateCodeDisplay()
    {
        if (codeDisplay != null)
        {
            codeDisplay.text = enteredCode;
        }
        else
        {
            Debug.LogWarning("Code display reference is missing.");
        }
    }

    /// <summary>
    /// Launches a process when the player gets the right code.
    /// </summary>
    /// <returns>An enumerator for coroutine control.</returns>
    IEnumerator UnlockLocker() {
        yield return PlaySoundAndContinue(successSound);
        Debug.Log("Correct code entered! Unlocking locker...");
        lockerInteraction.UnlockLocker();
    }

    /// <summary>
    /// Plays a sound effect letting the player know that the input is wrong
    /// </summary>
    /// <returns>An enumerator for coroutine control.</returns>
    IEnumerator ProcessWrongInput()
    {
        yield return PlaySoundAndContinue(failSound);
        Debug.Log("Incorrect code entered!");
        ResetCode();
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


    void OnValidate()
    {
        // Ensures maxCodeLength is positive during editing
        if (maxCodeLength < 1)
        {
            maxCodeLength = 1;
        }
    }
}
