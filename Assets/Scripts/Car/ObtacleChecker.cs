using UnityEngine;

public class ObtacleChecker : MonoBehaviour
{
    private IMoveableController _movable;

    private void Awake()
    {
        _movable = GetComponentInParent<IMoveableController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Obtacle") && !other.CompareTag("Player"))
            return;

        _movable.StopMove();
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Obtacle") && !other.CompareTag("Player"))
            return;

        _movable.StartMove();
    }
}