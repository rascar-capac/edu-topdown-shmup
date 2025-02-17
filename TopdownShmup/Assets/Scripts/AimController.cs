using UnityEngine;

public class AimController : MonoBehaviour
{
    [SerializeField] private Transform _targetToRotate;

    private Camera _camera;
    private Vector3 _aimDirection;

    private void ReadInput()
    {
        if (_camera == null)
        {
            return;
        }

        if (Inputs.CurrentAimStrategyIsTowards)
        {
            Vector2 joystickDirection2D = Inputs.AimTowardsInput;
            _aimDirection = new Vector3(joystickDirection2D.x, 0f, joystickDirection2D.y);
        }
        else
        {
            Plane plane = new(Vector3.up, transform.position);
            Vector2 mousePosition = Inputs.AimAtInput;
            Ray ray = _camera.ScreenPointToRay(mousePosition);

            if (plane.Raycast(ray, out float distance))
            {
                Vector3 targetPosition = ray.GetPoint(distance);
                _aimDirection = targetPosition - transform.position;
            }
            else
            {
                _aimDirection = Vector3.zero;
            }
        }
    }

    private void ApplyMovement()
    {
        Transform targetToRotate = _targetToRotate ?? transform;
        targetToRotate.rotation = Quaternion.LookRotation(_aimDirection);
    }

    private void Awake()
    {
        _camera = Game.Instance.Camera;
    }

    private void Update()
    {
        ReadInput();
        ApplyMovement();
    }
}
