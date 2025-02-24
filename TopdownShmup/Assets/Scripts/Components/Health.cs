using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [Tooltip("If empty, float values below will be used.")]
    [SerializeField] private FloatInRange _healthAsset;
    [SerializeField] private float _initialHealth;
    [SerializeField] private float _minHealth;
    [SerializeField] private float _maxHealth;

    [SerializeField] private UnityEvent<float> _onHurt = new();
    [SerializeField] private UnityEvent<float> _onHealed = new();
    [SerializeField] private UnityEvent _onMinReached = new();

    private float _health;

    public UnityEvent<float> OnHurt => _onHurt;
    public UnityEvent<float> OnHealed => _onHealed;
    public UnityEvent OnMinReached => _onMinReached;

    public void Hurt(float damagePercentage)
    {
        ModifyHealth(-damagePercentage);
        _onHurt.Invoke(GetRatio());
    }

    public void Heal(float healPercentage)
    {
        ModifyHealth(healPercentage);
        _onHealed.Invoke(GetRatio());
    }

    private void ModifyHealth(float healthToAdd)
    {
        if (_healthAsset != null)
        {
            _healthAsset.Value += healthToAdd;

            if (_healthAsset.Value == _healthAsset.MinValue)
            {
                _onMinReached.Invoke();
            }
        }
        else
        {
            _health = Mathf.Clamp(_health + healthToAdd, _minHealth, _maxHealth);

            if (_health == _minHealth)
            {
                _onMinReached.Invoke();
            }
        }
    }

    private float GetRatio()
    {
        return _healthAsset != null ? _healthAsset.CurrentRatio : Mathf.InverseLerp(_minHealth, _maxHealth, _health);
    }

    private void Awake()
    {
        _health = _initialHealth;
    }
}
