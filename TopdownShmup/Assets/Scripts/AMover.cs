using UnityEngine;
using UnityEngine.Events;

public abstract class AMover : MonoBehaviour
{
    [SerializeField] protected UnityEvent _onMovementStarted = new();
    [SerializeField] protected UnityEvent _onMovementStopped = new();

    public Vector3 Velocity { get; protected set; }
    public Vector3 Destination { get; protected set; }
    public float MetersPerSecond { get; protected set; }
    public EMovementType Type { get; protected set; }

    public UnityEvent OnMovementStarted => _onMovementStarted;
    public UnityEvent OnMovementStopped => _onMovementStopped;

    public virtual void SetVelocity(Vector3 velocity)
    {
        Velocity = velocity;
        Type = EMovementType.Velocity;
    }

    public virtual void SetDestination(Vector3 destination, float _metersPerSecond = 1f)
    {
        Destination = destination;
        MetersPerSecond = _metersPerSecond;
        Type = EMovementType.Destination;
    }

    public enum EMovementType
    {
        Velocity = 0,
        Destination = 1
    }
}
