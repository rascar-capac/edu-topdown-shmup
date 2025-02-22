using UnityEngine;

public class DefeatOnPlayerDeath : GameEndCondition
{
    [SerializeField] private Float _playerHealth;

    private void PlayerHealth_OnValueChanged(float newValue)
    {
        if (newValue == 0f)
        {
            _onTrue.Invoke(false);
        }
    }

    private void Awake()
    {
        if (_playerHealth != null)
        {
            _playerHealth.OnValueChanged.AddListener(PlayerHealth_OnValueChanged);
        }
    }
}
