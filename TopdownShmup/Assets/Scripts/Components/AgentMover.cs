using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AgentMover : AMover
{
    private const float MIN_DESTINATION_UPDATE_PERIOD = 0.01f;

    [SerializeField] private NavMeshAgent _navMeshAgent;
    [Tooltip("Optional.")]
    [SerializeField] private TargetHolder _targetHolder;
    [Tooltip("Leave at 0 to keep the agent speed.")]
    [SerializeField] private float _metersPerSecond = 5f;
    [Tooltip("Period used to recompute the destination path.")]
    [SerializeField] private float _destinationUpdatePeriod = 0.1f;

    private float _updateTimer;

    public override void SetDestination(Vector3 destination, float metersPerSecond = 0f)
    {
        if (_navMeshAgent != null && metersPerSecond != 0f)
        {
            _navMeshAgent.speed = metersPerSecond;
        }

        base.SetDestination(destination, metersPerSecond);
    }

    private void SetDestinationTowardsTarget()
    {
        if (_targetHolder.Target == null)
        {
            return;
        }

        SetDestination(_targetHolder.Target.position, _metersPerSecond);
    }

    private void UpdateMovement()
    {
        if (_navMeshAgent == null)
        {
            return;
        }

        _navMeshAgent.velocity = Velocity;
    }

    private void CheckUpdateTime()
    {
        if (_navMeshAgent == null)
        {
            return;
        }

        _updateTimer -= Time.deltaTime;

        while (_updateTimer < 0f)
        {
            _navMeshAgent.SetDestination(Destination);
            _updateTimer += _destinationUpdatePeriod;
        }
    }

    private void OnValidate()
    {
        _destinationUpdatePeriod = Mathf.Max(MIN_DESTINATION_UPDATE_PERIOD, _destinationUpdatePeriod);
    }

    private void Awake()
    {
        if (_navMeshAgent == null)
        {
            Debug.LogWarning("No NavMeshAgent provided.", this);
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

        if (_navMeshAgent.velocity == Vector3.zero)
        {
            _onMovementStopped.Invoke();
        }
        else
        {
            _onMovementStarted.Invoke();
        }
    }
}
