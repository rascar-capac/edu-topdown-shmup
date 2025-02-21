using UnityEngine;
using UnityEngine.InputSystem;

public class ShootingController : MonoBehaviour
{
    [SerializeField] private AShooter _shooter;
    [SerializeField] private InputActionReference _shootInput;

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

        if (_shootInput == null)
        {
            Debug.LogWarning("No input provided", this);

            return;
        }

        if (_shootInput.action.inProgress)
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
