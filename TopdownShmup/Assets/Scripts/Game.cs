using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Game : MonoBehaviour
{
    [SerializeField] private List<GameEndCondition> _gameEndConditionList;

    [SerializeField] private UnityEvent<bool> _onGameEnded = new();

    public UnityEvent<bool> OnGameEnded => _onGameEnded;

    private void EndGame(bool gameIsWon)
    {
        OnGameEnded.Invoke(gameIsWon);
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
