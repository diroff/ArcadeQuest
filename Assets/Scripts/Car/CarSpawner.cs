using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    [SerializeField] private CarPool _carPool;

    [SerializeField] private float _spawnDelay = 2f;

    [SerializeField] private bool _isHorizontalSpawner;

    private float _currentTime;

    private bool _isSpawnEnabled = true;

    private CarsTrafficController _trafficController;

    public void SetSpawnerState(bool isActive)
    {
        _isSpawnEnabled = isActive;
    }

    public void SetTrafficState(bool enabled)
    {
        SetSpawnerState(enabled);
        _trafficController.SetTrafficState(enabled);

        if (_trafficController.IsWaitTime)
            SetSpawnerState(false);

        if (_isHorizontalSpawner && !_trafficController.IsHorizontalDirection)
            SetSpawnerState(false);

        if (!_isHorizontalSpawner && _trafficController.IsHorizontalDirection)
            SetSpawnerState(false);
    }

    public void SetTrafficController(CarsTrafficController controller)
    {
        _trafficController = controller;
    }

    private void Update()
    {
        if (!_isSpawnEnabled)
            return;

        _currentTime += Time.deltaTime;

        if (_currentTime < _spawnDelay)
            return;

        _carPool.SpawnFromPool(transform.position, transform.localRotation, this);
        _currentTime = 0;
    }
}