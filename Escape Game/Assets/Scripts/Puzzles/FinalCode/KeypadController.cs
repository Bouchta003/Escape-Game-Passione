using UnityEngine;
using TMPro;

public class KeypadController : MonoBehaviour
{
    [Header("UI Components")]
    [Tooltip("Text field to display the entered code.")]
    [SerializeField] private TMP_Text codeDisplay;

    [Header("Locker Interaction")]
    [Tooltip("Reference to the locker interaction script.")]
    [SerializeField] private LockerInteraction lockerInteraction;

    [Header("Code Settings")]
    [Tooltip("Maximum allowed code length.")]
    [SerializeField] private int maxCodeLength = 3;

    private string enteredCode = ""; // Stores the currently entered code

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
            Debug.Log("Correct code entered! Unlocking locker...");
            lockerInteraction.UnlockLocker();
        }
        else
        {
            Debug.Log("Incorrect code entered!");
            ResetCode();
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
    private void UpdateCodeDisplay()
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

    private void OnValidate()
    {
        // Ensures maxCodeLength is positive during editing
        if (maxCodeLength < 1)
        {
            maxCodeLength = 1;
        }
    }
}
