using UnityEngine;
using UnityEngine.Events;

public class Death : MonoBehaviour
{
    [SerializeField] private UnityEvent _onTriggered = new();

    public UnityEvent OnTriggered => _onTriggered;

    public void Trigger()
    {
        Destroy(gameObject);
        _onTriggered.Invoke();
    }
}
