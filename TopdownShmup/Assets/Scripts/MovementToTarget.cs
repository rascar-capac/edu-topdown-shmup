using UnityEngine;

[RequireComponent(typeof(TargetHolder))]
public class MovementToTarget : MonoBehaviour
{
    [SerializeField] private TargetHolder _targetHolder;
    [SerializeField] private float _metersPerSecond = 5f;
    [SerializeField] private bool _lookAtTarget = false;

    private void MoveToTarget()
    {
        if (_targetHolder.Target == null)
        {
            return;
        }

        Vector3 velocity = _metersPerSecond * Time.deltaTime * (_targetHolder.Target.position - transform.position).normalized;
        transform.Translate(velocity, Space.World);
    }

    private void OnValidate()
    {
        _targetHolder = GetComponent<TargetHolder>();
    }

    private void Update()
    {
        MoveToTarget();
    }
}
