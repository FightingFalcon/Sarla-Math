using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Canvas canvasBoard;
    public GameObject playerPrefab;
    public List<GameObject> players;
    public int numPlayers;
    public List<MathProblem> mathProblems;
    
    private int currentProblemIndex = 0;
    private int _numPlayersAnswered;

    private Dictionary<string, Tuple<int?, float>> _playerAnswers = new Dictionary<string, Tuple<int?, float>>();
    
    void Start()
    {
        // Initialize players
        players = new List<GameObject>();
        CreatePlayerObjects(); 

        // Initialize math problems
        mathProblems = new List<MathProblem>();
        mathProblems.Add(new MathProblem());
        mathProblems.Add(new MathProblem());
        mathProblems.Add(new MathProblem());
        mathProblems.Add(new MathProblem());
        mathProblems.Add(new MathProblem());
        mathProblems.Add(new MathProblem());
        mathProblems.Add(new MathProblem());
        StartGame();
    }
    
    void StartGame()
    {
        DisplayCurrentMathProblem();
    }

    void CreatePlayerObjects()
    {
        for (int i = 0; i < numPlayers; i++)
        {
            GameObject parentObject = GameObject.FindWithTag(Consts.PositionPlayer + (i+1)); // Find the parent GameObject
            Transform parentTransform = parentObject.transform;
            GameObject newPlayerObject = Instantiate(playerPrefab, parentTransform);
            newPlayerObject.name = Consts.PlayerName + (i + 1);

            Player newPlayer = newPlayerObject.GetComponent<Player>();
            newPlayer.Name = newPlayerObject.name;
            
            players.Add(newPlayerObject);
        }
    }
    
    private void OnEnable()
    {
        Player.OnAnswerSubmitted += HandleAnswerSubmitted;
    }
    
    private void OnDisable()
    {
        Player.OnAnswerSubmitted -= HandleAnswerSubmitted;
    }
    
    private void HandleAnswerSubmitted(string playerName, int? answer, float timeToAnswer)
    {
        if (!_playerAnswers.ContainsKey(playerName))
        {
            _playerAnswers.Add(playerName, Tuple.Create(answer, timeToAnswer));
            _numPlayersAnswered++;
            
            if (_numPlayersAnswered == numPlayers)
            {
                StartCoroutine(UpdateScores());
            }
        }
        else
        {
            _playerAnswers[playerName] = Tuple.Create(answer, timeToAnswer);
        }
    }
    
    private IEnumerator UpdateScores()
    {
        yield return new WaitForEndOfFrame();
        List<Player> correctPlayers = new List<Player>();
 
        foreach (GameObject playerObject in players)
        {
            var player = playerObject.GetComponent<Player>();
            var inputHandler = playerObject.GetComponent<InputHandler>();
            Tuple<int?, float> playerAnswer = _playerAnswers[player.gameObject.name];
            
            var correctAnswer = GetCurrentMathProblem().CheckAnswer(playerAnswer.Item1);
            if (correctAnswer)
            {
                player.Score += 3;
                correctPlayers.Add(player);
            }
            
            player.UpdateScoreText();
            inputHandler.ClearInput();
        }

        correctPlayers.Sort((p1, p2) => p1.TimeToAnswer.CompareTo(p2.TimeToAnswer));
        // Award points to each player based on their relative speed in answering correctly
        for (int i = 0; i < correctPlayers.Count; i++)
        {
            int points = correctPlayers.Count - 1 - i;
            correctPlayers[i].Score += points;
            correctPlayers[i].UpdateScoreText();
        } 

        _playerAnswers.Clear();
        _numPlayersAnswered = 0;
        IncrementCurrentMathProblemIndex();
    }

    public void IncrementCurrentMathProblemIndex()
    {
        currentProblemIndex++;
        if (currentProblemIndex < mathProblems.Count)
        {
            DisplayCurrentMathProblem();
        }
        else
        {
            players.ForEach(x => x.GetComponent<Player>().ProblemText.text = "Game Over");
        }
    }

    public void UpdatePlayerScore(Player player, int score)
    {
        player.IncrementScore(score);
    }

    public void ResetPlayerScores()
    {
        foreach (GameObject player in players)
        {
            player.GetComponent<Player>().ResetScore();
        }
    }
    
    private void DisplayCurrentMathProblem()
    {
        var problem = GetCurrentMathProblem();
        var problemString = problem.GetProblem();
        //problemText.text = problemString;
        players.ForEach(x => x.GetComponent<Player>().ProblemText.text = problemString);
    }
    
    public MathProblem GetCurrentMathProblem()
    {
        return currentProblemIndex < mathProblems.Count ? mathProblems[currentProblemIndex] : null;
    }

    public void SetCurrentMathProblemAnswer(int answer)
    {
        if (currentProblemIndex < mathProblems.Count)
        {
            //mathProblems[currentProblemIndex].SetAnswer(answer);
        }
    }
    
    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}