using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using TMPro;



public class MemoryGameController : MonoBehaviour

{
    //public UnityEvent onPuzzleComplete;  pour lancer puzzle Ali

    public Image supplyChainImage; // Image de la chaîne d'approvisionnement
    public Image periodicTableImage; // Image du tableau périodique
    public Image numberSequenceImage; // Nouvelle référence pour la séquence de nombres (en tant qu'image)
    public Image inutile2; // Image inutile 2 qui doit disparaître

    public GameObject[] panels; // Un tableau pour contenir vos panels


    public TextMeshProUGUI countdownText; // Référence au texte du compte à rebours
    public TextMeshProUGUI tapToContinueText; // Texte "Tap to Continue", qui sera ensuite utilisé pour afficher les questions
    public TextMeshProUGUI questionText; // Texte dédié aux questions
    public TextMeshProUGUI instructionText; // Texte Instructions
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
    private bool isRestarting = false; // Indique si le jeu est en cours de redémarrage


    public GameObject backgroundPanel; // Référence au BackgroundPanel





    // Liste des questions et des réponses
    private string[] questions = {
        "what were the height and width of the battery shown?",
        "in which stage are the cell manufacturers within the supply chain?",
        "what is the atomic number of Nickel?"
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

    private void StartGame()
    {
        // Initialiser les propriétés de texte pour la police, la taille, et la couleur
        SetTextProperties();

        // Réinitialiser l'état des panels
        foreach (GameObject panel in panels)
        {
            panel.SetActive(false); // Panels désactivés
            CanvasGroup canvasGroup = panel.GetComponent<CanvasGroup>();
            if (canvasGroup != null)
            {
                canvasGroup.alpha = 1f; // Réinitialiser l'opacité
            }
        }

        // Réactiver le BackgroundPanel
        if (backgroundPanel != null)
        {
            backgroundPanel.SetActive(true);
            Debug.Log("BackgroundPanel réactivé.");
        }

        // Réinitialiser l'état des images
        supplyChainImage.enabled = false;
        periodicTableImage.enabled = false;
        numberSequenceImage.enabled = false;
        inutile2.enabled = false;

        // Réinitialiser les boutons de réponse
        foreach (Button btn in answerButtons)
        {
            btn.gameObject.SetActive(false);
            ColorBlock cb = btn.colors;
            cb.normalColor = Color.white; // Réinitialiser à la couleur par défaut
            btn.colors = cb;
        }

        // Réinitialiser les textes
        tapToContinueText.gameObject.SetActive(false);
        tapToContinueText.text = "TAP TO CONTINUE";
        tapToContinueText.fontSize = 36;
        tapToContinueText.color = Color.white;
        tapToContinueText.GetComponent<Button>().interactable = false;

        questionText.gameObject.SetActive(false);
        questionText.text = ""; // Vider le texte des questions
        questionText.fontSize = questionFontSize;
        questionText.color = Color.white;

        countdownText.gameObject.SetActive(false); // Masquer le texte du compte à rebours

        // Réinitialiser le score et l'index de la question
        currentQuestionIndex = 0;
        correctAnswerCount = 0;

        // Afficher les instructions
        StartCoroutine(DisplayInstructions());
    }


    IEnumerator StartWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Afficher la première question
        ShowQuestion(currentQuestionIndex);
    }



    private void SetTextProperties()
    {
        tapToContinueText.font = bangersFont;
        tapToContinueText.fontSize = 36;
        tapToContinueText.color = Color.white;
        tapToContinueText.fontStyle = FontStyles.Normal;
    }

