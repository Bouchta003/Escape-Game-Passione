using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMManager : MonoBehaviour
{
    static BGMManager instance;

    AudioSource audioSource;

    [Header("Scenes to Mute Music")]
    [Tooltip("Add scene names where the music should stop or mute.")]
    public string[] scenesToMute;

    void Awake()
    {
        // Ensure there is only one instance of the BackgroundMusicManager
        if (instance != null)
        {
            Destroy(gameObject); // Destroy duplicate instance
            return;
        }

        // Set this GameObject as the singleton instance
        instance = this;

        // Prevent this GameObject from being destroyed across scene loads
        DontDestroyOnLoad(gameObject);

        // Get the AudioSource component
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("No AudioSource found on the BackgroundMusicManager GameObject.");
        }
    }

    /// <summary>
    /// Call this method to change the background music clip at runtime.
    /// </summary>
    /// <param name="newClip">The new AudioClip to play.</param>
    public void ChangeMusic(AudioClip newClip)
    {
        if (audioSource != null && newClip != null)
        {
            audioSource.Stop();
            audioSource.clip = newClip;
            audioSource.Play();
        }
    }

    /// <summary>
    /// Call this method to adjust the volume of the music.
    /// </summary>
    /// <param name="volume">Volume level (0 to 1).</param>
    public void SetVolume(float volume)
    {
        if (audioSource != null)
        {
            audioSource.volume = Mathf.Clamp01(volume);
        }
    }

    /// <summary>
    /// Call this method to mute or unmute the music.
    /// </summary>
    /// <param name="mute">True to mute, false to unmute.</param>
    public void SetMute(bool mute)
    {
        if (audioSource != null)
        {
            audioSource.mute = mute;
        }
    }

    void OnEnable()
    {
        // Subscribe to sceneLoaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        // Unsubscribe from sceneLoaded event
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Check if the current scene is in the list of scenes to mute
        foreach (string sceneName in scenesToMute)
        {
            if (scene.name == sceneName)
            {
                audioSource.Stop(); // Stop the music for this scene
                return;
            }
        }

        // If not in the mute list, ensure the music is playing
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}
