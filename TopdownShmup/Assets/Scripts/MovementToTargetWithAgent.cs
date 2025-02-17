using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(TargetHolder))]
public class MovementToTargetWithAgent : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private TargetHolder _targetHolder;
    [SerializeField] private float _destinationUpdatePeriod = 0.1f;

    private float _nextUpdateTime;

    public void SetTarget(Transform transform)
    {
        _targetHolder.Target = transform;
    }

    private void CheckUpdateTime()
    {
        if (Time.time > _nextUpdateTime)
        {
            SetDestinationToTarget();
            _nextUpdateTime += _destinationUpdatePeriod;
        }
    }

    private void SetDestinationToTarget()
    {
        if (_targetHolder.Target == null)
        {
            return;
        }

        _navMeshAgent.SetDestination(_targetHolder.Target.position);
    }

    private void OnValidate()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _targetHolder = GetComponent<TargetHolder>();
    }

    private void Update()
    {
        CheckUpdateTime();
    }
}
