using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    [SerializeField] private List<Car> _cars;

    [SerializeField] private float _spawnDelay = 2f;

    private bool _isSpawnEnabled = true;

    private void Start()
    {
        StartCoroutine(SpawnCar());
    }

    private IEnumerator SpawnCar()
    {
        while (_isSpawnEnabled)
        {
            int carNumber = Random.Range(0, _cars.Count);

            Instantiate(_cars[carNumber], transform.position, transform.rotation);
            yield return new WaitForSeconds(_spawnDelay);
        }
    }
}