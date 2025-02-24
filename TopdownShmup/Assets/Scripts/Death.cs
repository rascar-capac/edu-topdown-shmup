using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Health))]
public class Death : MonoBehaviour
{
    [SerializeField] private Health _health;

    [SerializeField] private UnityEvent _onTriggered = new();

    public UnityEvent OnTriggered => _onTriggered;

    public void Trigger()
    {
        Destroy(gameObject);
        _onTriggered.Invoke();
    }

    private void TrackingValue_OnValueChanged(float newValue, float ratio)
    {
        if (ratio == 0f)
        {
            Trigger();
        }
    }

    private void Awake()
    {
        if (_health == null)
        {
            Debug.LogWarning("No Health provided.", this);

            return;
        }

        if (_health.Data != null)
        {
            _health.Data.OnValueChanged.AddListener(TrackingValue_OnValueChanged);
        }
    }
}
