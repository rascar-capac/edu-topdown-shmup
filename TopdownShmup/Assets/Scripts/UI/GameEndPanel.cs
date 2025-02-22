using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class GameEndPanel : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private Button _retryButton;
    [SerializeField] private Game _game;

    private void Show()
    {
        _canvasGroup.alpha = 1f;
        _scoreText.text = "no score";
        _retryButton.onClick.AddListener(RetryButton_OnClick);
    }

    private void Hide()
    {
        _canvasGroup.alpha = 0f;
    }

    private void Game_OnGameEnded(bool gameIsWon)
    {
        Show();
    }

    private void RetryButton_OnClick()
    {
        _game.RestartGame();
    }

    private void Awake()
    {
        Hide();
        _game.OnGameEnded.AddListener(Game_OnGameEnded);
    }
}
