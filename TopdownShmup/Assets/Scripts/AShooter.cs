using UnityEngine;

public abstract class AShooter : MonoBehaviour
{
    [SerializeField] private ParticleSystem _muzzleFlash;
    [SerializeField] private bool _autoShoots;
    [SerializeField] private float _shootingPeriod = 0.5f;

    private float _shootingTimer;
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
        _shootingTimer -= Time.deltaTime;

        if (!_isShooting)
        {
            _shootingTimer = Mathf.Max(0f, _shootingTimer);

            return;
        }

        while (_shootingTimer < 0f)
        {
            Shoot();
            _shootingTimer += _shootingPeriod;
        }
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

    protected virtual void Awake()
    {
        if (_autoShoots)
        {
            Play();
        }
    }
}
