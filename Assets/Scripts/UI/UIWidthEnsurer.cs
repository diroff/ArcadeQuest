using UnityEngine;

public class UIWidthEnsurer : MonoBehaviour
{
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private RectTransform _viewportTransform;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _viewportTransform = transform.parent.GetComponent<RectTransform>();
    }

    private void OnRectTransformDimensionsChange()
    {
        EnsureMinWidth();
    }

    private void EnsureMinWidth()
    {
        if (_rectTransform.rect.width < _viewportTransform.rect.width)
            _rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _viewportTransform.rect.width);
    }
}