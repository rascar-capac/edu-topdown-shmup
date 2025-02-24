using UnityEngine;
using UnityEngine.InputSystem;

public class MovementController : MonoBehaviour
{
    [SerializeField] private AMover _mover;
    [SerializeField] private float _metersPerSecond = 5f;
    [SerializeField] private InputActionReference _moveInput;

    private void CheckInputs()
    {
        if (_mover == null)
        {
            return;
        }

        if (_moveInput == null)
        {
            return;
        }

        Vector3 direction = _moveInput.action.ReadValue<Vector2>();

        //rename TopDownMovementController or make it more versatile
        _mover.SetVelocity(_metersPerSecond * new Vector3(direction.x, 0f, direction.y));
    }

    private void Awake()
    {
        if (_mover == null)
        {
            Debug.LogWarning("No mover provided.", this);
        }

        if (_moveInput == null)
        {
            Debug.LogWarning("No input provided.", this);
        }
    }

    private void Update()
    {
        CheckInputs();
    }
}
