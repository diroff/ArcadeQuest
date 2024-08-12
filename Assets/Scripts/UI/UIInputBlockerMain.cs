using UnityEngine;

public class UIInputBlockerMain : UIInputBlocker
{
    [SerializeField] private Joystick _joystick;

    protected override void OnLevelCompleted()
    {
        _joystick.ResetValue();
        Destroy(_joystick);
        base.OnLevelCompleted();
    }
}