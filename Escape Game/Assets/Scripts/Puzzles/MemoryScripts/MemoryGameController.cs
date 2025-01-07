using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using TMPro;



public class MemoryGameController : MonoBehaviour

{
    public UnityEvent onPuzzleComplete; // pour lancer puzzle Ali

    public Image supplyChainImage; // Image de la cha�ne d'approvisionnement
    public Image periodicTableImage; // Image du tableau p�riodique
    public Image numberSequenceImage; // Nouvelle r�f�rence pour la s�quence de nombres (en tant qu'image)
    public Image inutile2; // Image inutile 2 qui doit dispara�tre

    public GameObject[] panels; // Un tableau pour contenir vos panels


    public TextMeshProUGUI countdownText; // R�f�rence au texte du compte � rebours
    public TextMeshProUGUI tapToContinueText; // Texte "Tap to Continue", qui sera ensuite utilis� pour afficher les questions
    public TextMeshProUGUI questionText; // Texte d�di� aux questions
    public TextMeshProUGUI instructionText; // Texte Instructions
    public Button[] answerButtons; // Boutons pour les r�ponses
    public TMP_FontAsset bangersFont; // R�f�rence � la police "Bangers-Regular SDF" que vous utilisez
    public TMP_FontAsset digitalFont; // Police pour le compte � rebours


    public AudioClip countdownClip; // Clip audio pour le compte � rebours
    public AudioClip yayClip; // Clip audio pour la fin du quiz
    public AudioSource audioSource; // Composant AudioSource pour jouer les sons

    public float displayTime = 5f;
    public float questionFontSize = 40f;

    private int currentQuestionIndex = 0; // Pour suivre quelle question est affich�e
    private int correctAnswerCount = 0; // Compte le nombre de bonnes r�ponses
    private int selectedButtonIndex = 0; // Index du bouton actuellement s�lectionn�
    private bool canContinue = false; // Indique si le joueur peut continuer



    // Liste des questions et des r�ponses
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

    private int[] correctAnswers = { 0, 2, 1 }; // Index des bonnes r�ponses dans chaque question

    private void Start()
    {
        StartGame(); // D�marre le jeu au d�but
    }

    private void StartGame()
    {
        // Initialiser les propri�t�s de texte pour la police, la taille, et la couleur
        SetTextProperties();

        // R�initialiser l'�tat des panels
        foreach (GameObject panel in panels)
        {
            panel.SetActive(false); // Panels d�sactiv�s
            CanvasGroup canvasGroup = panel.GetComponent<CanvasGroup>();
            if (canvasGroup != null)
            {
                canvasGroup.alpha = 1f; // R�initialiser l'opacit�
            }
        }

        // R�initialiser l'�tat des images
        supplyChainImage.enabled = false;
        supplyChainImage.color = new Color(1f, 1f, 1f, 1f); // Blanc opaque

        periodicTableImage.enabled = false;
        periodicTableImage.color = new Color(1f, 1f, 1f, 1f);

        numberSequenceImage.enabled = false;
        numberSequenceImage.color = new Color(1f, 1f, 1f, 1f);

        inutile2.enabled = false;
        inutile2.color = new Color(1f, 1f, 1f, 1f);

        // R�initialiser les boutons de r�ponse
        foreach (Button btn in answerButtons)
        {
            btn.gameObject.SetActive(false);
            ColorBlock cb = btn.colors;
            cb.normalColor = Color.white; // R�initialiser � la couleur par d�faut
            btn.colors = cb;
        }

        // R�initialiser les textes
        tapToContinueText.gameObject.SetActive(false);
        tapToContinueText.text = "TAP TO CONTINUE";
        tapToContinueText.fontSize = 36;
        tapToContinueText.color = Color.white;
        tapToContinueText.GetComponent<Button>().interactable = false;

        questionText.gameObject.SetActive(false);
        questionText.text = ""; // Vider le texte des questions
        questionText.fontSize = questionFontSize;
        questionText.color = Color.white;

        countdownText.gameObject.SetActive(false); // Masquer le texte du compte � rebours

        // R�initialiser le score et l'index de la question
        currentQuestionIndex = 0;
        correctAnswerCount = 0;

        // Afficher les instructions
        StartCoroutine(DisplayInstructions());
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
        instructionText.text = welcomeText; // Affecter le texte d'introduction
        yield return StartCoroutine(FadeInText(instructionText, 1f)); // Appliquer le fade-in
        yield return new WaitForSeconds(2f); // Attendre un moment

        instructionText.text = instructions; // Affecter les instructions
        yield return new WaitForSeconds(5f);

        yield return StartCoroutine(FadeOutText(1f, instructionText)); // Appliquer le fade-out
        instructionText.gameObject.SetActive(false); // D�sactiver le texte apr�s le fade-out

        // D�marrer l'affichage des images et le compte � rebours
        StartCoroutine(DisplayImagesAndCountdown(25));

    }

    IEnumerator FadeInText(TextMeshProUGUI textElement, float fadeDuration)
    {
        Color originalColor = textElement.color;
        float elapsedTime = 0f;

        // R�initialiser la transparence � 0
        textElement.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);

