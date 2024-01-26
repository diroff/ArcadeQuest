public class SpeedBonus : Bonus
{
    public override void UseBonus()
    {
        base.UseBonus();
        Player.Movement.AddSpeed(Value);
    }

    public override void StopBonus()
    {
        Player.Movement.AddSpeed(-Value);
        base.StopBonus();
    }
}