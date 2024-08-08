using UnityEngine;

public class OverlayRenderer : MonoBehaviour
{
    private int _overlayLayer;

    private void Start()
    {
        _overlayLayer = LayerMask.NameToLayer("Overlay");
    }

    public void SetObjectToOverlay(GameObject gameObject)
    {
        gameObject.layer = _overlayLayer;

        foreach (Transform child in gameObject.transform)
        {
            child.gameObject.layer = _overlayLayer;
        }
    }
}
