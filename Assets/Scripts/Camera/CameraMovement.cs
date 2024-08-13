using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform _target;

    [SerializeField] private Vector3 _positionOffset;
    [SerializeField] private Quaternion _rotationOffset;

    [SerializeField] private float _positionSmoothing = 8f;
    [SerializeField] private float _rotationSmoothing = 3f;

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

    public bool IsAtTarget()
    {
        float positionTolerance = 0.01f;
        float rotationTolerance = 1f;

        bool isPositionClose = Vector3.Distance(_camera.transform.position, _nextPosition) <= positionTolerance;
        bool isRotationClose = Quaternion.Angle(_camera.transform.rotation, _nextRotation) <= rotationTolerance;

        return isPositionClose && isRotationClose;
    }

    private void Move()
    {
        if (Time.deltaTime == 0 || Time.timeScale == 0)
            return;

        if (_target == null)
            _target = _defaultTarget;

        Vector3 targetPosition = _target.transform.position + (_rotationOffset * _positionOffset);
        Quaternion targetRotation = _rotationOffset;

        Vector3 currentPosition = _camera.transform.position;
        Quaternion currentRotation = _camera.transform.rotation;

        float rotationSpeed = Quaternion.Angle(currentRotation, targetRotation) / Time.deltaTime;
        float positionSmoothingFactor = Mathf.Lerp(_positionSmoothing, _positionSmoothing * 0.5f, rotationSpeed / 180.0f);

        _nextPosition = Vector3.Lerp(currentPosition, targetPosition, positionSmoothingFactor * Time.deltaTime);
        _nextRotation = Quaternion.Lerp(currentRotation, targetRotation, _rotationSmoothing * Time.deltaTime);

        _camera.transform.position = _nextPosition;
        _camera.transform.rotation = _nextRotation;
    }
}