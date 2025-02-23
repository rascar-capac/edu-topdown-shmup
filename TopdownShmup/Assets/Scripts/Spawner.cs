using System;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [Tooltip("The sum of probabilities should equal 1.")]
    [SerializeField] private List<SpawnProbabilityInfo> _prefabsToSpawn;
    [Tooltip("Normalized spawn period fluctuation. Keep this between 0 and 1 on both axes.")]
    [SerializeField] private AnimationCurve _spawnPeriodCurve;
    [Tooltip("Spawn periods corresponding to 0 and 1 on the curve's Y axis.")]
    [SerializeField] private Vector2 _minMaxSpawnPeriods = new(1f, 10f);
    [Tooltip("If this value is 100, then the end spawn period will be reached 100 seconds after the spawner has been started.")]
    [SerializeField] private float _timeSpanBetweenStartAndEndInSeconds = 120f;
    [Space]
    [SerializeField] private EPositionStrategy _positionStrategy;
    [SerializeField] private List<Transform> _spawnPositions;
    [SerializeField] private Vector3 _minSpawnPosition;
    [SerializeField] private Vector3 _maxSpawnPosition;
    [SerializeField] private NavMeshSurface _navMeshSurface;
    [SerializeField] private Vector3 _circleCenter;
    [SerializeField] private float _circleRadius;
    [Space]
    [SerializeField] private ERotationStrategy _rotationStrategy;
    [SerializeField] private Transform _targetedRotation;
    [SerializeField] private Vector3 _minSpawnRotation;
    [SerializeField] private Vector3 _maxSpawnRotation;
    [Space]
    [SerializeField] private Transform _container;
    [SerializeField] private bool _startsOnAwake;

    private float _spawnTimer;
    private List<GameObject> _spawnedObjects = new();
    private float _startTime;

    public void StartSpawn(bool forceRestart = false)
    {
        if (_startTime > Time.time || forceRestart)
        {
            _startTime = Time.time;
            _spawnTimer = 0f;
        }
    }

    public void Stop()
    {
        _startTime = float.MaxValue;
    }

    public void Spawn()
    {
        GameObject prefabToSpawn = GetRandomPrefabToSpawn();

        if (prefabToSpawn == null)
        {
            Debug.LogWarning("No spawnable prefab provided. Please fill the prefab list.", this);

            return;
        }

        GameObject spawnedObject = Instantiate(prefabToSpawn, GetSpawnPosition(), GetSpawnRotation(), _container);
        _spawnedObjects.Add(spawnedObject);
    }

    private void CheckSpawnTime()
    {
        if (Time.time < _startTime)
        {
            return;
        }

        _spawnTimer += Time.deltaTime;

        // we use a while to compensate potentially skipped spawns due to frame drops
        while (_spawnTimer > GetCurrentSpawnPeriod(currentTime: Time.time - _spawnTimer)) // we actually want the period at the previous spawn time (to know when to spawn next)
        {
            Spawn();
            _spawnTimer -= GetCurrentSpawnPeriod(currentTime: Time.time - _spawnTimer);
        }
    }

    private GameObject GetRandomPrefabToSpawn()
    {
        if (_prefabsToSpawn.Count == 0)
        {
            return null;
        }

        float randomValue = Random.value;
        float cummulativeProbability = 0f;

        foreach (SpawnProbabilityInfo probabilityInfo in _prefabsToSpawn)
        {
            cummulativeProbability += probabilityInfo.Probability;

            if (randomValue < cummulativeProbability)
            {
                return probabilityInfo.PrefabToSpawn;
            }
        }

        return _prefabsToSpawn[^1].PrefabToSpawn;
    }

    private Vector3 GetSpawnPosition()
    {
        switch (_positionStrategy)
        {
            case EPositionStrategy.AmongPositions:

                if (_spawnPositions.Count == 0)
                {
                    Debug.LogWarning("No spawn positions provided.", this);

                    return Vector3.zero;
                }

                return _spawnPositions[Random.Range(0, _spawnPositions.Count)].position;

            case EPositionStrategy.WithinRanges:

                return new(
                    Random.Range(_minSpawnPosition.x, _maxSpawnPosition.x),
                    Random.Range(_minSpawnPosition.y, _maxSpawnPosition.y),
                    Random.Range(_minSpawnPosition.z, _maxSpawnPosition.z)
                );

            case EPositionStrategy.OnNavMeshSurface:

                if (_navMeshSurface == null)
                {
                    Debug.LogWarning("No NavMeshSurface provided.", this);

                    return Vector3.zero;
                }

                Bounds navMeshBounds = _navMeshSurface.navMeshData.sourceBounds;

                Vector3 randomPointInBounds =
                    new(
                        Random.Range(navMeshBounds.min.x, navMeshBounds.max.x),
                        Random.Range(navMeshBounds.min.y, navMeshBounds.max.y),
                        Random.Range(navMeshBounds.min.z, navMeshBounds.max.z)
                    );

                if (NavMesh.SamplePosition(randomPointInBounds, out NavMeshHit hit, maxDistance: 100, NavMesh.AllAreas))
                {
                    return hit.position;
                }

                Debug.LogWarning("No position found on NavMeshSurface. Try increasing the max distance.", this);

                return Vector3.zero;

            case EPositionStrategy.OnCircle:

                Vector2 circleOffset = Random.insideUnitCircle.normalized * _circleRadius;

                return new(_circleCenter.x + circleOffset.x, _circleCenter.y, _circleCenter.z + circleOffset.y);

            default:

                return Vector3.zero;
        }
    }

    private Quaternion GetSpawnRotation()
    {
        switch (_rotationStrategy)
        {
            case ERotationStrategy.Target:

                if (_targetedRotation == null)
                {
                    Debug.LogWarning("No targeted rotation provided.", this);

                    return Quaternion.identity;
                }

                return _targetedRotation.rotation;

            case ERotationStrategy.WithinRanges:

                return Quaternion.Euler(
                    Random.Range(_minSpawnRotation.x, _maxSpawnRotation.x),
                    Random.Range(_minSpawnRotation.y, _maxSpawnRotation.y),
                    Random.Range(_minSpawnRotation.z, _maxSpawnRotation.z)
                );

            default:

                return Quaternion.identity;
        }
    }

    private float GetCurrentSpawnPeriod(float currentTime)
    {
        float timeOnCurve01 = Mathf.InverseLerp(_startTime, _startTime + _timeSpanBetweenStartAndEndInSeconds, currentTime);
        float periodOnCurve01 = _spawnPeriodCurve.Evaluate(timeOnCurve01);
        float currentSpawnPeriod = Mathf.Lerp(_minMaxSpawnPeriods.x, _minMaxSpawnPeriods.y, periodOnCurve01);

        return currentSpawnPeriod;
    }

    [ContextMenu("Start")]
    private void Test_Start()
    {
        StartSpawn(forceRestart: false);
    }

    [ContextMenu("Stop")]
    private void Test_Stop()
    {
        Stop();
    }

    [ContextMenu("Spawn")]
    private void Test_Spawn()
    {
        Spawn();
    }

    private void Awake()
    {
        Stop();

        if (_startsOnAwake)
        {
            StartSpawn();
        }
    }

    private void Update()
    {
        CheckSpawnTime();
    }

    [Serializable]
    private struct SpawnProbabilityInfo
    {
        [SerializeField] private GameObject prefabToSpawn;
        [SerializeField] private float probability;

        public readonly GameObject PrefabToSpawn => prefabToSpawn;
        public readonly float Probability => probability;
    }

    private enum EPositionStrategy
    {
        None = 0,
        AmongPositions = 1,
        WithinRanges = 2,
        OnNavMeshSurface = 3,
        OnCircle = 4
    }

    private enum ERotationStrategy
    {
        None = 0,
        Target = 1,
        WithinRanges = 2
    }
}