using UnityEngine;

public class ObtacleChecker : MonoBehaviour
{
    [SerializeField] private Car _car;

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