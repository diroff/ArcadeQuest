using UnityEngine;

public class ObtacleChecker : MonoBehaviour
{
    [SerializeField] private Car _car;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Obtacle"))
            return;

        Debug.Log(gameObject.name + ":Car was stopped");

        _car.LetMove(false);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Obtacle"))
            return;

        Debug.Log(gameObject.name + ":Car was started");

        _car.LetMove(true);
    }
}