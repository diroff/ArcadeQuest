using UnityEngine;

public class CreatureAnimator : MonoBehaviour
{
    [SerializeField] protected Animator _animator;
    [SerializeField] private float _baseMaxSpeedForAnimation;

    private Creature _creature;

    private int _speed;

    private int _wait;
    private int _phone;
    private int _happy;
    private int _walk;

    private void Awake()
    {
        _creature = GetComponent<Creature>();

        _speed = Animator.StringToHash("Speed");
        _wait = Animator.StringToHash("Wait");
        _walk = Animator.StringToHash("Walk");
        _happy = Animator.StringToHash("Happy");
        _phone = Animator.StringToHash("Phone");
    }

    private void OnEnable()
    {
        _creature.SpeedWasChanged += UpdateAnimatorValues;
    }

    private void OnDisable()
    {
        _creature.SpeedWasChanged -= UpdateAnimatorValues;
}

    private void UpdateAnimatorValues(float moveAmount, float currentMovementSpeed)
    {
        float speedRatio = moveAmount / _baseMaxSpeedForAnimation;
        float animationSpeedMultiplier = CalculateAnimationSpeedMultiplier(moveAmount);

        _animator.SetFloat(_speed, speedRatio, 0.1f, Time.deltaTime);
        _animator.speed = animationSpeedMultiplier;
    }

    private float CalculateAnimationSpeedMultiplier(float moveAmount)
    {
        if (moveAmount > 0 && moveAmount <= _baseMaxSpeedForAnimation / 2)
            return 0.5f + (moveAmount / (_baseMaxSpeedForAnimation / 2)) * 0.5f;

        else if (moveAmount > _baseMaxSpeedForAnimation / 2)
        {
            float excessSpeed = moveAmount - _baseMaxSpeedForAnimation;
            float proportion = 1 + (excessSpeed / _baseMaxSpeedForAnimation);
            return Mathf.Max(1f, proportion);
        }

        else
            return 1f;
    }


    public void SetWaitPose()
    {
        _animator.ResetTrigger(_walk);
        _animator.SetTrigger(_wait);
    }

    public void SetPhonePose()
    {
        _animator.ResetTrigger(_walk);
        _animator.SetTrigger(_phone);
    }

    public void SetHappyPose()
    {
        _animator.ResetTrigger(_walk);
        _animator.SetTrigger(_happy);
    }

    public void ContinueWalk()
    {
        _animator.ResetTrigger(_walk);
        _animator.SetTrigger(_walk);
    }
}


// moveAmount = 10 / baseMovementSpeed = 10, currentMovementSpeed = 10 // _baseMaxSpeedForAnimation = 10 - run
// moveAmount = 5 / baseMovementSpeed = 5, currentMovementSpeed = 5 // _baseMaxSpeedForAnimation = 10 - walk
// moveAmount = 2.5 / baseMovementSpeed = 2.5, currentMovementSpeed = 2.5 // _baseMaxSpeedForAnimation = 10 - slow walk