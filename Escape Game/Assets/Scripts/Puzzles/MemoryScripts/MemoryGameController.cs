using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class MemoryGameController : MonoBehaviour
{
    public Image supplyChainImage; // Image de la chaîne d'approvisionnement
    public Image periodicTableImage; // Image du tableau périodique
    public Image numberSequenceImage; // Nouvelle référence pour la séquence de nombres (en tant qu'image)
    public Image inutile1; // Image inutile 1 qui doit disparaître
    public Image inutile2; // Image inutile 2 qui doit disparaître

    public TextMeshProUGUI countdownText; // Référence au texte du compte à rebours
    public TextMeshProUGUI tapToContinueText; // Texte "Tap to Continue", qui sera ensuite utilisé pour afficher les questions
    public Button[] answerButtons; // Boutons pour les réponses
    public TMP_FontAsset bangersFont; // Référence à la police "Bangers-Regular SDF" que vous utilisez

    public AudioClip countdownClip; // Clip audio pour le compte à rebours
    public AudioClip yayClip; // Clip audio pour la fin du quiz
    public AudioSource audioSource; // Composant AudioSource pour jouer les sons

    public float displayTime = 5f;
    public float questionFontSize = 40f;

    private int currentQuestionIndex = 0; // Pour suivre quelle question est affichée
    private int correctAnswerCount = 0; // Compte le nombre de bonnes réponses

    // Liste des questions et des réponses
    private string[] questions = {
        "What were the height and width of the battery shown?",
        "In which stage are the cell manufacturers within the supply chain?",
        "What is the atomic number of Nickel?"
    };

    private string[][] answers = {
        new string[] { "61.5 and 33.2", "63.2 and 9.5", "61.5 and 9.5" },
        new string[] { "3rd", "4th", "2nd" },
        new string[] { "14", "28", "7" }
    };

    private int[] correctAnswers = { 0, 2, 1 }; // Index des bonnes réponses dans chaque question

    private void Start()
    {
        StartGame(); // Démarre le jeu au début
    }

    void StartGame()
    {
        // Initialiser les propriétés de texte pour la police, la taille, et la couleur
        SetTextProperties();

        // Initialiser l'état des éléments UI
        tapToContinueText.gameObject.SetActive(false); // Désactiver le texte "Tap to Continue" au début
        tapToContinueText.GetComponent<Button>().interactable = false; // Désactiver l'interaction

        // Désactiver les boutons de réponse au début
        foreach (Button btn in answerButtons)
        {
            btn.gameObject.SetActive(false);
        }

        // Activer les images pour commencer
        supplyChainImage.enabled = true;
        periodicTableImage.enabled = true;
        numberSequenceImage.enabled = true;
        inutile1.enabled = true; // Activer l'image inutile1 au début
        inutile2.enabled = true; // Activer l'image inutile2 au début

        // Réinitialiser le score et l'index de la question
        currentQuestionIndex = 0;
        correctAnswerCount = 0;

        // Lancer le compte à rebours
        StartCoroutine(CountdownThenShowTapToContinue(25)); // Compte à rebours de 20 secondes avant de masquer les images et afficher "TAP TO CONTINUE"

        // Jouer le son de compte à rebours pendant 21 secondes
        if (audioSource != null && countdownClip != null)
        {
            audioSource.clip = countdownClip;
            audioSource.loop = true; // Boucle pour maintenir le son pendant 21 secondes
            audioSource.Play();
            Invoke("StopCountdownSound", 26f); // Arrêter le son de compte à rebours après 21 secondes
        }
    }

    private void SetTextProperties()
    {
        // Définir les propriétés de texte pour garantir la consistance
        tapToContinueText.font = bangersFont; // Assure-toi que la police est bien définie à "Bangers-Regular SDF"
        tapToContinueText.fontSize = 36; // Définir la taille du texte pour "TAP TO CONTINUE"
        tapToContinueText.color = Color.white; // Définir la couleur du texte (par exemple, blanc)
        tapToContinueText.fontStyle = FontStyles.Normal; // Peut être Normal, Bold, Italic, etc.
    }

    IEnumerator CountdownThenShowTapToContinue(int countdownTime)
    {
        // Activer le texte du compte à rebours
        countdownText.gameObject.SetActive(true);

        // Lancer le compte à rebours
        while (countdownTime > 0)
        {
            countdownText.text = "Time remaining: " + countdownTime + " seconds";
            yield return new WaitForSeconds(1f);
            countdownTime--;
        }

        // Désactiver le texte du compte à rebours une fois terminé
        countdownText.gameObject.SetActive(false);

        // Masquer les images une fois le compte à rebours terminé
        supplyChainImage.enabled = false;
        periodicTableImage.enabled = false;
        numberSequenceImage.enabled = false;
        inutile1.enabled = false; // Masquer l'image inutile1 après le compte à rebours
        inutile2.enabled = false; // Masquer l'image inutile2 après le compte à rebours

        // Activer le texte "TAP TO CONTINUE" après que les images ont disparu
        tapToContinueText.gameObject.SetActive(true);
        tapToContinueText.text = "TAP TO CONTINUE";
        tapToContinueText.font = bangersFont; // Redéfinir la police pour s'assurer que c'est bien Bangers
        tapToContinueText.GetComponent<Button>().interactable = true; // Rendre le texte cliquable

        // Ajouter l'événement pour déclencher OnTapToContinue lorsque l'utilisateur clique sur le texte
        Button tapButton = tapToContinueText.GetComponent<Button>();
        if (tapButton != null)
        {
            tapButton.onClick.RemoveAllListeners(); // Supprimer les anciens listeners s'il y en a
            tapButton.onClick.AddListener(OnTapToContinue);
        }
    }

    void StopCountdownSound()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    void OnTapToContinue()
    {
        Debug.Log("Tap to Continue clicked!"); // Vérifie que cette méthode est bien appelée

        // Désactiver l'interaction après l'appui
        tapToContinueText.GetComponent<Button>().interactable = false;

        // Utiliser `tapToContinueText` pour afficher la question suivante
        ShowQuestion(currentQuestionIndex);
    }

    void ShowQuestion(int questionIndex)
    {
        Debug.Log("ShowQuestion called for question index: " + questionIndex); // Vérifie que ShowQuestion est bien appelée

        tapToContinueText.gameObject.SetActive(true);
        tapToContinueText.text = questions[questionIndex];
        tapToContinueText.font = bangersFont; // Redéfinir la police pour s'assurer qu'elle est correcte
        tapToContinueText.fontSize = questionFontSize; // Définir la taille de la police pour les questions
        tapToContinueText.color = new Color(1f, 1f, 1f, 1f); // Assurer l'opacité totale du texte

        // Configurer les boutons de réponse pour la question actuelle
        for (int i = 0; i < answerButtons.Length; i++)
        {
            if (i < answers[questionIndex].Length)
            {
                Debug.Log("Configuring answer button: " + i); // Vérifie que les boutons de réponse sont bien configurés
                answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = answers[questionIndex][i];
                answerButtons[i].gameObject.SetActive(true);
                int answerIndex = i; // Pour éviter un problème de référence dans la closure
                answerButtons[i].onClick.RemoveAllListeners(); // Supprime les anciens listeners
                answerButtons[i].onClick.AddListener(() => CheckAnswer(answerIndex == correctAnswers[questionIndex]));
            }
        }
    }

    void CheckAnswer(bool isCorrect)
    {
        Debug.Log("CheckAnswer called - isCorrect: " + isCorrect); // Vérifie que cette méthode est bien appelée

        if (isCorrect)
        {
            Debug.Log("Correct Answer!");
            correctAnswerCount++; // Incrémenter le nombre de bonnes réponses
            tapToContinueText.color = Color.green; // Feedback color pour une bonne réponse
        }
        else
        {
            Debug.Log("Wrong Answer!");
            tapToContinueText.color = Color.red; // Feedback color pour une mauvaise réponse
        }

        StartCoroutine(NextQuestion());
    }

    IEnumerator NextQuestion()
    {
        yield return new WaitForSeconds(2f); // Attendre 2 secondes avant de passer à la prochaine question

        // Passer à la question suivante
        currentQuestionIndex++;

        if (currentQuestionIndex < questions.Length)
        {
            tapToContinueText.color = Color.white; // Remettre la couleur du texte à blanc pour la prochaine question
            ShowQuestion(currentQuestionIndex);
        }
        else
        {
            // Toutes les questions ont été posées
            DisplayFinalScore(); // Afficher le score final
        }
    }

    void DisplayFinalScore()
    {
        // Désactiver les boutons de réponse après la fin du quiz
        foreach (Button btn in answerButtons)
        {
            btn.gameObject.SetActive(false);
        }

        // Afficher le score final
        if (correctAnswerCount == questions.Length)
        {
            tapToContinueText.text = correctAnswerCount + "/" + questions.Length + " - You have unlocked the puzzle, here is the code!";
        }
        else
        {
            tapToContinueText.text = "Your Score: " + correctAnswerCount + "/" + questions.Length + "\nTry Again!";
            StartCoroutine(RestartGameAfterDelay(3f)); // Relancer le jeu après 3 secondes
        }

        tapToContinueText.fontSize = 50; // Augmenter la taille de la police pour le score final, si nécessaire
        tapToContinueText.color = Color.yellow; // Mettre la couleur du texte en jaune pour le score final

        // Jouer le son de félicitations à la fin du quiz pendant 5 secondes
        if (audioSource != null && yayClip != null && correctAnswerCount == questions.Length)
        {
            audioSource.clip = yayClip;
            audioSource.Play();
            Invoke("StopYaySound", 5f);
        }
    }

    IEnumerator RestartGameAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        StartGame(); // Relancer le jeu
    }

    void StopYaySound()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}

