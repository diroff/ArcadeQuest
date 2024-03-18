using UnityEngine;

public class MetaTrash : MonoBehaviour
{
    [SerializeField] private float _force = 50f;

    private Vector3 mousePosition;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private Vector3 GetMousePosition()
    {
        return Camera.main.WorldToScreenPoint(transform.position);
    }

    private void OnMouseDown()
    {
        mousePosition = Input.mousePosition - GetMousePosition();
    }

    private void OnMouseDrag()
    {
        Vector3 targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition - mousePosition);
        Vector3 force = (targetPos - rb.position) * _force;
        rb.AddForce(force);
    }
}