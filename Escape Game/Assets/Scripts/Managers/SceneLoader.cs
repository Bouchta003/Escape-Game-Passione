using UnityEngine;
using UnityEngine.SceneManagement; // N�cessaire pour la gestion des sc�nes

public class SceneLoader : MonoBehaviour
{
    // M�thode pour charger une nouvelle sc�ne
    public void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // M�thode pour charger une sc�ne via son index
    public void LoadSceneByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
