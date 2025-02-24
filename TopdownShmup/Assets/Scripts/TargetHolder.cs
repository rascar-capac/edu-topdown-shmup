using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct TargetHolder
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
            if (_target == null)
            {
                TryFindTransformWithAnyTag(_tagList, out _actualTarget);
            }
            else
            {
                _actualTarget = _target;
            }

            return _actualTarget;
        }
        set
        {
            _target = value;
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
}
