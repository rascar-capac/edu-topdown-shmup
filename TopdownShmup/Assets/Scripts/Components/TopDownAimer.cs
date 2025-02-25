using UnityEngine;

public class TopDownAimer : MonoBehaviour
{
    [Tooltip("Optional.")]
    [SerializeField] private TargetHolder _targetHolder;

    public TargetHolder TargetHolder => _targetHolder;
    public Vector3 Direction { get; private set; }

    public void SetDirection(Vector3 direction)
    {
        Direction = direction;
    }

    private void UpdateDirectionToTarget()
    {
        if (_targetHolder.Target == null)
        {
            return;
        }

        Direction = (_targetHolder.Target.position - transform.position).normalized;
        Direction = new Vector3(Direction.x, transform.position.y, Direction.z);
    }

    private void UpdateRotation()
    {
        if (Direction == Vector3.zero)
        {
            return;
        }

        transform.rotation = Quaternion.LookRotation(Direction);
    }

    private void Update()
    {
        UpdateDirectionToTarget();
        UpdateRotation();
    }
}
