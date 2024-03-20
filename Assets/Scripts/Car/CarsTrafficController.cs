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
    private bool _isControllerEnabled = true;
    private bool _isWaitTime = false;

    public bool IsHorizontalDirection => _isHorizontalDirection;
    public bool IsWaitTime => _isWaitTime;

    private void Awake()
    {
        foreach (var item in _horizontalSpawners)
            item.SetTrafficController(this);

        foreach (var item in _verticalSpawners)
            item.SetTrafficController(this);
    }

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
        if (!_isControllerEnabled)
            return;

        _currentTimeToSpawn += Time.deltaTime;

        if (_currentTimeToSpawn < _timeToSpawn)
            return;

        DisableCurrentSpawners();

        _currentTimeToWait += Time.deltaTime;

        if (_currentTimeToWait < _timeToWait)
            return;

        StopAllSpawners();
        EnableNextDirection();
        _currentTimeToSpawn = 0;
        _currentTimeToWait = 0;
        _isWaitTime = false;
    }

    public void SetTrafficState(bool enabled)
    {
        _isControllerEnabled = enabled;
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
        _isWaitTime = true;
    }

    private void EnableNextDirection()
    {
        if (_isHorizontalDirectionNext)
            EnableHorizontalSpawners();
        else
            EnableVerticalSpawners();

        _isTrafficStopped = false;
    }

    private void StopAllSpawners()
    {
        foreach (var item in _horizontalSpawners)
            item.SetSpawnerState(false);

        foreach (var item in _verticalSpawners)
            item.SetSpawnerState(false);
    }
}