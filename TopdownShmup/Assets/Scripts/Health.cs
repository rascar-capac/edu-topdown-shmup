using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 20f;
    [SerializeField, Range(0f, 1f)] private float _startingRatio = 1f;

    public float CurrentHealth { get; private set; }
    public float CurrentHealthRatio => CurrentHealth / _maxHealth;

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
        CurrentHealth = Mathf.Clamp(CurrentHealth + healthToAdd, 0f, _maxHealth);

        //update UI
        //trigger end game somehow if player health is at 0
    }

    // private void CheckNearbyEnemies()
    // {
    //     Collider[] colliders = Physics.OverlapSphere(transform.position, 0.7f);

    //     foreach (Collider collider in colliders)
    //     {
    //         if (collider.TryGetComponent(out EnemyBehaviour enemy))
    //         {
    //             enemy.HurtPlayer();
    //         }
    //     }
    // }

    private void Awake()
    {
        CurrentHealth = _maxHealth * _startingRatio;
        //update UI
    }

    // private void Update()
    // {
    //     CheckNearbyEnemies();
    // }
}
