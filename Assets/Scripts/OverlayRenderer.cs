using UnityEngine;

public class OverlayRenderer : MonoBehaviour
{
    [SerializeField] private GameObject _targetObject;
    [SerializeField] private Camera _overlayCamera;

    private int _originalLayer;
    private int _overlayLayer;

    private void Start()
    {
        _originalLayer = _targetObject.layer;
        _overlayLayer = LayerMask.NameToLayer("Overlay");

        SetupOverlayCamera();
    }

    private void SetupOverlayCamera()
    {
        _overlayCamera.clearFlags = CameraClearFlags.Nothing;
        _overlayCamera.cullingMask = 1 << _overlayLayer;
        _overlayCamera.depth = Camera.main.depth + 1;
    }

    public void SetObjectToOverlay()
    {
        _targetObject.layer = _overlayLayer;

        foreach (Transform child in _targetObject.transform)
        {
            child.gameObject.layer = _overlayLayer;
        }
    }

    [ContextMenu("Reset over layer")]
    public void ResetObjectLayer()
    {
        _targetObject.layer = _originalLayer;

        foreach (Transform child in _targetObject.transform)
        {
            child.gameObject.layer = _originalLayer;
        }
    }
}
