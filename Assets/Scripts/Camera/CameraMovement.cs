using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform _target;

    [SerializeField] private Vector3 _positionOffset;
    [SerializeField] private Quaternion _rotationOffset;

    [SerializeField] private float _positionSmoothing = 2f;
    [SerializeField] private float _rotationSmoothing = 2f;

    public Vector3 PositionOffset => _positionOffset;
    public Quaternion RotationOffset => _rotationOffset;

    private Camera _camera;
    private Transform _previousTarget;
    private Transform _defaultTarget;

    private Vector3 _nextPosition;
    private Quaternion _nextRotation;

    private float _previousPositionSmoothing;

    private void Awake()
    {
        _camera = Camera.main;
        _defaultTarget = _target;
    }

    private void LateUpdate()
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
        _previousPositionSmoothing = _positionSmoothing;
        _positionSmoothing = value;
    }

    public void UndoChangingPositionSmoothing()
    {
        _positionSmoothing = _previousPositionSmoothing;
    }

    private void Move()
    {
        if (_target == null)
            _target = _defaultTarget;

        _nextPosition = Vector3.Lerp(_camera.transform.position, _target.transform.position + (_rotationOffset * _positionOffset), _positionSmoothing * Time.deltaTime);
        _nextRotation = Quaternion.Lerp(_camera.transform.rotation, _rotationOffset, _rotationSmoothing * Time.deltaTime);

        _camera.transform.position = _nextPosition;
        _camera.transform.rotation = _nextRotation;
    }
}