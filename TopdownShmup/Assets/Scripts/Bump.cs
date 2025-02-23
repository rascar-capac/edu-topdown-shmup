using UnityEngine;

[RequireComponent(typeof(TargetHolder))]
public class Bump : MonoBehaviour
{
    [SerializeField] private TargetHolder _targetHolder;
    [SerializeField] private Vector3 _velocity;
    [SerializeField] private bool _triggersOnAwake;

    public Vector3 Velocity { get; set; }

    private void Trigger()
    {
        if (_targetHolder == null || _targetHolder.Target == null)
        {
            return;
        }

        if (!_targetHolder.TryGetComponent(out Rigidbody rigidbody))
        {
            Debug.LogWarning("The target has no rigidbody.", this);

            return;
        }

        rigidbody.AddForce(Velocity, ForceMode.Impulse);
    }

    private void Awake()
    {
        Velocity = _velocity;

        if (_triggersOnAwake)
        {
            Trigger();
        }
    }
}
