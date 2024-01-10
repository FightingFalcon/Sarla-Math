using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public string Name { get; set; }
    public int Score { get; set; }
    public float TimeToAnswer { get; set; }


    public GameObject inputControls;
    public TMP_Text countdownText;
    public Text ProblemText; 
    public Text ScoreText;
    
    private string _playerName;
    private float _timeStarted;
     
    
    public delegate void AnswerSubmitted(string playerName, int? i, float timeToAnswer);
    public static event AnswerSubmitted OnAnswerSubmitted;

    void Start()
    {
        Name = name;
        Score = 0;
        _timeStarted = Time.time;
        ScoreText.text = Score.ToString();
    }

    public void SubmitAnswer(int? answer)
    {
        TimeToAnswer = Time.time - _timeStarted;
        if (OnAnswerSubmitted != null)
        {
            OnAnswerSubmitted(Name, answer, TimeToAnswer);
        }
    }

    public void IncrementScore(int scoreToAdd)
    {
        ScoreText.text = "Score: " + (Score += scoreToAdd).ToString();
    }

    public void UpdateScoreText()
    {
        ScoreText.text = "Score: " + Score.ToString();
    }
    
    public void SetScore(int scoreToSet)
    {
        Score = scoreToSet;
    }

    public void ResetScore()
    {
        Score = 0;
    }

    public void ToggleGameObject(GameObject gameObjectToToggle)
    {
        gameObjectToToggle.SetActive(!gameObjectToToggle.activeSelf);
    }
}