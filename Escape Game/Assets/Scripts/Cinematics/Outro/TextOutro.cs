using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Nécessaire pour gérer les scènes
using TMPro;

public class TextOutro : MonoBehaviour
{
    public TextMeshProUGUI uiText; // Référence au composant Text
    private string currentMessage = ""; // Stocke le message actuellement affiché
    private int currentIndex = 0; // Stocke l'indice actuel du message affiché

    // Liste des textes à afficher
    private string[] messages = {
        "... and that’s why this innovation is important...",
        "Special delivery for Master Ampaire!",
        "You’re a lifesaver, Voltix. I owe you one.",
        "Just doing my job!",
        "Ladies and gentlemen, let me show the future of innovation",
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

        // Passe à l'indice suivant, en cyclant si nécessaire
        currentIndex = (currentIndex + 1) % messages.Length;
        /*if (currentIndex == 0)
        {
            currentMessage = messages[currentIndex];
            typingCoroutine = StartCoroutine(TypeText(currentMessage));
            currentIndex += 1;
        }
        else
        {
            // Met à jour le message et l'indice
            currentIndex = (currentIndex + 1) % messages.Length; // Tourne dans la liste
            currentMessage = messages[currentIndex];
            typingCoroutine = StartCoroutine(TypeText(currentMessage));
        }*/
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
        SceneManager.LoadScene(5); // Charge la scène par son index (scène numéro 1)
    }

    // Fonction pour obtenir l'indice actuel
    public int GetCurrentIndex()
    {
        return currentIndex;
    }
}
