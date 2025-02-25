using UnityEngine;

public class SimpleMover : AMover
{
    [Tooltip("Optional.")]
    [SerializeField] private Vector3 _velocity;
    [Tooltip("Leave empty to directly move the transform.")]
    [SerializeField] private CharacterController _characterController;

    private void UpdateMovement()
    {
        Vector3 velocity = Type switch
        {
            EMovementType.Velocity => Velocity,
            EMovementType.Destination => MetersPerSecond * (Destination - transform.position).normalized,
            _ => Vector3.zero
        };

        if (_characterController != null)
        {
            _characterController.SimpleMove(velocity);
        }
        else
        {
            transform.Translate(Time.deltaTime * velocity, Space.World);
        }

        if (velocity == Vector3.zero)
        {
            _onMovementStopped.Invoke();
        }
        else
        {
            _onMovementStarted.Invoke();
        }
    }

    private void Awake()
    {
        SetVelocity(_velocity);
    }

    private void Update()
    {
        UpdateMovement();
    }
}
