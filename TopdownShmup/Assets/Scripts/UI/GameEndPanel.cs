using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class GameEndPanel : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private Float _score;
    [SerializeField] private Button _retryButton;
    [SerializeField] private Game _game;

    private void Show()
    {
        if (_canvasGroup == null)
        {
            Debug.LogWarning("No CanvasGroup provided.", this);
        }
        else
        {
            _canvasGroup.alpha = 1f;
        }

        if (_scoreText != null)
        {
            _scoreText.text = $"score: {_score.Value}";
        }

        if (_retryButton != null)
        {
            _retryButton.onClick.AddListener(RetryButton_OnClick);
        }
    }

    private void Hide()
    {
        if (_canvasGroup == null)
        {
            Debug.LogWarning("No CanvasGroup provided.", this);
        }
        else
        {
            _canvasGroup.alpha = 0f;
        }
    }

    private void Game_OnGameEnded(bool gameIsWon)
    {
        Show();
    }

    private void RetryButton_OnClick()
    {
        if (_game != null)
        {
            _game.RestartGame();
        }
    }

    private void Awake()
    {
        Hide();

        if (_game != null)
        {
            _game.OnGameEnded.AddListener(Game_OnGameEnded);
        }
    }
}
