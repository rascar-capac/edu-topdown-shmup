using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DamageSource : MonoBehaviour
{
    [SerializeField] private float _damageAmount = 1f;
    [SerializeField] private float _damagePeriod = 0f;
    [SerializeField] private List<string> _damageableTags;
    [Space]
    [SerializeField] private bool _dealsProximityDamage;
    [SerializeField] private float _damageRadius = 0;

    private Dictionary<Health, float> _damageTimerMap = new();
    private Dictionary<Health, float> _proximityDamageTimerMap = new();

    public bool TryDealDamageInstant(GameObject gameObject)
    {
        if (gameObject == null)
        {
            return false;
        }

        if (GameObjectHasDamageableHealth(gameObject, out Health health))
        {
            DealDamage(health);

            return true;
        }

        return false;
    }

    public bool TryStartDealingDamage(GameObject gameObject)
    {
        if (gameObject == null)
        {
            return false;
        }

        if (GameObjectHasDamageableHealth(gameObject, out Health health))
        {
            _damageTimerMap.Add(health, 0f);

            return true;
        }

        return false;
    }

    public bool TryStopDealingDamage(GameObject gameObject)
    {
        if (gameObject == null)
        {
            return false;
        }

        if (GameObjectHasDamageableHealth(gameObject, out Health health))
        {
            _damageTimerMap.Remove(health);

            return true;
        }

        return false;
    }

    private bool GameObjectHasDamageableHealth(GameObject gameObject, out Health health)
    {
        health = null;

        return _damageableTags.Contains(gameObject.tag) && gameObject.TryGetComponent(out health);
    }

    private void DealDamage(Health health)
    {
        health.Hurt(_damageAmount);
    }

    private void DealDamageOverTime()
    {
        foreach (Health health in _damageTimerMap.Keys)
        {
            _damageTimerMap[health] += Time.deltaTime;

            while (_damageTimerMap[health] > _damagePeriod)
            {
                DealDamage(health);
                _damageTimerMap[health] -= _damagePeriod;
            }
        }
    }

    private void CheckProximityDamage()
    {
        if (!_dealsProximityDamage)
        {
            return;
        }

        Collider[] colliders = new Collider[10];
        Physics.OverlapSphereNonAlloc(transform.position, _damageRadius, colliders);

        //LINQ request that returns the intersection between the current map and the colliders array,
        //so only the elements whose key is also in the colliders
        _proximityDamageTimerMap = _proximityDamageTimerMap
            .Where(time => time.Key != null && colliders.Any(collider => collider != null && collider.gameObject == time.Key.gameObject))
            .ToDictionary(time => time.Key, time => time.Value);

        foreach (Collider collider in colliders)
        {
            if (collider == null)
            {
                continue;
            }

            if (!GameObjectHasDamageableHealth(collider.gameObject, out Health health))
            {
                continue;
            }

            if (!_proximityDamageTimerMap.ContainsKey(health))
            {
                _proximityDamageTimerMap.Add(health, 0f);
            }

            _proximityDamageTimerMap[health] += Time.deltaTime;

            while (_proximityDamageTimerMap[health] > _damagePeriod)
            {
                DealDamage(health);
                _proximityDamageTimerMap[health] -= _damagePeriod;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, _damageRadius);
    }

    private void Update()
    {
        DealDamageOverTime();
        CheckProximityDamage();
    }
}
