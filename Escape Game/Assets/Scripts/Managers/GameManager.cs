using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Player_Inventory.inventory.Contains("USBKey"))
        {
            EnableCursor();
            SceneManager.LoadScene("Outro");
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

}
