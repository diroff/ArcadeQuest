using UnityEngine;

public class CameraTransformChanging : MonoBehaviour
{
    [SerializeField] private CameraMovement _cameraMovement;

    [SerializeField] private Quaternion _rotation;

    [SerializeField] private bool _isPositionChanged;
    [SerializeField] private Vector3 _position;

    private Quaternion _previouRotation;
    private Vector3 _previouPosition;

    public void RotateCamera()
    {
        _previouRotation = _cameraMovement.RotationOffset;
        _previouPosition = _cameraMovement.PositionOffset;

        _cameraMovement.ChangeOffsetRotation(_rotation);

        if(_isPositionChanged)
            _cameraMovement.ChangeOffsetPosition(_position);
    }

    public void UndoRotationOfCamera()
    {
        _cameraMovement.ChangeOffsetRotation(_previouRotation);

        if(_isPositionChanged)
            _cameraMovement.ChangeOffsetPosition(_previouPosition);
    }
}