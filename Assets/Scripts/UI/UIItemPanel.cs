using UnityEngine;
using UnityEngine.UI;

public class UIItemPanel : MonoBehaviour // Combine with UIMetaItemsPanel!
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
        _slotsCount = GetComponentsInChildren<UIItemSlot>().Length;

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