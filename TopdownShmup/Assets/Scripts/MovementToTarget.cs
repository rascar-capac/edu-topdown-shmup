using UnityEngine;

[RequireComponent(typeof(TargetHolder))]
public class MovementToTarget : MonoBehaviour
{
    [SerializeField] private AMover _mover;
    [SerializeField] private TargetHolder _targetHolder;
    [Tooltip("If used with an agent mover, leave at 0 to keep the agent speed.")]
    [SerializeField] private float _metersPerSecond = 5f;

    public void SetMover(AMover mover)
    {
        _mover = mover;
    }

    private void MoveToTarget()
    {
        if (_mover == null)
        {
            Debug.LogWarning("No mover provided", this);

            return;
        }

        if (_targetHolder.Target == null)
        {
            Debug.LogWarning("No target provided in the target holder", this);

            return;
        }

        _mover.SetDestination(_targetHolder.Target.position, _metersPerSecond);
    }

    private void OnValidate()
    {
        _targetHolder = GetComponent<TargetHolder>();

        if (_mover == null)
        {
            _mover = GetComponent<AMover>();
        }
    }

    private void Update()
    {
        MoveToTarget();
    }
}
