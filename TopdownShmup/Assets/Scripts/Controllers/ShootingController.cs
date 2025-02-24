using UnityEngine;
using UnityEngine.InputSystem;

public class ShootingController : MonoBehaviour
{
    [SerializeField] private AShooter _shooter;
    [SerializeField] private InputActionReference _shootInput;

    private void CheckInputs()
    {
        if (_shooter == null)
        {
            return;
        }

        if (_shootInput == null)
        {
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

    private void Awake()
    {
        if (_shooter == null)
        {
            Debug.LogWarning("No shooter provided.", this);
        }

        if (_shootInput == null)
        {
            Debug.LogWarning("No input provided.", this);
        }
    }

    private void Update()
    {
        CheckInputs();
    }
}
