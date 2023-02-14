public class GunShottingState : GunBaseState
{
    public GunShottingState(GunStateManager _currentState, GunStateFactory _factory)
        : base(_currentState, _factory) { }

    public override void EnterState()
    {
        Ctx.IsShooting = true;
        Ctx.PlayerState._ani.SetBool(Ctx.PlayerState.shoot, true);
    }

    public override void UpdateState()
    {
        CheckSwitchState();
    }

    public override void ExitState()
    {
    }

    public override void CheckSwitchState()
    {
        if (Ctx.CurrentBullet == 0 && Ctx.BulletLeft > 0)
            SwitchState(Factory.Reload());
        else if (!Ctx.IsFirePress && Ctx.CurrentBullet > 0 && !Ctx.IsShooting)
            SwitchState(Factory.Ready());
    }
}
