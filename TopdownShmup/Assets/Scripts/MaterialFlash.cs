using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class MaterialFlash : MonoBehaviour
{
    [SerializeField] private Renderer _renderer;
    [SerializeField] private Color _color;
    [SerializeField] private float _duration;

    private WaitForSeconds _wait;
    private Color _initialColor;
    private Coroutine _flashRoutine;

    public void Flash()
    {
        if (_flashRoutine != null)
        {
            StopCoroutine(_flashRoutine);
        }

        _flashRoutine = StartCoroutine(FlashCoroutine());
    }

    private IEnumerator FlashCoroutine()
    {
        if (_renderer == null)
        {
            yield break;
        }

        _renderer.material.color = _color;

        yield return _wait;

        _renderer.material.color = _initialColor;
    }

    private void Awake()
    {
        _initialColor = _renderer.material.color;
        _wait = new WaitForSeconds(_duration);
    }
}
