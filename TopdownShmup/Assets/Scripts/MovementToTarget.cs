using UnityEngine;

public class MovementToTarget : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _metersPerSecond = 5f;
    [SerializeField] private bool _lookAtTarget = false;

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    private void MoveToTarget()
    {
        if (_target == null)
        {
            return;
        }

        Vector3 velocity = _metersPerSecond * Time.deltaTime * (_target.position - transform.position).normalized;
        transform.Translate(velocity, Space.World);
    }

    private void Update()
    {
        MoveToTarget();
    }
}
