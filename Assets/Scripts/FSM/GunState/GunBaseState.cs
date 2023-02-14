public abstract class GunBaseState
{
    private GunStateManager _ctx;
    private GunStateFactory _factory;

    protected GunStateManager Ctx { get { return _ctx; } }
    protected GunStateFactory Factory { get { return _factory; } }

    public GunBaseState(GunStateManager currentContex, GunStateFactory factory)
    {
        _ctx = currentContex;
        _factory = factory;
    }

    public abstract void EnterState();

    public abstract void UpdateState();

    public abstract void ExitState();

    public abstract void CheckSwitchState();

    protected void SwitchState(GunBaseState newState)
    {
        ExitState();

        newState.EnterState();
        _ctx.CurrentGunState = newState;
    }

}
