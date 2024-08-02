using UnityEngine;
using UnityEngine.Events;

public class Item : MonoBehaviour
{
    [SerializeField] protected int ItemPrefabID;
    [SerializeField] protected int ItemSceneID;
    [SerializeField] protected Sprite ItemIcon;

    protected bool ItemIsCollected = false;

    public UnityAction ItemWasCollected;
    public UnityAction<Item> ConcreteItemWasCollected;
    public UnityAction<int> ItemWasCollectedWithSceneID;
    public UnityAction<int> ItemWasCollectedWithPrefabID;

    public UnityAction<int> ItemWasDestroyedWithPrefabID;
    public UnityAction<Item> ConcreteItemWasDestroyed;
    public UnityAction ItemWasDestroyed;

    public Sprite Icon => ItemIcon;
    public int PrefabID => ItemPrefabID;
    public int SceneID => ItemSceneID;

    public bool IsCollected => ItemIsCollected;

    public virtual void Collect()
    {
        ItemIsCollected = true;

        ItemWasCollected?.Invoke();
        ConcreteItemWasCollected?.Invoke(this);
        ItemWasCollectedWithSceneID?.Invoke(ItemSceneID);
        ItemWasCollectedWithPrefabID?.Invoke(PrefabID);
    }

    public virtual void Destroy()
    {
        ItemWasDestroyed?.Invoke();
        ItemWasDestroyedWithPrefabID?.Invoke(ItemPrefabID);
        ConcreteItemWasDestroyed?.Invoke(this);
        Destroy(gameObject);
    }

    public void CollectImmediately()
    {
        Collect();
        Destroy();
    }
}