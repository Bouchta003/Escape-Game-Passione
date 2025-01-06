using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class MemoryGameController : MonoBehaviour
{
    public Image supplyChainImage; // Image de la cha�ne d'approvisionnement
    public Image periodicTableImage; // Image du tableau p�riodique
    public Image numberSequenceImage; // Nouvelle r�f�rence pour la s�quence de nombres (en tant qu'image)
    //public Image inutile1; // Image inutile 1 qui doit dispara�tre
    public Image inutile2; // Image inutile 2 qui doit dispara�tre

    public GameObject[] panels; // Un tableau pour contenir vos panels


    public TextMeshProUGUI countdownText; // R�f�rence au texte du compte � rebours
    public TextMeshProUGUI tapToContinueText; // Texte "Tap to Continue", qui sera ensuite utilis� pour afficher les questions
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
        "What were the height and width of the battery shown?",
        "In which stage are the cell manufacturers within the supply chain?",
        "What is the atomic number of Nickel?"
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

    void StartGame()
    {
        // Initialiser les propri�t�s de texte pour la police, la taille, et la couleur
        SetTextProperties();

        // Initialiser l'�tat des �l�ments UI
        tapToContinueText.gameObject.SetActive(false); // D�sactiver le texte "Tap to Continue" au d�but
        canContinue = true; // Permettre au joueur de continuer avec la barre d'espace
        tapToContinueText.GetComponent<Button>().interactable = false; // D�sactiver l'interaction

        // D�sactiver les boutons de r�ponse au d�but
        foreach (Button btn in answerButtons)
        {
            btn.gameObject.SetActive(false);
        }

        // Activer les images pour commencer
        supplyChainImage.enabled = true;
        periodicTableImage.enabled = true;
        numberSequenceImage.enabled = true;
        inutile2.enabled = true; // Activer l'image inutile2 au d�but

        // R�initialiser le score et l'index de la question
        currentQuestionIndex = 0;
        correctAnswerCount = 0;

        // Afficher un message initial avant le compte � rebours
        StartCoroutine(DisplayMessageThenStartCountdown());
    }

    private void SetTextProperties()
    {
        // D�finir les propri�t�s de texte pour garantir la consistance
        tapToContinueText.font = bangersFont; // Assure-toi que la police est bien d�finie � "Bangers-Regular SDF"
        tapToContinueText.fontSize = 36; // D�finir la taille du texte pour "TAP TO CONTINUE"
        tapToContinueText.color = Color.white; // D�finir la couleur du texte (par exemple, blanc)
        tapToContinueText.fontStyle = FontStyles.Normal; // Peut �tre Normal, Bold, Italic, etc.
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

        // D�couper la phrase en mots
        string[] words = message.Split(' ');

        // R�initialiser le texte
        countdownText.text = "";

        // Afficher chaque mot avec un d�lai
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
            "Watch the following images carefully", // Message � afficher
            digitalFont, // Police Digital
            42, // Taille de la police
            Color.white, // Couleur
            0.5f // D�lai entre chaque mot (en secondes)
        ));

        // Attendre un instant apr�s que le message complet soit affich�
        yield return new WaitForSeconds(2f);

        // Masquer le texte
        countdownText.gameObject.SetActive(false);

        // D�marrer le compte � rebours
        yield return StartCoroutine(StartCountdown(25)); // Compte � rebours de 25 secondes
    }


    IEnumerator StartCountdown(int countdownTime)
    {
        // Configurer le texte pour le compte � rebours
        SetCountdownTextProperties(digitalFont, 42, countdownText.color); // Police Digital, taille 42
        countdownText.gameObject.SetActive(true);

        // Lancer le son de compte � rebours
        if (audioSource != null && countdownClip != null)
        {
            audioSource.clip = countdownClip;
            audioSource.loop = true; // Faire boucler le son si n�cessaire
            audioSource.Play(); // Jouer le son
        }

        // Lancer le compte � rebours
        while (countdownTime > 0)
        {
            countdownText.text = "Time Left : " + countdownTime + " seconds";
            yield return new WaitForSeconds(1f);
            countdownTime--;
        }

        // Arr�ter le son de compte � rebours apr�s la fin
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }

        // Masquer le texte du compte � rebours une fois termin�
        countdownText.gameObject.SetActive(false);

        // Masquer les images une fois le compte � rebours termin�
        supplyChainImage.enabled = false;
        periodicTableImage.enabled = false;
        numberSequenceImage.enabled = false;
        inutile2.enabled = false;

        // Masquer les panels en m�me temps que les images
        foreach (GameObject panel in panels)
        {
            panel.SetActive(false); // D�sactiver les panels
        }

        // Activer le texte "TAP TO CONTINUE" apr�s que les images ont disparu
        tapToContinueText.gameObject.SetActive(true);
        tapToContinueText.text = "TAP TO CONTINUE";
        tapToContinueText.font = bangersFont; // Red�finir la police pour s'assurer que c'est bien Bangers
        tapToContinueText.GetComponent<Button>().interactable = true; // Rendre le texte cliquable

        // Ajouter l'�v�nement pour d�clencher OnTapToContinue lorsque l'utilisateur clique sur le texte
        Button tapButton = tapToContinueText.GetComponent<Button>();
        if (tapButton != null)
        {
            tapButton.onClick.RemoveAllListeners(); // Supprimer les anciens listeners s'il y en a
            tapButton.onClick.AddListener(OnTapToContinue);
        }
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

        //v�rification dans Update pour d�tecter si le joueur appuie sur la barre d'espace
        if (canContinue && Input.GetKeyDown(KeyCode.Space))
        {
            OnTapToContinue(); // Appelle la m�thode pour continuer
            canContinue = false; // D�sactive la capacit� de continuer pour �viter les doublons
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

    void OnTapToContinue()
    {
        Debug.Log("Tap to Continue activated!"); // V�rification dans la console

        tapToContinueText.gameObject.SetActive(false); // Masquer le texte "Tap to Continue"
        tapToContinueText.GetComponent<Button>().interactable = false; // D�sactiver l'interaction
        canContinue = false; // R�initialiser le flag

        // Afficher la prochaine question ou passer � l'�tape suivante
        ShowQuestion(currentQuestionIndex);
    }

    void ShowQuestion(int questionIndex)
    {
        Debug.Log("ShowQuestion called for question index: " + questionIndex); // V�rifie que ShowQuestion est bien appel�e

        HighlightButton(selectedButtonIndex);

        tapToContinueText.gameObject.SetActive(true);
        tapToContinueText.text = questions[questionIndex];
        tapToContinueText.font = bangersFont; // Red�finir la police pour s'assurer qu'elle est correcte
        tapToContinueText.fontSize = questionFontSize; // D�finir la taille de la police pour les questions
        tapToContinueText.color = new Color(1f, 1f, 1f, 1f); // Assurer l'opacit� totale du texte

        // Configurer les boutons de r�ponse pour la question actuelle
        for (int i = 0; i < answerButtons.Length; i++)
        {
            if (i < answers[questionIndex].Length)
            {
                Debug.Log("Configuring answer button: " + i); // V�rifie que les boutons de r�ponse sont bien configur�s
                answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = answers[questionIndex][i];
                answerButtons[i].gameObject.SetActive(true);
                int answerIndex = i; // Pour �viter un probl�me de r�f�rence dans la closure
                answerButtons[i].onClick.RemoveAllListeners(); // Supprime les anciens listeners
                answerButtons[i].onClick.AddListener(() => CheckAnswer(answerIndex == correctAnswers[questionIndex]));
            }
        }
    }

    void CheckAnswer(bool isCorrect)
    {
        Debug.Log("CheckAnswer called - isCorrect: " + isCorrect); // V�rifie que cette m�thode est bien appel�e

        if (isCorrect)
        {
            Debug.Log("Correct Answer!");
            correctAnswerCount++; // Incr�menter le nombre de bonnes r�ponses
            tapToContinueText.color = Color.green; // Feedback color pour une bonne r�ponse
        }
        else
        {
            Debug.Log("Wrong Answer!");
            tapToContinueText.color = Color.red; // Feedback color pour une mauvaise r�ponse
        }

        StartCoroutine(NextQuestion());
    }

    IEnumerator NextQuestion()
    {
        yield return new WaitForSeconds(2f); // Attendre 2 secondes avant de passer � la prochaine question

        // Passer � la question suivante
        currentQuestionIndex++;

        if (currentQuestionIndex < questions.Length)
        {
            tapToContinueText.color = Color.white; // Remettre la couleur du texte � blanc pour la prochaine question
            ShowQuestion(currentQuestionIndex);
        }
        else
        {
            // Toutes les questions ont �t� pos�es
            DisplayFinalScore(); // Afficher le score final
        }
    }

    void DisplayFinalScore()
    {
        // D�sactiver les boutons de r�ponse apr�s la fin du quiz
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
            StartCoroutine(RestartGameAfterDelay(3f)); // Relancer le jeu apr�s 3 secondes
        }

        tapToContinueText.fontSize = 50; // Augmenter la taille de la police pour le score final, si n�cessaire
        tapToContinueText.color = Color.yellow; // Mettre la couleur du texte en jaune pour le score final

        // Jouer le son de f�licitations � la fin du quiz pendant 5 secondes
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

