using UnityEngine;
using Random = UnityEngine.Random;

public class MathProblem
{
    private int _coefficient;
    private int _constant;
    private int _answer;
    private int _solution;
    
    private string _problem;
    public DifficultyLevel difficulty;

    public MathProblem()
    {
        RandomizeProblem();
    }

    private void RandomizeProblem()
    {
        _coefficient = Random.Range(1, 5);
        _constant = Random.Range(-10, 11);
        _answer = Random.Range(1, 21);

        while ( (_answer - _constant) % _coefficient != 0)
        {
            _coefficient++;
        }

        _solution = (_answer - _constant ) / _coefficient;

        if (_coefficient == 1 && _constant <= 5 && _answer <= 10)
        {
            difficulty = DifficultyLevel.Easy;
        }
        else if (_coefficient <= 2 && _constant <= 10 && _answer <= 20)
        {
            difficulty = DifficultyLevel.Medium;
        }
        else
        {
            difficulty = DifficultyLevel.Hard;
        }
        
//        Debug.Log("Coefficient: " + _coefficient + "    Constant: " + _constant + "     Answer: " + _answer + "     Solution: " + _solution);
        _problem = GetFormattedProblem();
    }

    private string GetFormattedProblem()
    {
        string formattedProblem = "";

        if (_coefficient == 1)
        {
            formattedProblem += "x";
        }
        else if (_coefficient == -1)
        {
            formattedProblem += "-x";
        }
        else
        {
            formattedProblem += _coefficient + "x";
        }

        if (_constant > 0)
        {
            formattedProblem += " + " + _constant;
        }
        else if (_constant < 0)
        {
            formattedProblem += " - " + Mathf.Abs(_constant);
        }

        formattedProblem += " = " + _answer;

        return formattedProblem;
    }
    
    public string GetProblem()
    {
        return _problem;
    }

    public bool CheckAnswer(int? playerAnswer)
    {
        return (playerAnswer == _solution);
    }
}