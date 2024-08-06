using UnityEngine;
using UnityEngine.Events;

public class Waypoint : MonoBehaviour
{
    [SerializeField] private float _waitTime;

    [SerializeField] private UnityEvent _enterAction;
    [SerializeField] private UnityEvent _waitAction;

    public float WaitTime => _waitTime;

    public void DoEnterAction()
    {
        _enterAction?.Invoke();
    }

    public void DoWaitAction()
    {
        _waitAction?.Invoke();
    }
}