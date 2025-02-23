using System;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Float", menuName = "Data/Float")]
public class Float : ScriptableObject
{
    [SerializeField] private float _initialValue;

    [SerializeField] private UnityEvent<float> _onValueChanged = new();

    [NonSerialized] private float _value;

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
            _onValueChanged.Invoke(_value);
        }
    }

    public UnityEvent<float> OnValueChanged => _onValueChanged;

    public void Add(float valueToAdd)
    {
        Value += valueToAdd;
    }

    public void Reset()
    {
        _value = _initialValue;
    }

    public Float Clone()
    {
        Float clone = CreateInstance<Float>();
        clone._initialValue = _initialValue;
        clone._value = _value;

        return clone;
    }

    private void OnEnable()
    {
        Reset();
    }
}
