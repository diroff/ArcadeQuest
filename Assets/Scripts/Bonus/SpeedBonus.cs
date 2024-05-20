public class SpeedBonus : Bonus
{
    public override void UseBonus()
    {
        base.UseBonus();
        Player.AddSpeed(Value);
    }

    public override void StopBonus()
    {
        Player.AddSpeed(-Value);
        base.StopBonus();
    }
}