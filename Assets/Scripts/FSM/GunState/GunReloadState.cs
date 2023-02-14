public class GunReloadState : GunBaseState
{
    public GunReloadState(GunStateManager _currentState, GunStateFactory _factory)
        : base(_currentState, _factory) { }

    public override void EnterState()
    {
        Ctx.GetWeaponIK.AttackHand = false;
        Ctx.PlayerState._ani.SetTrigger(Ctx.PlayerState.Reload);
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
        if (Ctx.CurrentBullet >= 0)
            SwitchState(Factory.Ready());
    }
}
