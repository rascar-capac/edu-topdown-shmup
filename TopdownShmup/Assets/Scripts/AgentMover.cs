using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AgentMover : AMover
{
    [SerializeField] private NavMeshAgent _navMeshAgent;
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

    private void OnValidate()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
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
