using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "FloatRange", menuName = "Data/FloatRange")]
public class FloatInRange : ScriptableObject
{
    [SerializeField] private Float _current;
    [SerializeField] private float _minValue = 0;
    [SerializeField] private float _maxValue = 1;

    [SerializeField] private UnityEvent<float, float> _onValueChanged = new();

    public float MinValue => _minValue;
    public float MaxValue => _maxValue;
    public float Value
    {
        get
        {
            if (_current != null)
            {
                return _current.Value;
            }

            return 0f;
        }
        set
        {
            value = Mathf.Clamp(value, _minValue, _maxValue);

            if (_current != null)
            {
                _current.Value = value;
            }
        }
    }
    public float CurrentRatio => Mathf.InverseLerp(_minValue, _maxValue, _current != null ? _current.Value : 0f);

    public UnityEvent<float, float> OnValueChanged => _onValueChanged;

    public FloatInRange Clone()
    {
        FloatInRange clone = CreateInstance<FloatInRange>();
        clone._minValue = _minValue;
        clone._maxValue = _maxValue;

        if (_current != null)
        {
            clone._current = _current.Clone();
            clone._current.OnValueChanged.AddListener(newValue => clone._onValueChanged.Invoke(newValue, clone.CurrentRatio));
        }

        return clone;
    }

    private void OnEnable()
    {
        if (_current != null)
        {
            _current.OnValueChanged.AddListener(newValue => _onValueChanged.Invoke(newValue, CurrentRatio));
        }
    }

    private void OnValidate()
    {
        if (_current != null)
        {
            _current.Value = Mathf.Clamp(_current.Value, _minValue, _maxValue);
        }
    }
}
