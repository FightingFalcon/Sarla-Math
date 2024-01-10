using UnityEngine;
using UnityEngine.UI;

public class InputHandler : MonoBehaviour
{
    public Player player;
    public Text currentAnswerText;
    public int? CurrentValue { get; private set; }
    private string _sign;
    private bool _isNegative;

    void Start()
    {
        _sign = string.Empty;
        _isNegative = false;
    }

    public void AppendDigit(int digit)
    {
        CurrentValue ??= 0;
        // Check if adding the digit would exceed the maximum value of an integer
        if (CurrentValue * 10L + digit > int.MaxValue)
            return;
        CurrentValue = (CurrentValue * 10) + digit;
        currentAnswerText.text = _sign + CurrentValue;
    }
    
    public void ChangeSign()
    {
        _isNegative = !_isNegative;
        _sign = _isNegative ?  _sign = "-" : _sign = string.Empty;
        currentAnswerText.text = CurrentValue == null ? _sign : _sign + CurrentValue;
    }
    
    public void ClearInput()
    {
        CurrentValue = null;
        _isNegative = false;
        _sign = string.Empty;
        currentAnswerText.text = string.Empty;
    }
    
    public void SetAnswer()
    {
        CurrentValue = _isNegative ? -CurrentValue : CurrentValue;
        player.SubmitAnswer(CurrentValue);
    }
}