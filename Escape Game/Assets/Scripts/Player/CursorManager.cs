using UnityEngine;

public class CursorManager : MonoBehaviour
{
    void Start()
    {
        // Rendre le curseur visible
        Cursor.visible = true;

        // Déverrouiller le curseur pour qu'il puisse se déplacer
        Cursor.lockState = CursorLockMode.None;
    }
}
