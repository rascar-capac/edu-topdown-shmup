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
        if (_targetHolder.Target == null)
        {
            Debug.LogWarning("No target provided.", this);

            yield break;
        }

        yield return _wait;

        Destroy(_targetHolder.Target.gameObject);
    }

    private void Awake()
    {
        if (_triggersOnAwake)
        {
            Trigger();
        }

        _wait = new WaitForSeconds(_delayInSeconds);
    }
}
