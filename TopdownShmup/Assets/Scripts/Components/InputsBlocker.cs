using UnityEngine;
using UnityEngine.InputSystem;

public class InputsBlocker : MonoBehaviour
{
    [SerializeField] private InputActionAsset _inputActions;

    public void SetInputsEnabled(bool enabled)
    {
        if (enabled)
        {
            _inputActions.Enable();
        }
        else
        {
            _inputActions.Disable();
        }
    }
}
