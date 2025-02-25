using System.Collections;
using UnityEngine;

public class Destruction : MonoBehaviour
{
    [SerializeField] private TargetHolder _targetHolder;
    [SerializeField] private float _delayInSeconds;
    [SerializeField] private bool _triggersOnAwake;

    private WaitForSeconds _wait;

    public void Trigger()
    {
        StartCoroutine(AutoDestructCoroutine());
    }

    private IEnumerator AutoDestructCoroutine()
    {
        if (_targetHolder.IsEmpty)
        {
            yield break;
        }

        yield return _wait;

        Destroy(_targetHolder.Target.gameObject);
    }

    private void Awake()
    {
        if (_targetHolder.IsEmpty)
        {
            Debug.LogWarning("No target provided.", this);
        }

        if (_triggersOnAwake)
        {
            Trigger();
        }

        _wait = new WaitForSeconds(_delayInSeconds);
    }
}
