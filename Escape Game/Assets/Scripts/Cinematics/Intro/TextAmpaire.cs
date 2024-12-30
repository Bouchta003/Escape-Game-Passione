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
        "(speaking to herself) The future of sustainable energy depends on this.", //Ampaire
        "Without that USB key, we’ll lose months—no, years of progress.", //Ampaire
        "(chiming in with a mechanical but cheerful voice) Relax, Ampaire.", //Voltix
        "I’m sure we’ll find it.", //Voltix
        "After all, I’m programmed for optimization—and cracking tough mysteries is well within my algorithms!", //Voltix
        "If only I were as confident as your circuits.", //Ampaire
        "That USB key holds the blueprints for the enhanced technology.", //Ampaire
        "Without it, my breakthrough… it’s just a concept. And the world… well, it’s waiting for results, not promises.", //Ampaire
        "(nodding dramatically) Indeed!", //Voltix
        "Plus, let’s not forget: encrypted secrets, potential threats, and… a ticking clock!", //Voltix
        "How exciting!", //Voltix
        "This isn’t a game, Voltix. Someone could have stolen it. Or worse, I could have… misplaced it.", //Ampaire
        "Misplaced it? You, Ampaire the Genius? Impossible!", //Voltix
        "Let’s not tarnish your impeccable reputation just yet. Let me access your task logs and retrace your steps.", //Voltix
        "I already tried that. Everything’s a blur after the conference.", //Ampaire
        "Too much stress. Too many people.", //Ampaire
        "But I remember working late in the lab… and then… nothing.", //Ampaire
        "A mystery worthy of our combined brilliance! Let’s solve it, step by step.", //Voltix
        "Clues are everywhere, if we know where to look. The USB key can’t have gone far.", //Voltix
        "You’re right. Time to stop worrying and start searching. Voltix, initiate search protocols. ", //Ampaire
        "Scan for anomalies in the lab first. If we don’t find anything here, we’ll expand to the conference hall.", //Ampaire
        "Consider it done. And don’t worry, Ampaire. With your brain and my circuits, nothing stays lost for long.", //Voltix
        "Let’s hope you’re right. The world’s watching us, Voltix. We can’t let them down.", //Ampaire
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
