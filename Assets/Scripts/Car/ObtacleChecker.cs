using UnityEngine;

public class ObtacleChecker : MonoBehaviour
{
    private Car _car;

    private void Awake()
    {
        _car = GetComponentInParent<Car>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Obtacle") && !other.CompareTag("Player"))
            return;

        _car.LetMove(false);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Obtacle") && !other.CompareTag("Player"))
            return;

        _car.LetMove(true);
    }
}