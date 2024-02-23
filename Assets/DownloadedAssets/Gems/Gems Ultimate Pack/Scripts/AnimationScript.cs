using System.Collections;
using UnityEngine;

public class AnimationScript : MonoBehaviour
{
    [SerializeField] private bool _isRotating = true;
    [SerializeField] private float _speed = 75f;

    private void OnDestroy()
    {
        StopCoroutine(RotateObject());
    }

    private void Start()
    {
        StartCoroutine(RotateObject());
    }

    private IEnumerator RotateObject()
    {
        while (_isRotating)
        {
            RotateObjectOnceTime();
            yield return null;
        }
    }

    private void RotateObjectOnceTime()
    {
        transform.Rotate(Vector3.up, _speed * Time.deltaTime, Space.Self);
    }
}