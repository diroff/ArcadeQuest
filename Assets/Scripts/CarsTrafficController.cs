using System.Collections.Generic;
using UnityEngine;

public class CarsTrafficController : MonoBehaviour
{
    [SerializeField] private List<CarSpawner> _horizontalSpawners;
    [SerializeField] private List<CarSpawner> _verticalSpawners;

    [SerializeField] private bool _isHorizontalSpawnersEnabledFirst;

    [SerializeField] private float _timeToSpawn;
    [SerializeField] private float _timeToWait;

    private float _currentTimeToSpawn;
    private float _currentTimeToWait;

    private bool _isHorizontalDirection;
    private bool _isHorizontalDirectionNext;

    private bool _isTrafficStopped = false;

    private void Start()
    {
        if (_isHorizontalSpawnersEnabledFirst)
        {
            DisableVerticalSpawners();
            EnableHorizontalSpawners();
        }
        else
        {
            DisableHorizontalSpawners();
            EnableVerticalSpawners();
        }
    }

    private void Update()
    {
        _currentTimeToSpawn += Time.deltaTime;

        if (_currentTimeToSpawn < _timeToSpawn)
            return;

        DisableCurrentSpawners();

        _currentTimeToWait += Time.deltaTime;

        if (_currentTimeToWait < _timeToWait)
            return;

        EnableNextDirection();
        _currentTimeToSpawn = 0;
        _currentTimeToWait = 0;
    }

    private void EnableHorizontalSpawners()
    {
        foreach (var item in _horizontalSpawners)
            item.SetSpawnerState(true);

        _isHorizontalDirection = true;
    }

    private void EnableVerticalSpawners()
    {
        foreach (var item in _verticalSpawners)
            item.SetSpawnerState(true);
    }

    private void DisableHorizontalSpawners()
    {
        foreach (var item in _horizontalSpawners)
            item.SetSpawnerState(false);

        _isHorizontalDirection = false;
    }

    private void DisableVerticalSpawners()
    {
        foreach (var item in _verticalSpawners)
            item.SetSpawnerState(false);
    }

    private void DisableCurrentSpawners()
    {
        if (_isTrafficStopped)
            return;

        if (_isHorizontalDirection)
        {
            DisableHorizontalSpawners();
            _isHorizontalDirectionNext = false;
        }
        else
        {
            DisableVerticalSpawners();
            _isHorizontalDirectionNext = true;
        }

        _isTrafficStopped = true;
    }

    private void EnableNextDirection()
    {
        if (_isHorizontalDirectionNext)
            EnableHorizontalSpawners();
        else
            EnableVerticalSpawners();

        _isTrafficStopped = false;
    }
}