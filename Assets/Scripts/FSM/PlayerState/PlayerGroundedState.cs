public class PlayerGroundedState : PlayerBaseState
{
    public PlayerGroundedState(PlayerStateManager _currentState, StateFactory stateFactory) 
        : base(_currentState, stateFactory) 
        { IsRootState = true; InitializeSubState(); }

    public override void EnterState()
    {
        Ctx._ani.SetBool(Ctx.Idle, true);
        Ctx._ani.ResetTrigger(Ctx.jumping);

        if(Ctx.GetWeaponIK != null)
            Ctx.GetWeaponIK.AttackHand = true;
    }

    public override void UpdateState()
    {
        CheckSwitchState();
    }

    public override void ExitState()
    {
        Ctx._ani.SetBool(Ctx.Idle, false);
    }
    public override void CheckSwitchState()
    {
        
        if (Ctx.IsJumpPress && !Ctx.IsNeedToResetJump)
        {
            SwitchState(Factory.Jump());
        }
    }
    public override void InitializeSubState()
    {
        if (!Ctx.IsMovementPress)
            SetSubState(Factory.Idle());
        else if (Ctx.IsMovementPress && !Ctx.IsRunPress)
            SetSubState(Factory.Walk());
        else if(Ctx.IsMovementPress && Ctx.IsRunPress)
            SetSubState(Factory.Run());
    }
}
