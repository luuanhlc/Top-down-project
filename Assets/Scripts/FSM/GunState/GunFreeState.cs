public class GunFreeState : GunBaseState
{
    public GunFreeState(GunStateManager _currentState, GunStateFactory _factory)
        : base(_currentState, _factory) { }

    public override void EnterState()
    {
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
        if (Ctx.IsFirePress && !Ctx.IsShooting)
        {
            SwitchState(Factory.Fire());
        }
    }
}
