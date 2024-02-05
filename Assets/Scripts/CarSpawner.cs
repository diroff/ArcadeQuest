using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    [SerializeField] private List<Car> _cars;

    [SerializeField] private float _spawnDelay = 2f;

    private float _currentTime;

    private bool _isSpawnEnabled = true;

    public void SetSpawnerState(bool isActive)
    {
        _isSpawnEnabled = isActive;
    }

    private void Update()
    {
        if (!_isSpawnEnabled)
            return;

        _currentTime += Time.deltaTime;

        if (_currentTime < _spawnDelay)
            return;

        int carNumber = Random.Range(0, _cars.Count);

        Instantiate(_cars[carNumber], transform.position, transform.rotation);

        _currentTime = 0;
    }
}