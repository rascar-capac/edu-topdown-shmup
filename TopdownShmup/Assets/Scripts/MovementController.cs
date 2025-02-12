using UnityEngine;
using UnityEngine.InputSystem;

public class MovementController : MonoBehaviour
{
    [SerializeField] private float _metersPerSecond = 5f;
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private InputAction _moveAction;

    private Vector2 _direction;

    private void ReadInput()
    {
        _direction = _moveAction.ReadValue<Vector2>();
    }

    private void ApplyMovement()
    {
        Vector3 movement = _metersPerSecond * Time.deltaTime * new Vector3(_direction.x, 0f, _direction.y);

        if (_characterController != null)
        {
            _characterController.Move(movement);
        }
        else
        {
            transform.Translate(movement, Space.World);
        }
    }

    private void OnEnable()
    {
        _moveAction.Enable();
    }

    private void OnDisable()
    {
        _moveAction.Disable();
    }

    private void Update()
    {
        ReadInput();
        ApplyMovement();
    }
}
