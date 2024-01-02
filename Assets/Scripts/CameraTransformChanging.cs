using UnityEngine;

public class CameraTransformChanging : MonoBehaviour
{
    [SerializeField] private CameraMovement _cameraMovement;

    [SerializeField] private Quaternion _rotation;
    [SerializeField] private Vector3 _position;

    private Quaternion _previouRotation;
    private Vector3 _previouPosition;

    public void RotateCamera()
    {
        _previouRotation = _cameraMovement.RotationOffset;
        _previouPosition = _cameraMovement.PositionOffset;

        _cameraMovement.ChangeOffsetRotation(_rotation);
        _cameraMovement.ChangeOffsetPosition(_position);
    }

    public void UndoRotationOfCamera()
    {
        _cameraMovement.ChangeOffsetRotation(_previouRotation);
        _cameraMovement.ChangeOffsetPosition(_previouPosition);
    }
}