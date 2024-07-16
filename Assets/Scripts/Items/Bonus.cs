using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider), typeof(MeshRenderer))]
public abstract class Bonus : MonoBehaviour, IInteractable
{
    [SerializeField] protected float Value;
    [SerializeField] protected float BonusTime;
    [SerializeField] protected Sprite Icon;

    [SerializeField] private AutoGUID _guid;

    public UnityEvent BonusStarted;
    public UnityEvent BonusEnded;
    public UnityEvent<float, float> BonusTimeChanged;

    public UnityAction <string> BonusTaked;

    protected float CurrentTime;
    protected Player Player;

    private BoxCollider _boxCollider;
    private MeshRenderer _meshRenderer;

    public Sprite BonusIcon => Icon;
    public string GUID => _guid.GUID;

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider>();
        _meshRenderer = GetComponent<MeshRenderer>();

        if (_guid == null)
            _guid = gameObject.AddComponent<AutoGUID>();
    }

    protected virtual void Start()
    {
        StartCoroutine(WaitBeforeEnabled());
    }

    public void Collect()
    {
        Destroy(gameObject);
    }

    private IEnumerator WaitBeforeEnabled()
    {
        yield return new WaitForSeconds(0.1f);
        _boxCollider.enabled = true;
    }

    public virtual void Interact(Player player)
    {
        BonusTaked?.Invoke(_guid.GUID);
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
            BonusTimeChanged?.Invoke(CurrentTime, BonusTime);
            yield return null;
        }

        StopBonus();
    }
}