    IEnumerator DisplayInstructions()
    {
        // Activer le texte d'instructions
        instructionText.gameObject.SetActive(true);
        instructionText.fontSize = 36;
        instructionText.color = Color.white;
        instructionText.alignment = TextAlignmentOptions.Center;

        RectTransform rectTransform = instructionText.GetComponent<RectTransform>();
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(1, 1);
        rectTransform.offsetMin = Vector2.zero;
        rectTransform.offsetMax = Vector2.zero;

        string welcomeText = "How great is your memory ?";
        string instructions = "\n\nInstructions:\n" +
                              "1. Observe the following images carefully.\n" +
                              "2. Answer the questions based on what you see.\n" +
                              "3. Use arrow keys to navigate and spacebar to confirm.\n" +
                              "\nGood luck!";

        // Appliquer l'effet de fade-in pour le texte "Welcome"
        instructionText.fontSize = 44; // Taille souhaitée
        instructionText.text = welcomeText; // Affecter le texte d'introduction
        yield return StartCoroutine(FadeInText(instructionText, 1f)); // Appliquer le fade-in
        yield return new WaitForSeconds(2f); // Attendre un moment

        instructionText.text = instructions; // Affecter les instructions
        yield return new WaitForSeconds(5f);

        yield return StartCoroutine(FadeOutText(1f, instructionText)); // Appliquer le fade-out
        instructionText.gameObject.SetActive(false); // Désactiver le texte après le fade-out

        // Lancer les images et le compte à rebours (25 secondes)
        yield return StartCoroutine(DisplayImagesAndCountdown(25));

        // Afficher "Tap to Continue" après le compte à rebours
        ///ShowTapToContinue();
    }




    IEnumerator FadeInText(TextMeshProUGUI textElement, float fadeDuration)
    {
        Color originalColor = textElement.color;
        float elapsedTime = 0f;

        // Réinitialiser la transparence à 0
        textElement.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);

