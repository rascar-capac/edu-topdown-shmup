using UnityEngine;

public class ShootingController : MonoBehaviour
{
    [SerializeField] private AShooter _shooter;

    public void SetShooter(AShooter shooter)
    {
        _shooter = shooter;
    }

    private void CheckInputs()
    {
        if (_shooter == null)
        {
            Debug.LogWarning("No shooter provided", this);

            return;
        }

        if (Inputs.IsFiring)
        {
            _shooter.Play();
        }
        else
        {
            _shooter.Stop();
        }
    }

    private void Update()
    {
        CheckInputs();
    }
}
