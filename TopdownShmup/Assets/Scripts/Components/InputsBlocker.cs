using UnityEngine;
using UnityEngine.InputSystem;

public class InputsBlocker : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputActions;

    public void SetInputsEnabled(bool enabled)
    {
        if (enabled)
        {
            inputActions.Enable();
        }
        else
        {
            inputActions.Disable();
        }
    }
}
