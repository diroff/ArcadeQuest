using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class MetaTimer : MonoBehaviour
{
    [SerializeField] private float _levelTime;

    [SerializeField] private MetaLevel _meta;

    public UnityAction<float, float> TimerWasUpdated;
    public UnityAction TimeWasOver;

    private float _currentTime;

    private bool _isTimerRunning;

    public float CurrentTime => _currentTime;

    private void Awake()
    {
        _currentTime = _levelTime;
    }

    private void OnEnable()
    {
        _meta.LevelWasCompleted += StopTimer;
    }

    private void OnDisable()
    {
        _meta.LevelWasCompleted -= StopTimer;
    }

    private void Start()
    {
        StartCoroutine(UpdateTimer());
    }

    private IEnumerator UpdateTimer()
    {
        _isTimerRunning = true;

        while (_isTimerRunning)
        {
            if (_currentTime <= 0)
            {
                _currentTime = 0;
                TimeWasOver?.Invoke();
                TimerWasUpdated?.Invoke(_currentTime, _levelTime);
                _isTimerRunning = false;
                break;
            }

            _currentTime -= Time.deltaTime;
            TimerWasUpdated?.Invoke(_currentTime, _levelTime);
            yield return null;
        }
    }

    private void StopTimer()
    {
        _isTimerRunning = false;
    }
}