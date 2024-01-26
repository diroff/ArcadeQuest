using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider), typeof(MeshRenderer))]
public abstract class Bonus : MonoBehaviour, IInteractable
{
    [SerializeField] protected float Value;
    [SerializeField] protected float BonusTime;
    [SerializeField] protected Sprite Icon;

    public UnityEvent BonusStarted;
    public UnityEvent BonusEnded;
    public UnityEvent<float> BonusTimeChanged;

    protected float CurrentTime;
    protected Player Player;

    private BoxCollider _boxCollider;
    private MeshRenderer _meshRenderer;

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider>();
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    public virtual void Interact(Player player)
    {
        _boxCollider.enabled = false;
        _meshRenderer.enabled = false;

        Player = player;

        UseBonus();
    }

    public virtual void UseBonus()
    {
        CurrentTime = BonusTime;
        BonusStarted?.Invoke();
        StartCoroutine(TimeChecker());
    }

    public virtual void StopBonus()
    {
        BonusEnded?.Invoke();
        Destroy(gameObject);
    }

    protected IEnumerator TimeChecker()
    {
        while (CurrentTime > 0)
        {
            CurrentTime -= Time.deltaTime;
            BonusTimeChanged?.Invoke(CurrentTime);
            yield return null;
        }

        StopBonus();
    }
}