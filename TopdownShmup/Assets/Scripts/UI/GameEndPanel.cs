using TMPro;
using UnityEngine;

public class GameEndPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private Float _score;

    public void SetScoreText()
    {
        if (_scoreText != null)
        {
            _scoreText.text = $"score: {_score.Value}";
        }
    }
}
