using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game Instance { get; private set; }

    [SerializeField] private GameObject _player;
    [SerializeField] private Camera _camera;

    public GameObject Player => _player;
    public Camera Camera => _camera;

    private void Awake()
    {
        Instance = this;

        if (_player == null)
        {
            Debug.LogWarning("No player provided.", Instance);
        }

        if (_camera == null)
        {
            Debug.LogWarning("No camera provided.", Instance);
        }
    }
}
