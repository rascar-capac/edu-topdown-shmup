using UnityEngine;

public class SimpleMover : AMover
{
    [Tooltip("Leave empty to directly move the transform")]
    [SerializeField] private CharacterController _characterController;

    private void UpdateMovement()
    {
        Vector3 velocity = Type switch
        {
            EMovementType.Velocity => Velocity,
            EMovementType.Destination => Speed * (Destination - transform.position).normalized,
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
    }

    private void Update()
    {
        UpdateMovement();
    }
}
