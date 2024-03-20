using UnityEngine;

public class MetaTrash : MonoBehaviour
{
    [SerializeField] private float _maxAngularVelocity;

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
        Vector3 targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition - _mousePosition);
        targetPos.y = transform.position.y;

        Vector3 direction = targetPos - transform.position;
        _rigidbody.velocity = direction / Time.fixedDeltaTime;
    }
}