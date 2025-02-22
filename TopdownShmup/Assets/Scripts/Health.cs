using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private FloatInRange _data;
    [SerializeField] private bool _usesRangeAsClone;

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
        if (Data != null)
        {
            Data.Value += healthToAdd;
        }
    }
}
