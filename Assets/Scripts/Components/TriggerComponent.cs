using UnityEngine;
using UnityEngine.Events;

public class TriggerComponent : MonoBehaviour
{
    [SerializeField] private string _tag;

    [SerializeField] private UnityEvent _enterAction;
    [SerializeField] private UnityEvent _exitAction;

    private void OnTriggerEnter(Collider other)
    {
        if (_enterAction == null)
            return;

        if (!other.CompareTag(_tag))
            return;

        _enterAction?.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        if (_exitAction == null)
            return;

        if (!other.CompareTag(_tag))
            return;

        _exitAction?.Invoke();
    }
}
