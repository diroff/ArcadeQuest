using UnityEngine;

public class CarDestroyer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Obtacle"))
            return;

        Debug.Log("Obtacle in trigger!");

        var car = other.GetComponent<Car>();

        Debug.Log("Car == null?");

        if (car == null)
            return;

        Destroy(car.gameObject);
        Debug.Log("car was destroyed");
    }
}