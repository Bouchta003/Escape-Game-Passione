using UnityEngine;

public class BlinkEffect : MonoBehaviour
{
    [Header("Blink Settings")]
    [Tooltip("Color to blink to.")]
    [SerializeField] Color blinkColor = Color.red;

    [Tooltip("Speed of the blinking effect.")]
    [Range(0f, 10f)]
    [SerializeField] float blinkSpeed = 1f;

    [Header("State")]
    [Tooltip("Indicates if the object is currently blinking.")]
    [SerializeField] bool isBlinking = false;

    Renderer objectRenderer;
    Color originalColor;
    float blinkTimer = 0f;

    // Awake is called when a script instance is being loaded
    void Awake()
    {
        if (TryGetComponent(out objectRenderer)) originalColor = objectRenderer.material.color;
        else Debug.LogError("BlinkEffect requires a Renderer Component");

    }

    // Start is called before the first frame update
    void Start()
    {
        StartBlinking(); // Starts blinking when the script starts
    }

    // Update is called once per frame
    void Update()
    {
        if (isBlinking && objectRenderer != null)
        {
            // Create a blinking effect by oscillating between original and blink colors
            blinkTimer += Time.deltaTime * blinkSpeed;
            objectRenderer.material.color = Color.Lerp(originalColor, blinkColor, Mathf.PingPong(blinkTimer, 1f));
        }
    }

    /// <summary>
    /// Starts the blinking effect.
    /// </summary>
    public void StartBlinking() => isBlinking = true;

    /// <summary>
    /// Stops the blinking effect and resets the color to the original.
    /// </summary>
    public void StopBlinking()
    {
        isBlinking = false;
        blinkTimer = 0f;

        // Reset material properties
        if(objectRenderer != null) objectRenderer.material.color = originalColor;
    }
}
