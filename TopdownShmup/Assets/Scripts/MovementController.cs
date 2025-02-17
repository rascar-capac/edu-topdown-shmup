using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] private float _metersPerSecond = 5f;
    [SerializeField] private CharacterController _characterController;

    private Vector2 _direction;

    private void ReadInput()
    {
        _direction = Inputs.MoveInput;
    }

    private void ApplyMovement()
    {
        Vector3 movement = _metersPerSecond * new Vector3(_direction.x, 0f, _direction.y);

        if (_characterController != null)
        {
            _characterController.SimpleMove(movement);
        }
        else
        {
            transform.Translate(Time.deltaTime * movement, Space.World);
        }
    }

    private void Update()
    {
        ReadInput();
        ApplyMovement();
    }
}
