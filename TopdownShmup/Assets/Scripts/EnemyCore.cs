using UnityEngine;

public class EnemyCore : MonoBehaviour
{
    [SerializeField] private MovementToTargetWithAgent _movementToTarget;

    public void Initialize()
    {
        _movementToTarget.SetTarget(Game.Instance.Player.transform);
    }

    private void Awake()
    {
        Initialize();
    }
}
