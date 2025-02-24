using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private FloatInRange _data;
    [SerializeField] private bool _usesRangeAsClone;

    [SerializeField] private UnityEvent _onHurt = new();
    [SerializeField] private UnityEvent _onHealed = new();

    private FloatInRange _dataClone;

    public FloatInRange Data
    {
        get
        {
            if (_usesRangeAsClone)
            {
                if (_dataClone == null && _data != null)
                {
                    _dataClone = _data.Clone();
                }

                return _dataClone;
            }

            return _data;
        }
    }

    public UnityEvent OnHurt => _onHurt;
    public UnityEvent OnHealed => _onHealed;

    public void Hurt(float damagePercentage)
    {
        ModifyHealth(-damagePercentage);
        _onHurt.Invoke();
    }

    public void Heal(float healPercentage)
    {
        ModifyHealth(healPercentage);
        _onHealed.Invoke();
    }

    private void ModifyHealth(float healthToAdd)
    {
        if (Data != null)
        {
            Data.Value += healthToAdd;
        }
    }
}
