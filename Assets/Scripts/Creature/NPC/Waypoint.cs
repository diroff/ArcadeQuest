using UnityEngine;
using UnityEngine.Events;

public class Waypoint : MonoBehaviour
{
    [SerializeField] private float _waitTime;

    [SerializeField] private UnityEvent _enterAction;
    [SerializeField] private UnityEvent _waitAction;
    [SerializeField] private UnityEvent _exitAction;

    public float WaitTime => _waitTime;

    public void DoEnterAction()
    {
        Debug.Log("Enter action:" + gameObject);
        _enterAction?.Invoke();
    }

    public void DoWaitAction()
    {
        Debug.Log("Wait action:" + gameObject);
        _waitAction?.Invoke();
    }

    public void DoExitAction()
    {
        Debug.Log("Exit action:" + gameObject);
        _exitAction?.Invoke();
    }
}