using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AgentMover : AMover
{
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [Tooltip("Optional")]
    [SerializeField] private TargetHolder _targetHolder;
    [Tooltip("Leave at 0 to keep the agent speed.")]
    [SerializeField] private float _metersPerSecond = 5f;
    [SerializeField] private float _destinationUpdatePeriod = 0.1f;

    private float _nextUpdateTime;

    public override void SetDestination(Vector3 destination, float speed = 0f)
    {
        if (speed != 0f)
        {
            _navMeshAgent.speed = speed;
        }

        base.SetDestination(destination, speed);
    }

    private void SetDestinationTowardsTarget()
    {
        if (_targetHolder == null)
        {
            return;
        }

        if (_targetHolder.Target == null)
        {
            Debug.LogWarning("No target provided in the target holder", this);

            return;
        }

        SetDestination(_targetHolder.Target.position, MetersPerSecond);
    }

    private void UpdateMovement()
    {
        _navMeshAgent.velocity = Velocity;
    }

    private void CheckUpdateTime()
    {
        if (Time.time > _nextUpdateTime)
        {
            _navMeshAgent.SetDestination(Destination);
            _nextUpdateTime += _destinationUpdatePeriod;
        }
    }

    private void Update()
    {
        SetDestinationTowardsTarget();

        switch (Type)
        {
            case EMovementType.Velocity:

                UpdateMovement();

                break;

            case EMovementType.Destination:

                CheckUpdateTime();

                break;

            default:

                break;
        }
    }
}
