using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class LevelTimer : MonoBehaviour
{
    [SerializeField] private LevelItems _levelItems;

    private float _currentTime;
    private bool _levelEnded = false;

    public UnityAction<float> LevelEndedWithTime;

    private void OnEnable()
    {
        _levelItems.AllItemsCollected += EndLevel;
    }

    private void OnDisable()
    {
        _levelItems.AllItemsCollected -= EndLevel;
    }

    private void Start()
    {
        StartCoroutine(TimeChecker());
    }

    private void EndLevel()
    {
        _levelEnded = true;
        LevelEndedWithTime?.Invoke(_currentTime);
    }

    private IEnumerator TimeChecker()
    {
        while (!_levelEnded)
        {
            _currentTime += Time.deltaTime;
            yield return null;
        }
    }
}