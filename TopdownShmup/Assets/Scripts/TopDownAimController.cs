using UnityEngine;

[RequireComponent(typeof(TargetHolder))]
public class TopDownAimController : MonoBehaviour
{
    [SerializeField] private Aimer _aimer;
    [Tooltip("This is used to place the raycast plane on the Y axis. It will most likely be a gunpoint.")]
    [SerializeField] private TargetHolder _aimOriginHolder;

    private Camera _camera;

    private void CheckInputs()
    {
        if (_aimer == null)
        {
            Debug.LogWarning("No aimer provided", this);

            return;
        }

        if (_aimOriginHolder.Target == null)
        {
            Debug.LogWarning("No target provided in the aim origin holder", this);
        }

        if (_camera == null)
        {
            return;
        }

        Vector3 aimDirection = Vector3.zero;

        if (Inputs.CurrentAimStrategyIsTowards)
        {
            Vector2 joystickDirection2D = Inputs.AimTowardsInput;
            aimDirection = new Vector3(joystickDirection2D.x, 0f, joystickDirection2D.y);
        }
        else
        {
            Plane plane = new(Vector3.up, _aimOriginHolder.Target.position);
            Vector2 mousePosition = Inputs.AimAtInput;
            Ray ray = _camera.ScreenPointToRay(mousePosition);

            if (plane.Raycast(ray, out float distance))
            {
                Vector3 targetPosition = ray.GetPoint(distance);
                aimDirection = targetPosition - _aimOriginHolder.Target.position;
            }
        }

        _aimer.SetDirection(aimDirection);
    }

    private void OnValidate()
    {
        _aimOriginHolder = GetComponent<TargetHolder>();

        if (_aimer == null)
        {
            _aimer = GetComponent<Aimer>();
        }
    }

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        CheckInputs();
    }
}
