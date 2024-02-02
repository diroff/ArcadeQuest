using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarsTrafficController : MonoBehaviour
{
    [SerializeField] private List<CarSpawner> _horizontalSpawners;
    [SerializeField] private List<CarSpawner> _verticalSpawners;

    [SerializeField] private bool _isHorizontalSpawnersEnabledFirst;

    [SerializeField] private float _timeToSpawn;
    [SerializeField] private float _timeToWait;

    private bool _isHorizontalDirection;
    private bool _isTrafficEnabled;

    private void Start()
    {
        StartCoroutine(StartTrafficLighter());
    }

    private void EnableHorizontalSpawners()
    {
        foreach (var item in _horizontalSpawners)
            item.SetSpawnerState(true);

        _isHorizontalDirection = true;
        DisableVerticalSpawners();
    }

    private void EnableVerticalSpawners()
    {
        foreach (var item in _verticalSpawners)
            item.SetSpawnerState(true);

        DisableHorizontalSpawners();
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

    private void SwitchTrafficDirection()
    {
        if (_isHorizontalDirection)
            EnableVerticalSpawners();
        else
            EnableHorizontalSpawners();
    }

    private IEnumerator StartTrafficLighter()
    {
        if (_isHorizontalDirection)
        {
            EnableVerticalSpawners();

            yield return new WaitForSeconds(_timeToSpawn);

            DisableVerticalSpawners();

            yield return new WaitForSeconds(_timeToWait);

            EnableHorizontalSpawners();
            StartCoroutine(StartTrafficLighter());
        }
        else
        {
            EnableHorizontalSpawners();

            yield return new WaitForSeconds(_timeToSpawn);

            DisableHorizontalSpawners();

            yield return new WaitForSeconds(_timeToWait);

            EnableVerticalSpawners();
            StartCoroutine(StartTrafficLighter());
        }
    }
}