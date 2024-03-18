using UnityEngine;

public class MetaTrash : MonoBehaviour
{
    [SerializeField] private float _force = 50f;

    private Vector3 _mousePosition;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private Vector3 GetMousePosition()
    {
        return Camera.main.WorldToScreenPoint(transform.position);
    }

    private void OnMouseDown()
    {
        _mousePosition = Input.mousePosition - GetMousePosition();
    }

    private void OnMouseDrag()
    {
        Vector3 targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition - _mousePosition);
        Vector3 force = (targetPos - _rigidbody.position) * _force;
        _rigidbody.AddForce(force);
    }
}