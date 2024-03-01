using UnityEngine;
using UnityEngine.UI;

public class UIItemList : MonoBehaviour
{
    [SerializeField] private UIItemSlot _slotPrefab;
    [SerializeField] private LevelItems _levelItems;

    private GridLayoutGroup _layoutGroup;

    public GridLayoutGroup LayoutGroup => _layoutGroup;

    private void Awake()
    {
        _layoutGroup = GetComponent<GridLayoutGroup>();
        CreateGrid();
    }

    private void CreateGrid()
    {
        foreach (var item in _levelItems.Items)
        {
            var slot = Instantiate(_slotPrefab, transform);
            slot.SetItem(item);
        }
    }
}