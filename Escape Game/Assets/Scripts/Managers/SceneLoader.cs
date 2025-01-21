using UnityEngine;
using UnityEngine.SceneManagement; // Nécessaire pour la gestion des scènes

public class SceneLoader : MonoBehaviour
{
    // Méthode pour charger une nouvelle scène
    public void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // Méthode pour charger une scène via son index
    public void LoadSceneByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
