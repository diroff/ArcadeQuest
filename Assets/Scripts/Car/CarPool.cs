using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CarPool : MonoBehaviour
{
    [SerializeField] private int _poolItemCount;
    [SerializeField] private List<Car> _availableEnemies;

    private Dictionary<Car, Queue<Car>> _poolDictionary = new Dictionary<Car, Queue<Car>>();
    private List<Car> _currentEnemies = new List<Car>();

    public List<Car> CurrentEnemies => _currentEnemies;

    public UnityAction<Car> CarWasAdded;
    public UnityAction<Car> CarWasRemoved;

    private void Awake()
    {
        Initialize();
    }

    private void OnEnable()
    {
        foreach (var kvp in _poolDictionary)
        {
            foreach (var car in kvp.Value)
            {
                car.CarWasDestroyed += ReturnToPool;
            }
        }
    }

    private void OnDisable()
    {
        foreach (var kvp in _poolDictionary)
        {
            foreach (var car in kvp.Value)
            {
                car.CarWasDestroyed -= ReturnToPool;
            }
        }
    }

    public void Initialize()
    {
        foreach (var enemy in _availableEnemies)
        {
            CreatePool(enemy, _poolItemCount);
        }
    }

    private void CreatePool(Car prefab, int initialCount)
    {
        if (!_poolDictionary.ContainsKey(prefab))
        {
            var newQueue = new Queue<Car>();
            _poolDictionary[prefab] = newQueue;

            for (int i = 0; i < initialCount; i++)
            {
                Car newEnemy = Instantiate(prefab);
                newEnemy.gameObject.SetActive(false);
                _poolDictionary[prefab].Enqueue(newEnemy);
            }
        }
    }

    public Car SpawnFromPool(Vector3 position, Quaternion rotation, CarSpawner spawner)
    {
        if (_availableEnemies.Count == 0)
        {
            Debug.LogWarning("No available enemies to spawn.");
            return null;
        }

        Car prefab = _availableEnemies[Random.Range(0, _availableEnemies.Count)];

        if (!_poolDictionary.ContainsKey(prefab) || _poolDictionary[prefab].Count == 0)
            CreatePool(prefab, 1);

        Car enemyToSpawn = _poolDictionary[prefab].Count == 0 ? Instantiate(prefab) : _poolDictionary[prefab].Dequeue();

        enemyToSpawn.gameObject.SetActive(true);

        Rigidbody rigidbody = enemyToSpawn.GetComponent<Rigidbody>();
        rigidbody.position = position;
        rigidbody.rotation = rotation;

        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;

        enemyToSpawn.transform.position = position;
        enemyToSpawn.transform.rotation = rotation;

        rigidbody.WakeUp();

        enemyToSpawn.SetSpawner(spawner);

        _currentEnemies.Add(enemyToSpawn);
        CarWasAdded?.Invoke(enemyToSpawn);

        return enemyToSpawn;
    }

    public void ReturnToPool(Car car)
    {
        car.gameObject.SetActive(false);
        _currentEnemies.Remove(car);
        CarWasRemoved?.Invoke(car);

        foreach (var kvp in _poolDictionary)
        {
            if (kvp.Value.Contains(car))
            {
                _poolDictionary[kvp.Key].Enqueue(car);
                return;
            }
        }
    }
}