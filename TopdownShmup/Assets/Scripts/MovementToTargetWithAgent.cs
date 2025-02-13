using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class MovementToTargetWithAgent : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _destinationUpdatePeriod = 0.1f;

    private NavMeshAgent _navMeshAgent;
    private float _nextUpdateTime;

    public void SetTarget(Transform target)
    {
        _target = target;
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
        if (_target == null)
        {
            return;
        }

        if (_navMeshAgent != null)
        {
            _navMeshAgent.SetDestination(_target.position);
        }
    }

    private void OnValidate()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        CheckUpdateTime();
    }
}
