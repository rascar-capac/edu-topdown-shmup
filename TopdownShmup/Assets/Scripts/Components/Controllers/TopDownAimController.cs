using UnityEngine;
using UnityEngine.InputSystem;

public class TopDownAimController : MonoBehaviour
{
    [SerializeField] private TopDownAimer _aimer;
    [Tooltip("This is used to place the raycast plane on the Y axis. It will most likely be a gunpoint.")]
    [SerializeField] private TargetHolder _aimYOriginHolder;
    [SerializeField] private InputActionReference _aimAtInput;
    [SerializeField] private InputActionReference _aimTowardsInput;

    private Camera _camera;
    private bool _currentAimStrategyIsTowards;
    private Vector2 _lastAimTowardsDirection;
    private Vector2 _lastAimAtDirection;

    private void HookInputsToCurrentStrategy()
    {
        if (_aimAtInput != null)
        {
            _aimAtInput.action.performed += context =>
            {
                _currentAimStrategyIsTowards = false;
                _lastAimAtDirection = context.ReadValue<Vector2>();
            };
        }

        if (_aimTowardsInput != null)
        {
            _aimTowardsInput.action.performed += context =>
            {
                _currentAimStrategyIsTowards = true;
                _lastAimTowardsDirection = context.ReadValue<Vector2>();
            };
        }
    }

    private void CheckInputs()
    {
        if (_aimer == null)
        {
            return;
        }

        if (_aimYOriginHolder.Target == null)
        {
            return;
        }

        if (_camera == null)
        {
            return;
        }

        Vector3 aimDirection = Vector3.zero;

        if (_currentAimStrategyIsTowards)
        {
            Vector2 joystickDirection2D = _lastAimTowardsDirection;
            aimDirection = new Vector3(joystickDirection2D.x, 0f, joystickDirection2D.y);
        }
        else
        {
            float aimHeight = _aimYOriginHolder.Target.position.y;
            Plane plane = new(Vector3.up, -aimHeight);
            Vector2 mousePosition = _lastAimAtDirection;
            Ray ray = _camera.ScreenPointToRay(mousePosition);

            if (plane.Raycast(ray, out float distance))
            {
                Vector3 targetPosition = ray.GetPoint(distance);
                aimDirection = targetPosition - new Vector3(_aimer.transform.position.x, aimHeight, _aimer.transform.position.z);
            }
        }

        _aimer.SetDirection(aimDirection);
    }

    private void Awake()
    {
        _camera = Camera.main;

        if (_aimer == null)
        {
            Debug.LogWarning("No aimer provided.", this);
        }

        if (_aimYOriginHolder.IsEmpty)
        {
            Debug.LogWarning("No aim Y origin provided.", this);
        }

        if (_aimAtInput == null && _aimTowardsInput == null)
        {
            Debug.LogWarning("No input provided.", this);
        }

        HookInputsToCurrentStrategy();
    }

    private void Update()
    {
        CheckInputs();
    }
}
