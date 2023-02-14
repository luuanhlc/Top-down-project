public class PlayerIdleState : PlayerBaseState
{

    public PlayerIdleState(PlayerStateManager _currentContex, StateFactory playerStateFactory) : base(_currentContex, playerStateFactory) { }

    public override void EnterState()
    {
        Ctx._ani.SetBool(Ctx.Idle, true);
    }

    public override void UpdateState( )
    {
        CheckSwitchState();
    }

    public override void ExitState()
    {
        Ctx._ani.SetBool(Ctx.Idle, false);
    }
    public override void CheckSwitchState()
    {
        if (Ctx.IsMovementPress && !Ctx.IsRunPress)
            SwitchState(Factory.Walk());
        else if (Ctx.IsMovementPress && Ctx.IsRunPress)
            SwitchState(Factory.Run());
        else if (!Ctx.IsMovementPress && Ctx.IsCrouching)
            SwitchState(Factory.Crouch());
        else if (Ctx.IsAttacking)
            SwitchState(Factory.Attack());
    }
    public override void InitializeSubState()
    {
    }
}
