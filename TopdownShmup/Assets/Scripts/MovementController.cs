using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] private AMover _mover;
    [SerializeField] private float _metersPerSecond = 5f;

    private void CheckInputs()
    {
        if (_mover == null)
        {
            Debug.LogWarning("No mover provided", this);

            return;
        }

        Vector3 direction = Inputs.MoveInput;

        //rename TopDownMovementController or make it more versatile
        _mover.SetVelocity(_metersPerSecond * new Vector3(direction.x, 0f, direction.y));
    }

    private void OnValidate()
    {
        if (_mover == null)
        {
            _mover = GetComponent<AMover>();
        }
    }

    private void Update()
    {
        CheckInputs();
    }
}
