using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    [SerializeField] private List<GameEndCondition> _gameEndConditionList;

    [SerializeField] private UnityEvent _onGameStarted = new();
    [SerializeField] private UnityEvent<bool> _onGameEnded = new();

    public UnityEvent OnGameStarted => _onGameStarted;
    public UnityEvent<bool> OnGameEnded => _onGameEnded;

    public void EndGame(bool gameIsWon)
    {
        _onGameEnded.Invoke(gameIsWon);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
        _onGameStarted.Invoke();
    }

    private void GameEndCondition_OnTrue(bool gameIsWon)
    {
        EndGame(gameIsWon);
    }

    private void Awake()
    {
        foreach (GameEndCondition condition in _gameEndConditionList)
        {
            condition.OnTrue.AddListener(GameEndCondition_OnTrue);
        }

        _onGameStarted.Invoke();
    }
}
