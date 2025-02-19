using System.Collections;
using UnityEngine;

public class HitScanShooter : AShooter
{
    [SerializeField] private TrailRenderer _bulletTrailPrefab;
    // since we shoot in hitscan, we don't want to target a specific speed, but rather a specific (short) duration
    [SerializeField] private float _trailDuration = 0.2f;
    [SerializeField] private ParticleSystem _collisionExplosionPrefab;
    [SerializeField] private Transform _fxContainer;

    protected override void Shoot()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo))
        {
            GameObject hitObject = hitInfo.collider.gameObject;

            if (_bulletTrailPrefab != null)
            {
                TrailRenderer bulletTrail = Instantiate(_bulletTrailPrefab, transform.position, Quaternion.identity, _fxContainer);
                StartCoroutine(MoveTrailRoutine(bulletTrail, transform.position, hitInfo.point));
            }

            if (_collisionExplosionPrefab != null)
            {
                ParticleSystem collisionExplosion = Instantiate(_collisionExplosionPrefab, hitInfo.point, Quaternion.identity, _fxContainer);
                collisionExplosion.transform.rotation = Quaternion.LookRotation(hitInfo.normal);
            }
        }
    }

    private IEnumerator MoveTrailRoutine(TrailRenderer trail, Vector3 origin, Vector3 destination)
    {
        float elapsedTime = 0f;

        while (elapsedTime < _trailDuration)
        {
            trail.transform.position = Vector3.Lerp(origin, destination, elapsedTime / _trailDuration);
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        trail.transform.position = destination;
    }
}