        // Faire appara�tre le texte progressivement
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            textElement.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        // Assurer que l'alpha est � 1
        textElement.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1f);
    }



    IEnumerator DisplayImagesAndCountdown(int countdownTime)
    {
        // Activer les images et panels
        foreach (GameObject panel in panels)
        {
            panel.SetActive(true); // Les panels sont maintenant correctement r�activ�s
        }
        supplyChainImage.enabled = true;
        periodicTableImage.enabled = true;
        numberSequenceImage.enabled = true;
        inutile2.enabled = true;

        // Attendre un instant avant d'afficher le compte � rebours
        yield return new WaitForSeconds(0.5f);

        // Configurer et afficher le texte du compte � rebours
        countdownText.gameObject.SetActive(true);

        // Lancer le son de compte � rebours
        if (audioSource != null && countdownClip != null)
        {
            audioSource.clip = countdownClip;
            audioSource.loop = true;
            audioSource.Play();
        }

        // Lancer le compte � rebours
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

        // Rendre le texte r�actif � la barre d'espace
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

        // Assurez-vous que l'image est r�activ�e avec la pleine opacit�
        image.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1f);
        image.enabled = false; // Laisser d�sactiv�, mais pr�t � �tre r�activ�
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

        // Assurez-vous que le panel n'est pas d�sactiv� apr�s le fade-out
        canvasGroup.alpha = 0f;
        panel.SetActive(false);
    }


    private void OnTapToContinue()
    {
        Debug.Log("Tap to Continue activated!");

        // Masquer le texte "Tap to Continue"
        tapToContinueText.gameObject.SetActive(false);
        tapToContinueText.GetComponent<Button>().interactable = false;

        // R�initialiser le flag
        canContinue = false;

        // Afficher la premi�re question
        ShowQuestion(currentQuestionIndex);
    }




    private void Update()
    {
        // Navigation avec les fl�ches gauche et droite
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            selectedButtonIndex = (selectedButtonIndex - 1 + answerButtons.Length) % answerButtons.Length; // Boucle vers le dernier bouton si on va � gauche
            HighlightButton(selectedButtonIndex);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            selectedButtonIndex = (selectedButtonIndex + 1) % answerButtons.Length; // Boucle vers le premier bouton si on va � droite
            HighlightButton(selectedButtonIndex);
        }

        // Validation avec Enter ou la barre d�espace
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            answerButtons[selectedButtonIndex].onClick.Invoke(); // Simule un clic sur le bouton s�lectionn�
        }

        // V�rifier si l'utilisateur appuie sur la barre d'espace pour continuer
        if (canContinue && Input.GetKeyDown(KeyCode.Space))
        {
            OnTapToContinue();
            canContinue = false; // D�sactiver le flag pour �viter les r�p�titions
        }
    }

    private void HighlightButton(int index)
    {
        // R�initialiser l'�tat des boutons (d�sactiver la surbrillance)
        for (int i = 0; i < answerButtons.Length; i++)
        {
            ColorBlock cb = answerButtons[i].colors;
            cb.normalColor = Color.white; // Couleur par d�faut
            answerButtons[i].colors = cb;
        }

        // Appliquer une couleur de surbrillance au bouton s�lectionn�
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

        // Afficher la question dans le texte d�di�
        questionText.gameObject.SetActive(true);
        questionText.text = questions[questionIndex];
        questionText.font = bangersFont;
        questionText.fontSize = questionFontSize;
        questionText.color = new Color(1f, 1f, 1f, 1f);

        // Configurer les boutons de r�ponse pour la question actuelle
        for (int i = 0; i < answerButtons.Length; i++)
        {
            if (i < answers[questionIndex].Length)
            {
                Debug.Log("Configuring answer button: " + i);
                answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = answers[questionIndex][i];
                answerButtons[i].gameObject.SetActive(true);
                int answerIndex = i; // Pour �viter un probl�me de r�f�rence dans la closure
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

        // Passer � la question suivante
        StartCoroutine(NextQuestion());
    }

    IEnumerator NextQuestion()
    {
        yield return new WaitForSeconds(2f); // Attendre 2 secondes avant de passer � la prochaine question

        // Passer � la question suivante
        currentQuestionIndex++;

        if (currentQuestionIndex < questions.Length)
        {
            // Remettre la couleur du texte � blanc pour la prochaine question
            questionText.color = new Color(1f, 1f, 1f, 1f);
            ShowQuestion(currentQuestionIndex);
        }
        else
        {
            // Toutes les questions ont �t� pos�es
            DisplayFinalScore();
        }
    }



    void DisplayFinalScore()
    {
        // D�sactiver les boutons de r�ponse apr�s la fin du quiz
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

            // Jouer le son de f�licitations
            if (audioSource != null && yayClip != null)
            {
                audioSource.clip = yayClip;
                audioSource.Play();
                Invoke("StopYaySound", 5f);
            }

            // Appeler l'�v�nement pour lancer le puzzle suivant
            onPuzzleComplete.Invoke(); //Appel Puzzle Ali
        }
        else
        {
            // Si le score est inf�rieur � 3/3, red�marrer le jeu apr�s 3 secondes
            questionText.text = "Your Score: " + correctAnswerCount + "/" + questions.Length + "\nTry Again!";
            StartCoroutine(RestartGameAfterDelay(3f));
        }
    }



    IEnumerator RestartGameAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Debug.Log("Restarting Game...");

        // R�activer les panels et les images pour le nouveau jeu
        foreach (GameObject panel in panels)
        {
            panel.SetActive(true); // Panels r�activ�s
            CanvasGroup canvasGroup = panel.GetComponent<CanvasGroup>();
            if (canvasGroup != null)
            {
                canvasGroup.alpha = 1f; // Assurer la pleine opacit�
            }
        }
        supplyChainImage.enabled = true;
        periodicTableImage.enabled = true;
        numberSequenceImage.enabled = true;
        inutile2.enabled = true;

        // R�initialiser toutes les variables
        currentQuestionIndex = 0;
        correctAnswerCount = 0;

        // Masquer les textes
        questionText.gameObject.SetActive(false);
        tapToContinueText.gameObject.SetActive(false);

        // Relancer le jeu
        StartGame();
    }
}



