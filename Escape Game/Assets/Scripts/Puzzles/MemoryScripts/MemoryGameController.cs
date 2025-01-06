using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class MemoryGameController : MonoBehaviour
{
    public Image supplyChainImage; // Image de la chaîne d'approvisionnement
    public Image periodicTableImage; // Image du tableau périodique
    public Image numberSequenceImage; // Nouvelle référence pour la séquence de nombres (en tant qu'image)
    //public Image inutile1; // Image inutile 1 qui doit disparaître
    public Image inutile2; // Image inutile 2 qui doit disparaître

    public GameObject[] panels; // Un tableau pour contenir vos panels


    public TextMeshProUGUI countdownText; // Référence au texte du compte à rebours
    public TextMeshProUGUI tapToContinueText; // Texte "Tap to Continue", qui sera ensuite utilisé pour afficher les questions
    public Button[] answerButtons; // Boutons pour les réponses
    public TMP_FontAsset bangersFont; // Référence à la police "Bangers-Regular SDF" que vous utilisez
    public TMP_FontAsset digitalFont; // Police pour le compte à rebours


    public AudioClip countdownClip; // Clip audio pour le compte à rebours
    public AudioClip yayClip; // Clip audio pour la fin du quiz
    public AudioSource audioSource; // Composant AudioSource pour jouer les sons

    public float displayTime = 5f;
    public float questionFontSize = 40f;

    private int currentQuestionIndex = 0; // Pour suivre quelle question est affichée
    private int correctAnswerCount = 0; // Compte le nombre de bonnes réponses
    private int selectedButtonIndex = 0; // Index du bouton actuellement sélectionné
    private bool canContinue = false; // Indique si le joueur peut continuer



    // Liste des questions et des réponses
    private string[] questions = {
        "What were the height and width of the battery shown?",
        "In which stage are the cell manufacturers within the supply chain?",
        "What is the atomic number of Nickel?"
    };

    private string[][] answers = {
        new string[] { "61.5  and  33.2", "63.2  and  9.5", "61.5  and  9.5" },
        new string[] { "2nd", "4th", "3rd" },
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
        canContinue = true; // Permettre au joueur de continuer avec la barre d'espace
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
        inutile2.enabled = true; // Activer l'image inutile2 au début

        // Réinitialiser le score et l'index de la question
        currentQuestionIndex = 0;
        correctAnswerCount = 0;

        // Afficher un message initial avant le compte à rebours
        StartCoroutine(DisplayMessageThenStartCountdown());
    }

    private void SetTextProperties()
    {
        // Définir les propriétés de texte pour garantir la consistance
        tapToContinueText.font = bangersFont; // Assure-toi que la police est bien définie à "Bangers-Regular SDF"
        tapToContinueText.fontSize = 36; // Définir la taille du texte pour "TAP TO CONTINUE"
        tapToContinueText.color = Color.white; // Définir la couleur du texte (par exemple, blanc)
        tapToContinueText.fontStyle = FontStyles.Normal; // Peut être Normal, Bold, Italic, etc.
    }

    private void SetCountdownTextProperties(TMP_FontAsset font, float fontSize, Color color)
    {
        countdownText.font = font; // Changer la police
        countdownText.fontSize = fontSize; // Changer la taille
        countdownText.color = color; // Changer la couleur
    }

    IEnumerator DisplayTextDynamically(string message, TMP_FontAsset font, float fontSize, Color color, float delayBetweenWords)
    {
        // Configurer le texte
        SetCountdownTextProperties(font, fontSize, color); // Appliquer la police, la taille et la couleur
        countdownText.gameObject.SetActive(true); // Activer le texte

        // Découper la phrase en mots
        string[] words = message.Split(' ');

        // Réinitialiser le texte
        countdownText.text = "";

        // Afficher chaque mot avec un délai
        foreach (string word in words)
        {
            countdownText.text += word + " "; // Ajouter le mot au texte actuel
            yield return new WaitForSeconds(delayBetweenWords); // Attendre avant d'afficher le mot suivant
        }
    }


    IEnumerator DisplayMessageThenStartCountdown()
    {
        // Afficher le message initial dynamiquement (mot par mot)
        yield return StartCoroutine(DisplayTextDynamically(
            "Watch the following images carefully", // Message à afficher
            digitalFont, // Police Digital
            42, // Taille de la police
            Color.white, // Couleur
            0.5f // Délai entre chaque mot (en secondes)
        ));

        // Attendre un instant après que le message complet soit affiché
        yield return new WaitForSeconds(2f);

        // Masquer le texte
        countdownText.gameObject.SetActive(false);

        // Démarrer le compte à rebours
        yield return StartCoroutine(StartCountdown(25)); // Compte à rebours de 25 secondes
    }


    IEnumerator StartCountdown(int countdownTime)
    {
        // Configurer le texte pour le compte à rebours
        SetCountdownTextProperties(digitalFont, 42, countdownText.color); // Police Digital, taille 42
        countdownText.gameObject.SetActive(true);

        // Lancer le son de compte à rebours
        if (audioSource != null && countdownClip != null)
        {
            audioSource.clip = countdownClip;
            audioSource.loop = true; // Faire boucler le son si nécessaire
            audioSource.Play(); // Jouer le son
        }

        // Lancer le compte à rebours
        while (countdownTime > 0)
        {
            countdownText.text = "Time Left : " + countdownTime + " seconds";
            yield return new WaitForSeconds(1f);
            countdownTime--;
        }

        // Arrêter le son de compte à rebours après la fin
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }

        // Masquer le texte du compte à rebours une fois terminé
        countdownText.gameObject.SetActive(false);

        // Masquer les images une fois le compte à rebours terminé
        supplyChainImage.enabled = false;
        periodicTableImage.enabled = false;
        numberSequenceImage.enabled = false;
        inutile2.enabled = false;

        // Masquer les panels en même temps que les images
        foreach (GameObject panel in panels)
        {
            panel.SetActive(false); // Désactiver les panels
        }

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


    private void Update()
    {
        // Navigation avec les flèches gauche et droite
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            selectedButtonIndex = (selectedButtonIndex - 1 + answerButtons.Length) % answerButtons.Length; // Boucle vers le dernier bouton si on va à gauche
            HighlightButton(selectedButtonIndex);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            selectedButtonIndex = (selectedButtonIndex + 1) % answerButtons.Length; // Boucle vers le premier bouton si on va à droite
            HighlightButton(selectedButtonIndex);
        }

        // Validation avec Enter ou la barre d’espace
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            answerButtons[selectedButtonIndex].onClick.Invoke(); // Simule un clic sur le bouton sélectionné
        }

        //vérification dans Update pour détecter si le joueur appuie sur la barre d'espace
        if (canContinue && Input.GetKeyDown(KeyCode.Space))
        {
            OnTapToContinue(); // Appelle la méthode pour continuer
            canContinue = false; // Désactive la capacité de continuer pour éviter les doublons
        }
    }

    private void HighlightButton(int index)
    {
        // Réinitialiser l'état des boutons (désactiver la surbrillance)
        for (int i = 0; i < answerButtons.Length; i++)
        {
            ColorBlock cb = answerButtons[i].colors;
            cb.normalColor = Color.white; // Couleur par défaut
            answerButtons[i].colors = cb;
        }

        // Appliquer une couleur de surbrillance au bouton sélectionné
        ColorBlock selectedColorBlock = answerButtons[index].colors;
        selectedColorBlock.normalColor = Color.yellow; // Couleur de surbrillance
        answerButtons[index].colors = selectedColorBlock;
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
        Debug.Log("Tap to Continue activated!"); // Vérification dans la console

        tapToContinueText.gameObject.SetActive(false); // Masquer le texte "Tap to Continue"
        tapToContinueText.GetComponent<Button>().interactable = false; // Désactiver l'interaction
        canContinue = false; // Réinitialiser le flag

        // Afficher la prochaine question ou passer à l'étape suivante
        ShowQuestion(currentQuestionIndex);
    }

    void ShowQuestion(int questionIndex)
    {
        Debug.Log("ShowQuestion called for question index: " + questionIndex); // Vérifie que ShowQuestion est bien appelée

        HighlightButton(selectedButtonIndex);

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

