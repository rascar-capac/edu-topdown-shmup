using System.Collections.Generic;
using UnityEngine;

public class TargetHolder : MonoBehaviour
{
    [Tooltip("The target defaults to this object's transform.")]
    [SerializeField] private Transform _target;
    [Tooltip("If target is empty, this will be used to fetch a target (first one found). Leave empty to use the object's transform instead.")]
    [SerializeField] private List<string> _tagList;

    private Transform _actualTarget;

    public Transform Target
    {
        get
        {
            // lazy initialization: if the target must be fetched with a tag, it only gets initialized when we actually call Target.
            // this way, it removes the overhead from the scene initialization and ensures that the object will be looked for until found, without searching for it every frame
            if (_target == null && !TryFindTransformWithAnyTag(_tagList, out _actualTarget))
            {
                _actualTarget = transform;
            }

            return _actualTarget;
        }
        set
        {
            _actualTarget = value;
        }
    }

    private static bool TryFindGameObjectWithAnyTag(List<string> tags, out GameObject gameObject)
    {
        foreach (string tag in tags)
        {
            if (string.IsNullOrEmpty(tag))
            {
                continue;
            }

            if (TryFindGameObjectWithTag(tag, out gameObject))
            {
                return true;
            }
        }

        gameObject = null;

        return false;
    }

    private static bool TryFindTransformWithAnyTag(List<string> tags, out Transform transform)
    {
        foreach (string tag in tags)
        {
            if (string.IsNullOrEmpty(tag))
            {
                continue;
            }

            if (TryFindTransformWithTag(tag, out transform))
            {
                return true;
            }
        }

        transform = null;

        return false;
    }

    public static bool TryFindGameObjectWithTag(string tag, out GameObject gameObject)
    {
        try
        {
            gameObject = GameObject.FindWithTag(tag);

            return gameObject != null;
        }
        catch
        {
            gameObject = null;

            return false;
        }
    }

    public static bool TryFindTransformWithTag(string tag, out Transform transform)
    {
        if (TryFindGameObjectWithTag(tag, out GameObject gameObject))
        {
            transform = gameObject.transform;

            return true;
        }

        transform = null;

        return false;
    }

    private void OnValidate()
    {
        _actualTarget = _target;
    }
}
