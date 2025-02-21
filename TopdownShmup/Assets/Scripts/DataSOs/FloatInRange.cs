using UnityEngine;

[CreateAssetMenu(fileName = "FloatRange", menuName = "Data/FloatRange")]
public class FloatInRange : ScriptableObject
{
    [SerializeField] private float _minValue = 0;
    [SerializeField] private float _maxValue = 1;
    [SerializeField] private float _value;

    private float _initialValue;

    public float MinValue => _minValue;
    public float MaxValue => _maxValue;
    public float Value
    {
        get => _value;
        set
        {
            _value = value;
            ClampValue();
        }
    }
    public float CurrentRatio => Mathf.InverseLerp(_minValue, _maxValue, _value);

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
