using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform _target;

    [SerializeField] private Vector3 _positionOffset;
    [SerializeField] private Quaternion _rotationOffset;

    [SerializeField] private float _positionSmothing = 7f;
    [SerializeField] private float _rotationSmothing = 5f;

    public Vector3 PositionOffset => _positionOffset;
    public Quaternion RotationOffset => _rotationOffset;

    private Camera _camera;

    private Vector3 _nextPosition;
    private Quaternion _nextRotation;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void ChangeOffsetPosition(Vector3 position)
    {
        _positionOffset = position;
    }

    public void ChangeOffsetRotation(Quaternion rotation)
    {
        _rotationOffset = rotation;
    }

    private void Move()
    {
        _nextPosition = Vector3.Lerp(_camera.transform.position, _target.transform.position + _positionOffset, Time.fixedDeltaTime * _positionSmothing);
        _nextRotation = Quaternion.Lerp(_camera.transform.rotation, _rotationOffset, Time.fixedDeltaTime * _rotationSmothing);

        _camera.transform.position = _nextPosition;
        _camera.transform.rotation = _nextRotation;
    }
}