using UnityEngine;

public abstract class AMover : MonoBehaviour
{
    public Vector3 Velocity { get; private set; }
    public Vector3 Destination { get; private set; }
    public float Speed { get; private set; }
    public EMovementType Type { get; private set; }

    public virtual void SetVelocity(Vector3 velocity)
    {
        Velocity = velocity;
        Type = EMovementType.Velocity;
    }

    public virtual void SetDestination(Vector3 destination, float speed = 1f)
    {
        Destination = destination;
        Speed = speed;
        Type = EMovementType.Destination;
    }

    public enum EMovementType
    {
        Velocity = 0,
        Destination = 1
    }
}
