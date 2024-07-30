using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class MainItem : Item, IInteractable
{
    private BoxCollider _boxCollider;
    public bool IsCollected => ItemIsCollected;

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider>();
    }

    public virtual void Interact(Player player)
    {
        Collect();
    }

    [ContextMenu("Collect")]
    public override void Collect()
    {
        _boxCollider.enabled = false;
        base.Collect();
        Destroy();
    }
}