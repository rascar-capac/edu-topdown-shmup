using UnityEngine;

public abstract class AShooter : MonoBehaviour
{
    [SerializeField] private ParticleSystem _muzzleFlash;
    [SerializeField] private bool _autoShoots;
    [SerializeField] private float _shootingPeriod = 0.5f;

    private float _nextShootingTime;
    private bool _isShooting;

    public void ShootOnce()
    {
        Shoot();
    }

    public void Play()
    {
        _isShooting = true;
    }

    public void Stop()
    {
        _isShooting = false;
    }

    private void CheckShooting()
    {
        if (!_isShooting)
        {
            return;
        }

        if (Time.time < _nextShootingTime)
        {
            return;
        }

        Shoot();

        _nextShootingTime = Time.time + _shootingPeriod;
    }

    protected virtual void Shoot()
    {
        if (_muzzleFlash != null)
        {
            _muzzleFlash.Play();
        }
    }

    private void Update()
    {
        CheckShooting();
    }

    private void Awake()
    {
        if (_autoShoots)
        {
            Play();
        }
    }
}