        // Faire apparaître le texte progressivement
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            textElement.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        // Assurer que l'alpha est à 1
        textElement.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1f);
    }



    IEnumerator DisplayImagesAndCountdown(int countdownTime)
    {

        // Ne rien faire si le puzzle est déjà terminé
        if (WallCode.Success)
        {
            yield break; // Arrêter l'exécution
        }

        // Activer les images et panels
        foreach (GameObject panel in panels)
        {
            panel.SetActive(true); // Les panels sont maintenant correctement réactivés
        }
        supplyChainImage.enabled = true;
        periodicTableImage.enabled = true;
        numberSequenceImage.enabled = true;
        inutile2.enabled = true;

        // Attendre un instant avant d'afficher le compte à rebours
        yield return new WaitForSeconds(0.5f);

        // Configurer et afficher le texte du compte à rebours
        countdownText.gameObject.SetActive(true);

        // Lancer le son de compte à rebours
        if (audioSource != null && countdownClip != null)
        {
            audioSource.clip = countdownClip;
            audioSource.loop = true;
            audioSource.Play();
        }

        // Lancer le compte à rebours
        while (countdownTime > 0)
        {
            countdownText.text = "Time Left : " + countdownTime + " seconds";
            yield return new WaitForSeconds(1f);
            countdownTime--;
        }

        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }

        countdownText.gameObject.SetActive(false);

        // Appliquer un fade-out aux panels et images
        foreach (GameObject panel in panels)
        {
            StartCoroutine(FadeOutPanel(panel, 1f));
        }
        StartCoroutine(FadeOutImage(supplyChainImage, 1f));
        StartCoroutine(FadeOutImage(periodicTableImage, 1f));
        StartCoroutine(FadeOutImage(numberSequenceImage, 1f));
        StartCoroutine(FadeOutImage(inutile2, 1f));

        yield return new WaitForSeconds(1f);

        // Afficher "TAP TO CONTINUE"
        tapToContinueText.gameObject.SetActive(true);
        tapToContinueText.GetComponent<Button>().interactable = true;

        // Rendre le texte réactif à la barre d'espace
        canContinue = true;
    }



    IEnumerator FadeOutText(float fadeDuration, TextMeshProUGUI textElement)
    {
        Color originalColor = textElement.color;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            textElement.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        textElement.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
    }

    IEnumerator FadeOutImage(Image image, float fadeDuration)
    {
        Color originalColor = image.color;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            image.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        // Assurez-vous que l'image est réactivée avec la pleine opacité
        image.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1f);
        image.enabled = false; // Laisser désactivé, mais prêt à être réactivé
    }


    IEnumerator FadeOutPanel(GameObject panel, float fadeDuration)
    {
        CanvasGroup canvasGroup = panel.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = panel.AddComponent<CanvasGroup>();
        }

        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            yield return null;
        }

        // Assurez-vous que le panel n'est pas désactivé après le fade-out
        canvasGroup.alpha = 0f;
        panel.SetActive(false);
    }


    private void OnTapToContinue()
    {
        Debug.Log("Tap to Continue activated!");

        // Masquer le texte "Tap to Continue"
        tapToContinueText.gameObject.SetActive(false);
        tapToContinueText.GetComponent<Button>().interactable = false;

        // Réinitialiser le flag
        canContinue = false;

        // Afficher la première question
        ShowQuestion(currentQuestionIndex);
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
            // Si "Tap to Continue" est actif, masquer et continuer
            if (canContinue)
            {
                OnTapToContinue();
                return;
            }

            // Sinon, simuler un clic sur le bouton sélectionné
            answerButtons[selectedButtonIndex].onClick.Invoke();
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


    void ShowQuestion(int questionIndex)
    {
        Debug.Log("ShowQuestion called for question index: " + questionIndex);

        HighlightButton(selectedButtonIndex);

        // Afficher la question dans le texte dédié
        questionText.gameObject.SetActive(true);
        questionText.text = questions[questionIndex];
        questionText.font = bangersFont;
        questionText.fontSize = questionFontSize;
        questionText.color = new Color(1f, 1f, 1f, 1f);

        // Configurer les boutons de réponse pour la question actuelle
        for (int i = 0; i < answerButtons.Length; i++)
        {
            if (i < answers[questionIndex].Length)
            {
                Debug.Log("Configuring answer button: " + i);
                answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = answers[questionIndex][i];
                answerButtons[i].gameObject.SetActive(true);
                int answerIndex = i; // Pour éviter un problème de référence dans la closure
                answerButtons[i].onClick.RemoveAllListeners();
                answerButtons[i].onClick.AddListener(() => CheckAnswer(answerIndex == correctAnswers[questionIndex]));
            }
        }
    }


    void CheckAnswer(bool isCorrect)
    {
        Debug.Log("CheckAnswer called - isCorrect: " + isCorrect);

        if (isCorrect)
        {
            Debug.Log("Correct Answer!");
            correctAnswerCount++;

            // Changer la couleur en vert transparent pour questionText
            questionText.color = new Color(0f, 1f, 0f, 0.5f);
        }
        else
        {
            Debug.Log("Wrong Answer!");

            // Changer la couleur en rouge transparent pour questionText
            questionText.color = new Color(1f, 0f, 0f, 0.5f);
        }

        // Assurer que la couleur est visible
        questionText.enabled = false;
        questionText.enabled = true;

        // Passer à la question suivante
        StartCoroutine(NextQuestion());
    }

    IEnumerator NextQuestion()
    {
        yield return new WaitForSeconds(2f); // Attendre 2 secondes avant de passer à la prochaine question

        // Passer à la question suivante
        currentQuestionIndex++;

        if (currentQuestionIndex < questions.Length)
        {
            // Remettre la couleur du texte à blanc pour la prochaine question
            questionText.color = new Color(1f, 1f, 1f, 1f);

            // Afficher la prochaine question
            ShowQuestion(currentQuestionIndex);
        }
        else
        {
            // Toutes les questions ont été posées, afficher le score final
            DisplayFinalScore();
        }
    }


    private void DisableAllPanels()
    {
        foreach (GameObject panel in panels)
        {
            CanvasGroup canvasGroup = panel.GetComponent<CanvasGroup>();
            if (canvasGroup != null)
            {
                canvasGroup.alpha = 0f; // Masquer avec opacité
            }
            panel.SetActive(false); // Désactiver complètement le panel
        }
    }

    private void StopAudio()
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }


   

    void DisplayFinalScore()
    {
        // Désactiver les boutons de réponse après la fin du quiz
        foreach (Button btn in answerButtons)
        {
            btn.gameObject.SetActive(false);
        }

        // Afficher le texte pour le score final dans questionText
        questionText.gameObject.SetActive(true);
        questionText.fontSize = 50;
        questionText.color = new Color(1f, 1f, 0f, 0.5f); // Jaune transparent

        if (correctAnswerCount == questions.Length)
        {
            questionText.text = correctAnswerCount + "/" + questions.Length + " - You have unlocked the puzzle, here is the code!";

            if (audioSource != null && yayClip != null)
            {
                audioSource.clip = yayClip;
                audioSource.Play();
                Invoke("StopAudio", 3f); // Arrêter le son après 3 secondes

            }
            tapToContinueText.text = correctAnswerCount + "/" + questions.Length + " - You have unlocked the puzzle, here is the code!";
            WallCode.Success = true;

            // Désactiver immédiatement le BackgroundPanel
            if (backgroundPanel != null)
            {
                backgroundPanel.SetActive(false);
                Debug.Log("BackgroundPanel désactivé car le joueur a terminé.");
            }

            // Désactiver le canvas ou les éléments spécifiques après un délai
            StartCoroutine(HideCanvasAfterDelay(3f));

            // Désactiver tous les panels
            DisableAllPanels();
        }
        else
        {
            // Si le score est inférieur à 3/3, redémarrer le jeu après 3 secondes
            questionText.text = "Your Score: " + correctAnswerCount + "/" + questions.Length + "\nTry Again!";
            StartCoroutine(RestartGameAfterDelay(3f));
        }
    }

    // Coroutine pour désactiver le canvas après un délai
    IEnumerator HideCanvasAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Désactiver les éléments (canvas, texte, etc.)
        questionText.gameObject.SetActive(false);
        tapToContinueText.gameObject.SetActive(false);

        // Désactiver les panels
        DisableAllPanels();

        // Désactiver le Canvas principal
        if (backgroundPanel != null)
        {
            backgroundPanel.SetActive(false);
            Debug.Log("BackgroundPanel désactivé.");
        }

        // Arrêter le son
        StopAudio();

        // Ajoutez d'autres éléments à désactiver si nécessaire
    }



    IEnumerator RestartGameAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Debug.Log("Restarting Game...");

        // Activer le flag de redémarrage
        isRestarting = true;

        // Réinitialiser l'état du puzzle avant de redémarrer
        WallCode.Success = false; // Réinitialiser le succès du puzzle
        Debug.Log("WallCode.Success réinitialisé à false.");

        // Réactiver le BackgroundPanel
        if (backgroundPanel != null)
        {
            backgroundPanel.SetActive(true);
            Debug.Log("BackgroundPanel réactivé pour le redémarrage.");
        }


        // Réinitialiser les boutons de réponse
        foreach (Button btn in answerButtons)
        {
            btn.gameObject.SetActive(false);
            btn.GetComponentInChildren<TextMeshProUGUI>().text = ""; // Effacer le texte des boutons
            btn.onClick.RemoveAllListeners(); // Supprimer les anciens listeners pour éviter les doublons
        }

        // Réinitialiser toutes les variables du jeu
        currentQuestionIndex = 0; // Réinitialiser l'indice des questions
        correctAnswerCount = 0; // Réinitialiser le score

        // Masquer les textes
        questionText.gameObject.SetActive(false);
        tapToContinueText.gameObject.SetActive(false);

        // Ajouter un délai avant de relancer le jeu
        yield return new WaitForSeconds(1f);

        // Désactiver le flag de redémarrage
        isRestarting = false;

        // Relancer le jeu
        StartGame();
    }

}