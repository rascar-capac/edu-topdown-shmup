using TMPro;
using UnityEngine;

public class GameEndPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private Float _score;

    public void SetScoreText()
    {
        if (_scoreText != null && _score != null)
        {
            _scoreText.text = $"score: {_score.Value}";
        }
    }

    private void Awake()
    {
        if (_scoreText == null)
        {
            Debug.LogWarning("No score text provided", this);
        }

        if (_score == null)
        {
            Debug.LogWarning("No score provided", this);
        }
    }
}
