using UnityEngine;

public class CreatureAnimator : MonoBehaviour
{
    [SerializeField] protected Animator _animator;

    private int _horizontal;
    private int _vertical;

    private int _wait;
    private int _phone;
    private int _happy;
    private int _walk;

    private void Awake()
    {
        _horizontal = Animator.StringToHash("Horizontal");
        _vertical = Animator.StringToHash("Vertical");
        _wait = Animator.StringToHash("Wait");
        _walk = Animator.StringToHash("Walk");
        _phone = Animator.StringToHash("Phone");
        _happy = Animator.StringToHash("Happy");

    }

    public void UpdateAnimatorValues(float horizontalMovement, float verticalMovement)
    {
        float snappedHorizontal;
        float snappedVertical;

        #region Snapped Horizontal

        if (horizontalMovement > 0 && horizontalMovement < 0.55f)
            snappedHorizontal = 0.5f;
        else if (horizontalMovement > 0.55f)
            snappedHorizontal = 1f;
        else if (horizontalMovement < 0 && horizontalMovement > -0.55f)
            snappedHorizontal = -0.5f;
        else if (horizontalMovement < -0.55f)
            snappedHorizontal = -1f;
        else
            snappedHorizontal = 0f;

        #endregion

        #region Snapped Vertical

        if (verticalMovement > 0 && verticalMovement < 0.55f)
            snappedVertical = 0.5f;
        else if (verticalMovement > 0.55f)
            snappedVertical = 1f;
        else if (verticalMovement < 0 && verticalMovement > -0.55f)
            snappedVertical = -0.5f;
        else if (verticalMovement < -0.55f)
            snappedVertical = -1f;
        else
            snappedVertical = 0f;

        #endregion

        _animator.SetFloat(_horizontal, snappedHorizontal, 0.1f, Time.deltaTime);
        _animator.SetFloat(_vertical, snappedVertical, 0.1f, Time.deltaTime);
    }

    public void SetWaitPose()
    {
        _animator.SetTrigger(_wait);
    }

    public void SetPhonePose()
    {
        _animator.SetTrigger(_phone);
    }

    public void SetHappyPose()
    {
        _animator.SetTrigger(_happy);
    }

    public void ContinueWalk()
    {
        _animator.SetTrigger(_walk);
    }
}