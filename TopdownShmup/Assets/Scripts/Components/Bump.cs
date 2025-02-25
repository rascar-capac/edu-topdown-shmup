using UnityEngine;

public class Bump : MonoBehaviour
{
    [SerializeField] private TargetHolder _targetHolder;
    [SerializeField] private Vector3 _velocity;
    [SerializeField] private bool _triggersOnAwake;

    private Rigidbody _targetRigidbody;

    public Vector3 Velocity { get; set; }

    private void Trigger()
    {
        if (_targetHolder.Target == null || (_targetRigidbody == null && !_targetHolder.Target.TryGetComponent(out _targetRigidbody)))
        {
            return;
        }

        _targetRigidbody.AddForce(Velocity, ForceMode.Impulse);
    }

    private void Awake()
    {
        if (_targetHolder.Target == null)
        {
            Debug.LogWarning("No target provided.", this);
        }

        if (!_targetHolder.Target.TryGetComponent(out _targetRigidbody))
        {
            Debug.LogWarning("The target has no rigidbody.", this);
        }

        Velocity = _velocity;

        if (_triggersOnAwake)
        {
            Trigger();
        }
    }
}
