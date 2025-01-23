using UnityEngine;

public class CursorManager : MonoBehaviour
{
    void Start()
    {
        // Rendre le curseur visible
        Cursor.visible = true;

        // D�verrouiller le curseur pour qu'il puisse se d�placer
        Cursor.lockState = CursorLockMode.None;
    }
}
