using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game Instance { get; private set; }

    [SerializeField] private GameObject _player;

    public GameObject Player => _player;

    private void Awake()
    {
        Instance = this;
    }
}
