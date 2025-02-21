using UnityEngine;

[CreateAssetMenu(fileName = "Float", menuName = "Data/Float")]
public class Float : ScriptableObject
{
    [SerializeField] private float _value;

    private float _initialValue;

    public float Value
    {
        get => _value;
        set => _value = value;
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
