using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // N�cessaire pour g�rer les sc�nes
using TMPro;

public class TextOutro : MonoBehaviour
{
    public TextMeshProUGUI uiText; // R�f�rence au composant Text
    private string currentMessage = ""; // Stocke le message actuellement affich�
    private int currentIndex = 0; // Stocke l'indice actuel du message affich�

    // Liste des textes � afficher
    private string[] messages = {
        "We did it! The USB key is safe. All the data�everything we need to revolutionize energy storage�it�s all here.", //Ampaire
        "Victory dance activated! You�ve saved the day, Ampaire� and with just 35 seconds to spare.", //Voltix
        "Close calls seem to be our specialty.", //Ampaire
        "But I couldn�t have done this without you� or without the help of our team of problem-solvers.", //Ampaire
        "That�s right!", //Voltix
        "To everyone who helped us along the way�your sharp eyes, quick thinking, and determination made all the difference.", //Voltix
        "You�re as much a part of this breakthrough as we are!", //Voltix
        "Together, we�ve ensured that this technology will reach the world. A brighter, more sustainable future is now within our grasp.", //Ampaire
        "And� the next adventure awaits! After all, what�s innovation without a little intrigue?", //Voltix
        "Let�s not get ahead of ourselves, Voltix. For now, let�s focus on what comes next: securing the data and moving forward with the project.", //Ampaire
        "But� if another mystery comes our way, I know exactly who I�ll call for help.", //Ampaire
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

        // Passe � l'indice suivant, en cyclant si n�cessaire
        currentIndex = (currentIndex + 1) % messages.Length;
        /*if (currentIndex == 0)
        {
            currentMessage = messages[currentIndex];
            typingCoroutine = StartCoroutine(TypeText(currentMessage));
            currentIndex += 1;
        }
        else
        {
            // Met � jour le message et l'indice
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
        Debug.Log("Chargement de la sc�ne suivante : Scene 1 (Index 1)");
        SceneManager.LoadScene(4); // Charge la sc�ne par son index (sc�ne num�ro 1)
    }

    // Fonction pour obtenir l'indice actuel
    public int GetCurrentIndex()
    {
        return currentIndex;
    }
}
