using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class MemoryGameController : MonoBehaviour
{
    public Image supplyChainImage; // Image de la cha�ne d'approvisionnement
    public Image periodicTableImage; // Image du tableau p�riodique
    public Image numberSequenceImage; // Nouvelle r�f�rence pour la s�quence de nombres (en tant qu'image)
    public Image inutile1; // Image inutile 1 qui doit dispara�tre
    public Image inutile2; // Image inutile 2 qui doit dispara�tre

    public TextMeshProUGUI countdownText; // R�f�rence au texte du compte � rebours
    public TextMeshProUGUI tapToContinueText; // Texte "Tap to Continue", qui sera ensuite utilis� pour afficher les questions
    public Button[] answerButtons; // Boutons pour les r�ponses
    public TMP_FontAsset bangersFont; // R�f�rence � la police "Bangers-Regular SDF" que vous utilisez

    public AudioClip countdownClip; // Clip audio pour le compte � rebours
    public AudioClip yayClip; // Clip audio pour la fin du quiz
    public AudioSource audioSource; // Composant AudioSource pour jouer les sons

    public float displayTime = 5f;
    public float questionFontSize = 40f;

    private int currentQuestionIndex = 0; // Pour suivre quelle question est affich�e
    private int correctAnswerCount = 0; // Compte le nombre de bonnes r�ponses

    // Liste des questions et des r�ponses
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
        inutile1.enabled = true; // Activer l'image inutile1 au d�but
        inutile2.enabled = true; // Activer l'image inutile2 au d�but

        // R�initialiser le score et l'index de la question
        currentQuestionIndex = 0;
        correctAnswerCount = 0;

        // Lancer le compte � rebours
        StartCoroutine(CountdownThenShowTapToContinue(25)); // Compte � rebours de 20 secondes avant de masquer les images et afficher "TAP TO CONTINUE"

        // Jouer le son de compte � rebours pendant 21 secondes
        if (audioSource != null && countdownClip != null)
        {
            audioSource.clip = countdownClip;
            audioSource.loop = true; // Boucle pour maintenir le son pendant 21 secondes
            audioSource.Play();
            Invoke("StopCountdownSound", 26f); // Arr�ter le son de compte � rebours apr�s 21 secondes
        }
    }

    private void SetTextProperties()
    {
        // D�finir les propri�t�s de texte pour garantir la consistance
        tapToContinueText.font = bangersFont; // Assure-toi que la police est bien d�finie � "Bangers-Regular SDF"
        tapToContinueText.fontSize = 36; // D�finir la taille du texte pour "TAP TO CONTINUE"
        tapToContinueText.color = Color.white; // D�finir la couleur du texte (par exemple, blanc)
        tapToContinueText.fontStyle = FontStyles.Normal; // Peut �tre Normal, Bold, Italic, etc.
    }

    IEnumerator CountdownThenShowTapToContinue(int countdownTime)
    {
        // Activer le texte du compte � rebours
        countdownText.gameObject.SetActive(true);

        // Lancer le compte � rebours
        while (countdownTime > 0)
        {
            countdownText.text = "Time remaining: " + countdownTime + " seconds";
            yield return new WaitForSeconds(1f);
            countdownTime--;
        }

        // D�sactiver le texte du compte � rebours une fois termin�
        countdownText.gameObject.SetActive(false);

        // Masquer les images une fois le compte � rebours termin�
        supplyChainImage.enabled = false;
        periodicTableImage.enabled = false;
        numberSequenceImage.enabled = false;
        inutile1.enabled = false; // Masquer l'image inutile1 apr�s le compte � rebours
        inutile2.enabled = false; // Masquer l'image inutile2 apr�s le compte � rebours

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

    void StopCountdownSound()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    void OnTapToContinue()
    {
        Debug.Log("Tap to Continue clicked!"); // V�rifie que cette m�thode est bien appel�e

        // D�sactiver l'interaction apr�s l'appui
        tapToContinueText.GetComponent<Button>().interactable = false;

        // Utiliser `tapToContinueText` pour afficher la question suivante
        ShowQuestion(currentQuestionIndex);
    }

    void ShowQuestion(int questionIndex)
    {
        Debug.Log("ShowQuestion called for question index: " + questionIndex); // V�rifie que ShowQuestion est bien appel�e

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

