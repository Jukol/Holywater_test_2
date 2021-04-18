using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Animator _bunnyAnimator, _questionBubbleAnimator;
    [SerializeField] private GameObject _questionBubble, _answerPrefab, _answersParent;
    [SerializeField] private GameObject _startPanel, _mainPanel, _finalPanel, _thankYouPanel;

    [SerializeField] private JsonReader _jsonReader;
    
    [SerializeField] private DataProcessor _dataProcessor;

    private WaitForSeconds _waitBetweenAnimations;
    private float _secondsBetweenAnimations = 0.3f;

    private int _counter = 0;

    private void Awake()
    {
        _questionBubble.gameObject.SetActive(false);
        _waitBetweenAnimations = new WaitForSeconds(_secondsBetweenAnimations);
    }

    private void Start()
    {
        _startPanel.SetActive(true);
        _mainPanel.SetActive(false);
        _finalPanel.SetActive(false);
        _thankYouPanel.SetActive(false);
    }

    public void StartApp()
    {
        StartCoroutine(AnimationSequence());
    }

    private IEnumerator AnimationSequence()
    {
        _bunnyAnimator.SetTrigger("StartPlaying");
        yield return _waitBetweenAnimations;

        _questionBubble.gameObject.SetActive(true);

        QuestionInit(_counter);

        yield return _waitBetweenAnimations;

        AnswerSelectionInit(_counter);
    }

    private void QuestionInit(int iteration)
    {
        _questionBubble.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = _dataProcessor.inputQuestionTexts[iteration];
        _questionBubbleAnimator.SetTrigger("StartPlaying");
    }

    private void AnswerSelectionInit(int iteration)
    {
        for (int i = 0; i < _jsonReader.ReadFromJson().listOfQuestions[iteration].answers.Length; i++)
        {
            GameObject answer = Instantiate(_answerPrefab, _answersParent.transform);
            answer.GetComponentInChildren<TextMeshProUGUI>().text = _dataProcessor.inputAnswerTexts[iteration][i];
            AnimationPlayer(answer);
        }
    }

    private void AnimationPlayer(GameObject gameObject)
    {
        gameObject.SetActive(true);
        gameObject.GetComponent<Animator>().SetTrigger("StartPlaying");
    }

    public void DataSwitcher()
    {
        if(_counter < _dataProcessor.numberOfQuestions - 1)
        {
            _counter++;

            DestroyAnswerButtons();

            QuestionInit(_counter);
            AnswerSelectionInit(_counter);
        }

        else
        {
            LoadFinalPanel();
        }
    }

    private void LoadFinalPanel()
    {
        _mainPanel.SetActive(false);
        _finalPanel.SetActive(true);
    }

    private static void DestroyAnswerButtons()
    {
        GameObject[] answerButtons = GameObject.FindGameObjectsWithTag("Answer Button");
        foreach (GameObject button in answerButtons)
        {
            Destroy(button);
        }
    }
    public void ReloadApp()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}
