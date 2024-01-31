using UnityEngine;

public class ObtacleChecker : MonoBehaviour
{
    [SerializeField] private Car _car;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Obtacle"))
            return;

        Debug.Log("Moving of {" + gameObject.name + "} was stopped, because: {" + other.gameObject.name);
        _car.LetMove(false);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Obtacle"))
            return;

        Debug.Log("Moving of {" + gameObject.name + "} was continue, because: {" + other.gameObject.name);
        _car.LetMove(true);
    }
}