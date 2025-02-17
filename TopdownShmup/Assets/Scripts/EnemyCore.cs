using UnityEngine;

[RequireComponent(typeof(MovementToTargetWithAgent))]
public class EnemyCore : MonoBehaviour
{
    [SerializeField] private MovementToTargetWithAgent _movementToTarget;

    public void Initialize()
    {
        if (Game.Instance.Player != null)
        {
            _movementToTarget.SetTarget(Game.Instance.Player.transform);
        }
    }

    private void OnValidate()
    {
        _movementToTarget = GetComponent<MovementToTargetWithAgent>();
    }

    private void Awake()
    {
        Initialize();
    }
}
