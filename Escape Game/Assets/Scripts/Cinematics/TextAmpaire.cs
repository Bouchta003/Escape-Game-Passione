using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextAmpaire : MonoBehaviour
{
    public TextMeshProUGUI uiText; // R�f�rence au composant Text
    private int clickCount = 0; // Compteur de clics

    // Liste des textes � afficher
    private string[] messages = {
        "Bonjour !",
        "Comment �a va ?",
        "Bienvenue sur Unity !",
        "Vous avez cliqu� plusieurs fois.",
        "C'est amusant, non ?"
    };

    // Update is called once per frame
    void Update()
    {
        // V�rifie si l'utilisateur clique avec la souris
        if (Input.GetMouseButtonDown(0))
        {
            ChangeText();
        }
    }

    void ChangeText()
    {
        // Change le contenu du texte en fonction du compteur
        uiText.text = messages[clickCount % messages.Length];

        // Incr�mente le compteur
        clickCount++;
    }
}
