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
    private Transform _previousTarget;

    private Vector3 _nextPosition;
    private Quaternion _nextRotation;

    private float _previousPositionSmoothing;

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

    public void ChangeTarget(Transform target)
    {
        _previousTarget = _target;
        _target = target;
    }

    public void UndoChangingTarget()
    {
        _target = _previousTarget;
    }

    public void ChangePositionSmoothing(float value)
    {
        _previousPositionSmoothing = _positionSmothing;
        _positionSmothing = value;
    }

    public void UndoChangingPositionSmoothing()
    {
        _positionSmothing = _previousPositionSmoothing;
    }

    private void Move()
    {
        if (_target == null)
            _target = _previousTarget;

        _nextPosition = Vector3.Lerp(_camera.transform.position, _target.transform.position + (_rotationOffset * _positionOffset), Time.fixedDeltaTime * _positionSmothing);
        _nextRotation = Quaternion.Lerp(_camera.transform.rotation, _rotationOffset, Time.fixedDeltaTime * _rotationSmothing);

        _camera.transform.position = _nextPosition;
        _camera.transform.rotation = _nextRotation;
    }
}