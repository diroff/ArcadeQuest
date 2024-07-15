using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    protected float _currentTime;
    private bool _timerStopped = false;

    public UnityAction<float> TimerEnded;

    private void Start()
    {
        StartCoroutine(AddTime());
    }

    protected void StopTimer()
    {
        _timerStopped = true;
        TimerEnded?.Invoke(_currentTime);
    }

    private IEnumerator AddTime()
    {
        while (!_timerStopped)
        {
            _currentTime += Time.deltaTime;
            yield return null;
        }
    }
}