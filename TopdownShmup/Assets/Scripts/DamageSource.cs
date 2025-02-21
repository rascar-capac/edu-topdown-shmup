using System.Collections.Generic;
using UnityEngine;

public class DamageSource : MonoBehaviour
{
    [SerializeField] private float _damageAmount = 1f;
    [SerializeField] private List<string> _damageableTags;

    public void TryDealDamage(GameObject gameObject)
    {
        if (gameObject == null)
        {
            return;
        }

        if (_damageableTags.Contains(gameObject.tag) && gameObject.TryGetComponent(out Health health))
        {
            health.Hurt(_damageAmount);
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        TryDealDamage(collider.gameObject);
    }
}
