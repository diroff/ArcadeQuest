using UnityEngine;
using UnityEngine.UI;

public class UIMetaItemsPanel : MonoBehaviour // Combine with UIItemPanel!
{
    [SerializeField] private int _maxItemsCount = 6;

    [SerializeField] private RectTransform _transform;
    [SerializeField] private ContentSizeFitter _sizeFilter;

    private int _slotsCount;

    private void OnTransformChildrenChanged()
    {
        AlignPanel();
    }

    private void AlignPanel()
    {
        _slotsCount = GetComponentsInChildren<UIMetaItemSlot>().Length;

        if (_slotsCount <= _maxItemsCount)
            CenteringPanel();
        else
            UncenteringPanel();
    }

    public void CenteringPanel()
    {
        _sizeFilter.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
        _transform.offsetMin = new Vector2(0, _transform.offsetMin.y);
        _transform.offsetMax = new Vector2(0, _transform.offsetMax.y);
    }

    public void UncenteringPanel()
    {
        _sizeFilter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
    }
}