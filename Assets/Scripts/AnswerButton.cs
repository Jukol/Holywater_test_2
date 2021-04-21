using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AnswerButton : MonoBehaviour
{
    public int Id { get; set; }

    public TextMeshProUGUI TMProText;

    public delegate void ButtonListener(int id, string buttonText);
    public event ButtonListener eventOnClick;

    private Button _thisButton;

    private string _buttonText;

    private void OnEnable()
    {
        _thisButton = GetComponent<Button>();
        _thisButton.onClick.AddListener(GetOutput);
    }

    private void GetOutput()
    {
        _buttonText = TMProText.text;
        eventOnClick?.Invoke(Id, _buttonText);
    }

    private void OnDisable()
    {
        _thisButton.onClick.RemoveListener(GetOutput);
    }
}
