using UnityEngine;

public class MetaTrash : MonoBehaviour
{
    [SerializeField] private float _maxAngularVelocity;
    [SerializeField] private LayerMask _collisionLayerMask;

    private Vector3 _mousePosition;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        _rigidbody.angularVelocity = Vector3.ClampMagnitude(_rigidbody.angularVelocity, _maxAngularVelocity);
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
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition - _mousePosition);
        mouseWorldPosition.y = transform.position.y;

        Vector3 direction = mouseWorldPosition - transform.position;
        float distance = direction.magnitude;

        if (!Physics.Raycast(transform.position, direction.normalized, distance, _collisionLayerMask))
        {
            Vector3 targetPos = transform.position + direction;

            _rigidbody.MovePosition(targetPos);
        }
    }
}