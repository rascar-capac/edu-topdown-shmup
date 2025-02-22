using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private FloatInRange _healthRange;
    [SerializeField] private bool _usesRangeAsClone;

    public void Hurt(float damagePercentage)
    {
        ModifyHealth(-damagePercentage);
    }

    public void Heal(float healPercentage)
    {
        ModifyHealth(healPercentage);
    }

    private void ModifyHealth(float healthToAdd)
    {
        _healthRange.Value += healthToAdd;
    }

    private void Awake()
    {
        if (_usesRangeAsClone)
        {
            _healthRange = _healthRange.Clone();
        }
    }
}
