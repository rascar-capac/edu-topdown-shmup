using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    [SerializeField] private List<GameEndCondition> _gameEndConditionList;

    [SerializeField] private UnityEvent<bool> _onGameEnded = new();

    public UnityEvent<bool> OnGameEnded => _onGameEnded;

    public void EndGame(bool gameIsWon)
    {
        OnGameEnded.Invoke(gameIsWon);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
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
    }
}
