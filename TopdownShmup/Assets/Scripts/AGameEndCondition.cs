using UnityEngine;
using UnityEngine.Events;

public abstract class GameEndCondition : MonoBehaviour
{
    [SerializeField] protected UnityEvent<bool> _onTrue = new();

    public UnityEvent<bool> OnTrue => _onTrue;
}
