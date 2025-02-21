using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "FloatRange", menuName = "Data/FloatRange")]
public class FloatInRange : ScriptableObject
{
    [SerializeField] private float _minValue = 0;
    [SerializeField] private float _maxValue = 1;
    [SerializeField] private float _value;

    [SerializeField] private UnityEvent<float, float> _onValueChanged = new();

    private float _initialValue;

    public float MinValue => _minValue;
    public float MaxValue => _maxValue;
    public float Value
    {
        get => _value;
        set
        {
            if (value == _value)
            {
                return;
            }

            _value = value;
            ClampValue();
            _onValueChanged.Invoke(_value, CurrentRatio);
        }
    }
    public float CurrentRatio => Mathf.InverseLerp(_minValue, _maxValue, _value);

    public UnityEvent<float, float> OnValueChanged => _onValueChanged;

    private void ClampValue()
    {
        _value = Mathf.Clamp(_value, _minValue, _maxValue);
    }

    public FloatInRange Clone()
    {
        FloatInRange clone = CreateInstance<FloatInRange>();
        clone._minValue = _minValue;
        clone._maxValue = _maxValue;
        clone._value = _value;

        return clone;
    }

    private void OnValidate()
    {
        ClampValue();
        _onValueChanged.Invoke(_value, CurrentRatio);
    }

    private void OnEnable()
    {
        _initialValue = _value;
    }

    private void OnDisable()
    {
        _value = _initialValue;
    }
}
