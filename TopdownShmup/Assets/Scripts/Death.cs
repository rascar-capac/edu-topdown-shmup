using UnityEngine;

[RequireComponent(typeof(Health))]
public class Death : MonoBehaviour
{
    [SerializeField] private Health _health;

    public void Trigger()
    {
        Destroy(gameObject);
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
        if (_health.Data != null)
        {
            _health.Data.OnValueChanged.AddListener(TrackingValue_OnValueChanged);
        }
    }
}
