using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AnswerButton : MonoBehaviour
{
    public string ButtonText { get; private set; }

    public TextMeshProUGUI TMProText;

    [SerializeField] private DataProcessor _dataProcessor;

    public int Id { get; set; }

    private Button _thisButton;

    private void OnEnable()
    {
        _dataProcessor = FindObjectOfType<DataProcessor>();
        _thisButton = GetComponent<Button>();
        _thisButton.onClick.AddListener(GetOutput);
    }

    private void GetOutput()
    {
        ButtonText = TMProText.text;
        _dataProcessor.HandleOutput(Id, ButtonText);
    }

    private void OnDisable()
    {
        _thisButton.onClick.RemoveAllListeners();
    }
}
