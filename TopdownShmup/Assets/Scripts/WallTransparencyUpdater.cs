using System.Collections.Generic;
using UnityEngine;

public class WallTransparencyUpdater : MonoBehaviour
{
    [SerializeField] private Transform _player;

    private List<Material> _wallMaterials = new();

    private void UpdatePlayerPosition()
    {
        if (_player == null)
        {
            return;
        }

        foreach (Material material in _wallMaterials)
        {
            material.SetVector("_PlayerPosition", _player.position);
        }
    }

    private void Awake()
    {
        Renderer[] renderers = GetComponentsInChildren<Renderer>();

        foreach (Renderer renderer in renderers)
        {
            _wallMaterials.Add(renderer.material);
        }

        if (_player == null)
        {
            Debug.LogWarning("No player provided.", this);
        }
    }

    private void Update()
    {
        UpdatePlayerPosition();
    }
}
