using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // N�cessaire pour g�rer les sc�nes
using TMPro;

public class TextAmpaire : MonoBehaviour
{
    public TextMeshProUGUI uiText; // R�f�rence au composant Text
    private string currentMessage = ""; // Stocke le message actuellement affich�
    private int currentIndex = 0; // Stocke l'indice actuel du message affich�

    // Liste des textes � afficher
    private string[] messages = {
        "Alright, Ampaire. Deep breaths.",
        "They�re here to see the future, and you�ve got it� except I don�t have it.",
        "Oh no, where�s the USB key?!",
        "If it is not here, it must be in my laboratory!",
        "Voltix, are you there? This is an emergency!",
        "Ready for action, Master Ampaire! What seems to be the problem?",
        "Voltix, my USB key� It�s not here!",
        "I must have forgotten it in my laboratory, do you see it?",
        "I know where it is, but I've forgotten the code for the chest.",
        "What?! This is catastrophic! How could this have happened?!",
        "Regardless, you must find the code and open the chest!",
        "I left some clues to find it in the laboratory, but I don�t have time to help you.",
        "You�ve got only 45 minutes before the presentation ends!",
        "Piece of cake! No worries, I�ll handle it. Sit tight!",
        "This is serious, Voltix. Don�t let me down.",
        "Roger that! Mission: USB Key Recovery is a go!",
        ""
    };

    public float typingSpeed = 0.05f; // Vitesse d'affichage des caract�res
    private Coroutine typingCoroutine; // Stocke la coroutine en cours
    private bool isTyping = false; // Indique si le texte est en train de s'�crire

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isTyping)
            {
                FinishTyping();
            }
            else
            {
                ChangeText();
            }
        }
    }

    void ChangeText()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        // V�rifie si on est � la fin du tableau
        if (currentIndex >= messages.Length - 1)
        {
            LoadNextScene(); // Change la sc�ne
            return;
        }

        // Met � jour le message et l'indice
        currentMessage = messages[currentIndex];
        typingCoroutine = StartCoroutine(TypeText(currentMessage));

        // Passe � l'indice suivant
        currentIndex++;
    }

    IEnumerator TypeText(string text)
    {
        uiText.text = "";
        isTyping = true;

        foreach (char letter in text)
        {
            uiText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
        typingCoroutine = null;
    }

    void FinishTyping()
    {
        uiText.text = currentMessage;

        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
            typingCoroutine = null;
        }

        isTyping = false;
    }

    void LoadNextScene()
    {
        Debug.Log("Chargement de la sc�ne suivante : Scene 1 (Index 1)");
        SceneManager.LoadScene(1); // Charge la sc�ne par son index (sc�ne num�ro 1)
    }

    // Fonction pour obtenir l'indice actuel
    public int GetCurrentIndex()
    {
        return currentIndex;
    }
}
