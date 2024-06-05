using UnityEngine;

public class CarDestroyer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Obtacle"))
            return;

        var car = other.GetComponent<Car>();

        if (car == null)
            return;

        car.DestroyCar();
    }
}