using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private FloatInRange _healthRange;

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
        //update UI
    }

    // private void Update()
    // {
    //     CheckNearbyEnemies();
    // }
}
