using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Player_Inventory.inventory.Contains("USBKey")) SceneManager.LoadScene("Outro");
    }
}
