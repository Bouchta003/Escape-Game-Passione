using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Nécessaire pour gérer les scènes
using TMPro;

public class TextAmpaire : MonoBehaviour
{
    public TextMeshProUGUI uiText; // Référence au composant Text
    private string currentMessage = ""; // Stocke le message actuellement affiché
    private int currentIndex = 0; // Stocke l'indice actuel du message affiché

    // Liste des textes à afficher
    private string[] messages = {
        "Alright, Ampaire. Deep breaths.",
        "They’re here to see the future, and you’ve got it… except I don’t have it.",
        "Oh no, where’s the USB key?!",
        "If it is not here, it must be in my laboratory!",
        "Voltix, are you there? This is an emergency!",
        "Ready for action, Master Ampaire! What seems to be the problem?",
        "Voltix, my USB key… It’s not here!",
        "I must have forgotten it in my laboratory, do you see it?",
        "I know where it is, but I've forgotten the code for the chest.",
        "What?! This is catastrophic! How could this have happened?!",
        "Regardless, you must find the code and open the chest!",
        "I left some clues to find it in the laboratory, but I don’t have time to help you.",
        "You’ve got only 45 minutes before the presentation ends!",
        "Piece of cake! No worries, I’ll handle it. Sit tight!",
        "This is serious, Voltix. Don’t let me down.",
        "Roger that! Mission: USB Key Recovery is a go!",
        ""
    };

    public float typingSpeed = 0.05f; // Vitesse d'affichage des caractères
    private Coroutine typingCoroutine; // Stocke la coroutine en cours
    private bool isTyping = false; // Indique si le texte est en train de s'écrire

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

        // Vérifie si on est à la fin du tableau
        if (currentIndex >= messages.Length - 1)
        {
            LoadNextScene(); // Change la scène
            return;
        }

        // Met à jour le message et l'indice
        currentMessage = messages[currentIndex];
        typingCoroutine = StartCoroutine(TypeText(currentMessage));

        // Passe à l'indice suivant
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
        Debug.Log("Chargement de la scène suivante : Scene 1 (Index 1)");
        SceneManager.LoadScene(1); // Charge la scène par son index (scène numéro 1)
    }

    // Fonction pour obtenir l'indice actuel
    public int GetCurrentIndex()
    {
        return currentIndex;
    }
}
