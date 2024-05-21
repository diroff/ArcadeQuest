using UnityEngine;
using UnityEngine.Events;

public class Waypoint : MonoBehaviour
{
    [SerializeField] private float _waitTime;

    [SerializeField] private UnityEvent _pointAction;

    public float WaitTime => _waitTime;

    public void DoAction()
    {
        _pointAction?.Invoke();
    }
}