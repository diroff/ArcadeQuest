using UnityEngine;

public class OverlayCameraSetup : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;

    private Camera _overlayCamera;
    private int _overlayLayer;

    private void Awake()
    {
        _overlayCamera = GetComponent<Camera>();
        _overlayLayer = _layerMask.value;
    }

    private void Start()
    {
        SetupOverlayCamera();
    }

    private void SetupOverlayCamera()
    {
        _overlayCamera.clearFlags = CameraClearFlags.Nothing;
        _overlayCamera.cullingMask = _overlayLayer;
        _overlayCamera.depth = Camera.main.depth + 1;
    }
}